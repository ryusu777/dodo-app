using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Data;
using DodoApp.Domain;
using DodoApp.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DodoApp.Repository
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly DodoAppContext _context;

        public TransactionRepo(DodoAppContext context)
        {
            _context = context;
        }

        /*
            return values
            -1 -> Internal error
            -2 -> TransactionHeader doesn't exists
            -3 -> Goods doesn't exists
            -4 -> TransactionDetail exists
        */
        public async Task<int> CreateTransactionDetailAsync(GoodsTransactionDetail transactionDetail)
        {
            var validity = await CheckTransferDetailValidity(transactionDetail);
            if (validity < -1)
            {
                return validity;
            }

            await _context.GoodsTransactionsDetails.AddAsync(transactionDetail);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }

            return transactionDetail.Id;
        }

        public async Task<HttpStatusCode> UpdateTransactionDetailAsync(GoodsTransactionDetail request)
        {
            var transactionDetail = await _context.GoodsTransactionsDetails
                .FirstOrDefaultAsync(g => g.Id == request.Id);

            if (transactionDetail == null)
            {
                return HttpStatusCode.NotFound;
            }

            _context.Entry(transactionDetail).State = EntityState.Modified;
            transactionDetail.PricePerItem = request.PricePerItem;
            transactionDetail.GoodsAmount = request.GoodsAmount;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.NoContent;
        }

        /*
            Return value
            -1: internal server error
            >= 1: id of created header
        */
        public async Task<int> CreateTransactionHeaderAsync(GoodsTransactionHeader transactionHeader)
        {
            await _context.GoodsTransactionHeaders.AddAsync(transactionHeader);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }

            return transactionHeader.Id;
        }

        public async Task<HttpStatusCode> DeleteTransactionDetail(int id)
        {
            var detail = await _context.GoodsTransactionsDetails.FindAsync(id);
            if (detail == null)
            {
                return HttpStatusCode.NotFound;
            }

            _context.GoodsTransactionsDetails.Remove(detail);
            await _context.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> DeleteTransactionHeaderAsync(int id)
        {
            var header = await _context.GoodsTransactionHeaders.FindAsync(id);
            if (header == null)
            {
                return HttpStatusCode.NotFound;
            }

            _context.GoodsTransactionHeaders.Remove(header);
            await _context.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        public async Task<GoodsTransactionHeader> GetGoodsTransactionHeaderByIdAsync(int id)
        {
            var goods = await _context.GoodsTransactionHeaders
                .Include(g => g.GoodsTransactionDetails)
                .FirstOrDefaultAsync(g => g.Id == id);

            return goods;
        }

        public async Task<PageWrapper<List<GoodsTransactionHeader>>> GetGoodsTransactionHeadersAsync(PageFilter pageFilter)
        {
            var validPageFilter = new PageFilter(pageFilter.Page, pageFilter.RowsPerPage, pageFilter.SortBy, pageFilter.Descending, pageFilter.SearchText);

            var qry = from s in _context.GoodsTransactionHeaders
                      select new GoodsTransactionHeader
                      {
                          Id = s.Id,
                          CreatedDate = s.CreatedDate,
                          PurchaseDate = s.PurchaseDate,
                          ReceiveDate = s.ReceiveDate,
                          TotalPrice = s.TotalPrice,
                          TransactionType = s.TransactionType,
                          Vendor = s.Vendor
                      };

            if (!String.IsNullOrEmpty(validPageFilter.SearchText))
            {
                qry = qry.Where(
                    k => k.Vendor.Contains(validPageFilter.SearchText)
                );
            }

            return await Pagination<GoodsTransactionHeader>.LoadPageAsync(qry, validPageFilter);
        }

        public async Task<HttpStatusCode> UpdateTransactionHeaderAsync(GoodsTransactionHeader transactionHeader)
        {
            _context.Entry(transactionHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await _context.GoodsTransactionHeaders.FirstOrDefaultAsync(g => g.Id == transactionHeader.Id)) == null)
                {
                    return HttpStatusCode.NotFound;
                }
                else
                {
                    return HttpStatusCode.InternalServerError;
                }
            }

            return HttpStatusCode.NoContent;
        }

        private async Task<int> CheckTransferDetailValidity(
            GoodsTransactionDetail transactionDetail)
        {
            if (await _context.GoodsTransactionHeaders.AnyAsync(q => 
                q.Id == transactionDetail.GoodsTransactionHeaderId) == false)
            {
                return -2;
            }

            if (await _context.Goods.AnyAsync(q => 
                q.Id == transactionDetail.GoodsId) == false)
            {
                return -3;
            }

            if (await _context.GoodsTransactionsDetails.AnyAsync(q =>
                q.GoodsId == transactionDetail.GoodsId && 
                q.GoodsTransactionHeaderId == 
                    transactionDetail.GoodsTransactionHeaderId) == true)
            {
                return -4;
            }

            return 0;
        }
    }
}