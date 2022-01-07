using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Data;
using DodoApp.Domain;
using DodoApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DodoApp.Repository
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly DodoAppContext _context;
        private readonly ICurrencyRepo _currencyRepo;
        private readonly IMapper _mapper;

        public TransactionRepo(
            DodoAppContext context, 
            ICurrencyRepo currencyRepo,
            IMapper mapper)
        {
            _context = context;
            _currencyRepo = currencyRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> CreateTransactionDetailAsync(CreateGoodsTransactionDetailDto request)
        {
            var transactionDetail = _mapper.Map<GoodsTransactionDetail>(request);
            var validity = await CheckTransferDetailValidity(transactionDetail);

            if (validity.GetType() != typeof(NoContentResult))
                return validity;

            await _context.GoodsTransactionsDetails.AddAsync(transactionDetail);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            return new OkObjectResult(new { id = transactionDetail.Id});
        }

        public async Task<IActionResult> UpdateTransactionDetailAsync(
            UpdateGoodsTransactionDetailDto request)
        {
            var transactionDetail = await _context.GoodsTransactionsDetails
                .Include(d => d.TheGoods)
                .Include(d => d.TheGoodsTransactionHeader)
                .FirstOrDefaultAsync(g => g.Id == request.Id);

            if (transactionDetail == null)
            {
                return new NotFoundObjectResult(new
                {
                    errors = new string[] { "Detil transaksi tidak ditemukan" }
                });
            }

            if (request.GoodsAmount > transactionDetail.TheGoods.StockAvailable
                && transactionDetail
                    .TheGoodsTransactionHeader.TransactionType == "sell")
                return new BadRequestObjectResult(new
                {
                    errors = new string[] { "Stok barang tidak mencukupi" }
                });

            _context.Entry(transactionDetail).State = EntityState.Modified;
            transactionDetail.PricePerItem = (int)request.PricePerItem;
            transactionDetail.GoodsAmount = (int)request.GoodsAmount;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult((int)HttpStatusCode
                    .InternalServerError);
            }

            return new NoContentResult();
        }

        public async Task<IActionResult> CreateTransactionHeaderAsync(
            CreateGoodsTransactionHeaderDto request)
        {
            var transactionHeader = _mapper.Map<GoodsTransactionHeader>(request);

            transactionHeader.CreatedDate = DateTime.Now;
            await _context.GoodsTransactionHeaders.AddAsync(transactionHeader);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            return new OkObjectResult(new { id = transactionHeader.Id });
        }

        public async Task<IActionResult> DeleteTransactionDetail(int id)
        {
            var detail = await _context.GoodsTransactionsDetails.FindAsync(id);
            if (detail == null)
            {
                return new NotFoundObjectResult(new
                {
                    errors = new string[] { "Detil transaksi tidak ditemukan" }
                });
            }

            _context.GoodsTransactionsDetails.Remove(detail);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        public async Task<IActionResult> DeleteTransactionHeaderAsync(int id)
        {
            var header = await _context.GoodsTransactionHeaders.FindAsync(id);
            if (header == null)
            {
                return new NotFoundObjectResult(new 
                    { 
                        errors = new string[] { "Transaksi tidak ditemukan" }
                    });
            }

            if (header.PurchaseDate != null)
            {
                return new BadRequestObjectResult(new { errors = new string[]
                {
                    "Transaksi telah dibayar, tidak bisa dihapus"
                }});
            }

            if (header.ReceiveDate != null)
            {
                return new BadRequestObjectResult(new { errors = new string[]
                {
                    "Transaksi telah diterima, tidak bisa dihapus"
                }});
            }

            _context.GoodsTransactionHeaders.Remove(header);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        public async Task<ActionResult<ReadGoodsTransactionHeaderDto>> GetGoodsTransactionHeaderByIdAsync(int id)
        {
            var transactionHeader = await _context.GoodsTransactionHeaders
                .Include(g => g.GoodsTransactionDetails)
                .ThenInclude(c => c.TheGoods)
                .FirstOrDefaultAsync(g => g.Id == id);
            
            if (transactionHeader == null)
            {
                return new NotFoundObjectResult(new { errors = new string[] { "Transaksi barang tidak ditemukan" }});
            }
            return new OkObjectResult(
                _mapper
                    .Map<ReadGoodsTransactionHeaderDto>(transactionHeader));
        }

        public async Task<ActionResult<PageWrapper<List<ReadGoodsTransactionHeaderDto>>>> GetGoodsTransactionHeadersAsync(
            PageFilter pageFilter, FilterGoodsTransactionHeader filter)
        {
            var validPageFilter = new PageFilter(pageFilter.Page, pageFilter.RowsPerPage, pageFilter.SortBy, pageFilter.Descending, pageFilter.SearchText);

            var qry = from s in _context.GoodsTransactionHeaders
                      select new ReadGoodsTransactionHeaderDto
                      {
                          Id = s.Id,
                          CreatedDate = s.CreatedDate,
                          PurchaseDate = s.PurchaseDate,
                          ReceiveDate = s.ReceiveDate,
                          TransactionType = s.TransactionType,
                          Vendor = s.Vendor,
                          Title = s.Title,
                          GoodsTransactionDetails = _mapper.Map<List<ReadGoodsTransactionDetailDto>>(s.GoodsTransactionDetails)
                      };

            if (filter != null)
            {
                var predicate = PredicateBuilder.True<ReadGoodsTransactionHeaderDto>();

                if (filter.PurchaseDateFrom != null)
                    predicate = predicate.And(h 
                        => h.PurchaseDate >= filter.PurchaseDateFrom);
                if (filter.PurchaseDateTo != null)
                    predicate = predicate.And(h 
                        => h.PurchaseDate <= filter.PurchaseDateTo);
                if (filter.ReceiveDateFrom != null)
                    predicate = predicate.And(h 
                        => h.ReceiveDate >= filter.ReceiveDateFrom);
                if (filter.ReceiveDateTo != null)
                    predicate = predicate.And(h 
                        => h.ReceiveDate <= filter.ReceiveDateTo);
                
                qry = qry.Where(predicate);
            }

            if (!String.IsNullOrEmpty(validPageFilter.SearchText))
            {
                qry = qry.Where(
                    k => k.Vendor.Contains(validPageFilter.SearchText)
                );
            }

            return new OkObjectResult(await Pagination<ReadGoodsTransactionHeaderDto>.LoadPageAsync(
                qry, validPageFilter));
        }

        public async Task<IActionResult> UpdateTransactionHeaderAsync(
            UpdateGoodsTransactionHeaderDto request)
        {
            var transactionHeader = await _context.GoodsTransactionHeaders
                .Include(h => h.GoodsTransactionDetails)
                .ThenInclude(d => d.TheGoods)
                .FirstOrDefaultAsync(h => h.Id == request.Id);
            
            if (transactionHeader == null)
            {
                return new NotFoundObjectResult(new 
                    { 
                        errors = new string[] 
                        { 
                            "Transaksi barang tidak ditemukan" 
                        }
                    });
            }

            var result = await ModifyGoodsAndCurrencyData(transactionHeader);

            transactionHeader.PurchaseDate = request.PurchaseDate;
            transactionHeader.ReceiveDate = request.ReceiveDate;
            _context
                .Entry(transactionHeader)
                .State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult(
                    (int) HttpStatusCode.InternalServerError);
            }

            return result;
        }

        private async Task<IActionResult> CheckTransferDetailValidity(
            GoodsTransactionDetail transactionDetail)
        {
            var header = await _context.GoodsTransactionHeaders
                .FirstOrDefaultAsync(h => 
                    h.Id == transactionDetail.GoodsTransactionHeaderId);

            if (header == null)
            {
                return new NotFoundObjectResult(new 
                { 
                    errors = new string[] 
                    {
                        "Transaksi tidak ditemukan"
                    }
                });
            }

            var goods = await _context.Goods
                .FirstOrDefaultAsync(g => g.Id == transactionDetail.GoodsId);

            if (goods == null)
            {
                return new NotFoundObjectResult(new
                {
                    errors = new string[]
                    {
                        "Barang tidak ditemukan"
                    }
                });
            }

            if (await _context.GoodsTransactionsDetails.AnyAsync(q =>
                q.GoodsId == transactionDetail.GoodsId && 
                q.GoodsTransactionHeaderId == 
                    transactionDetail.GoodsTransactionHeaderId) == true)
            {
                return new BadRequestObjectResult(new
                {
                    errors = new string[] { "Detil transaksi sudah ada" }
                });
            }


            if (goods.StockAvailable < transactionDetail.GoodsAmount 
                && header.TransactionType == "sell")
            {
                return new BadRequestObjectResult(new
                {
                    errros = new string[] { "Stok barang tidak mencukupi" }
                });
            }
            return new NoContentResult();
        }

        private async Task<IActionResult> ModifyGoodsAndCurrencyData(GoodsTransactionHeader header)
        {

            // Modify Goods Stock
            foreach(var detail in header.GoodsTransactionDetails)
            {
                if (header.TransactionType == "sell")
                {
                    if (detail.TheGoods.StockAvailable < detail.GoodsAmount)
                        return new BadRequestObjectResult(new 
                        { 
                            errors = new string[] 
                            { 
                                "Terdapat stok barang tidak mencukupi" 
                            }
                        });
                    detail.TheGoods.StockAvailable -= detail.GoodsAmount;
                }
                else
                {
                    detail.TheGoods.StockAvailable += detail.GoodsAmount;
                }
            }

            // Create Currency Report
            CreateCurrencyDto request = new CreateCurrencyDto 
            {
                TransactionHeaderId = header.Id,
                DateOfChange = DateTime.Now,
                ChangeDescription = header.TransactionType == "sell" ? 
                    "Transaksi penjualan barang" : "Transaksi restok barang"
            };

            await _currencyRepo.CreateCurrencyReportAsync(request);

            return new NoContentResult();
        }
    }
}