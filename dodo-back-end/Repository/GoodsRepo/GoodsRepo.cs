using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Controllers.V1;
using DodoApp.Data;
using DodoApp.Domain;
using DodoApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DodoApp.Repository
{
    public class GoodsRepo : IGoodsRepo
    {
        private readonly DodoAppContext _context;
        private readonly IMapper _mapper;

        public GoodsRepo(DodoAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        
        /* 
            returns the created Goods Id
            returns -1 for internal server error
            returns -2 for conflict error
        */
        public async Task<IActionResult> CreateGoodsAsync(CreateGoodsDto request)
        {
            if (await _context.Goods.FirstOrDefaultAsync(g => g.GoodsCode == request.GoodsCode) != null)
            {
                return new ConflictObjectResult(new { errors = new string[] { "Kode barang sudah dipakai "}});
            }
            var goods = _mapper.Map<Goods>(request);

            await _context.Goods.AddAsync(goods);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            return new OkObjectResult(new { id = goods.Id });
        }

        public async Task<IActionResult> DeleteGoodsAsync(int id)
        {
            var goods = await _context.Goods
                .FirstOrDefaultAsync(g => g.Id == id);
            if (goods == null)
            {
                return new NotFoundObjectResult(new { errors = new string[] { "Barang tidak ditemukan" }});
            }

            if (await _context.GoodsTransactionsDetails
                .AnyAsync(d => d.GoodsId == id))
            {
                return new BadRequestObjectResult(new { errors = new string[] { "Tidak bisa menghapus barang tersebut" }});
            }

            _context.Goods.Remove(goods);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ActionResult<PageWrapper<List<Goods>>>> GetGoodsAsync(PageFilter pageFilter)
        {
            var validPageFilter = new PageFilter(pageFilter.Page, pageFilter.RowsPerPage, pageFilter.SortBy, pageFilter.Descending, pageFilter.SearchText);

            var qry = _context.Goods.AsQueryable();

            if (!String.IsNullOrEmpty(validPageFilter.SearchText))
            {
                qry = qry.Where(
                    k => k.GoodsName.Contains(validPageFilter.SearchText)
                    ||   k.GoodsCode.Contains(validPageFilter.SearchText)
                    ||   k.PartNumber.Contains(validPageFilter.SearchText)
                    ||   k.CarType.Contains(validPageFilter.SearchText)
                );
            }

            return new OkObjectResult(await Pagination<Goods>.LoadPageAsync(qry, validPageFilter));
        }

        public async Task<IActionResult> UpdateGoodsAsync(int id, UpdateGoodsDto request)
        {
            var goods = await _context.Goods
                .Include(g => g.GoodsTransactionDetails)
                .ThenInclude(d => d.TheGoodsTransactionHeader)
                .FirstOrDefaultAsync(g => g.Id == request.Id);
            
            if ((await _context.Goods.FirstOrDefaultAsync(g => g.Id == id)) == null)
            {
                return new NotFoundObjectResult(new { errors = new string[] { "Barang tidak ditemukan" }});
            }

            if (await _context.Goods.AnyAsync(g => 
                g.GoodsCode == request.GoodsCode && g.Id != request.Id) == true)
            {
                return new ConflictObjectResult(new { errors = new string[] { "Kode barang sudah dipakai" }});
            }

            goods.GoodsName = request.GoodsName;
            goods.GoodsCode = request.GoodsCode;
            goods.StockAvailable = request.StockAvailable;
            goods.PartNumber = request.PartNumber;
            goods.PurchasePrice = request.PurchasePrice;
            goods.CarType = request.CarType;
            goods.MinimalAvailable = request.MinimalAvailable;

            foreach(var detail in goods.GoodsTransactionDetails)
            {
                if (detail.TheGoodsTransactionHeader.TransactionType == "purchase" 
                    && detail.TheGoodsTransactionHeader.PurchaseDate == null)
                {
                    detail.PricePerItem = request.PurchasePrice;
                }
            }

            _context.Entry(goods).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            return new NoContentResult();
        }

        public async Task<ActionResult<ReadGoodsDto>> GetGoodsByIdAsync(int id)
        {
            var goods = await _context.Goods.FirstOrDefaultAsync(g => g.Id == id);

            return _mapper.Map<ReadGoodsDto>(goods);
        }
    }
}