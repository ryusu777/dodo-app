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
        private readonly ICurrencyRepo _currencyRepo;

        public TransactionRepo(DodoAppContext context, ICurrencyRepo currencyRepo)
        {
            _context = context;
            _currencyRepo = currencyRepo;
        }

        /*
            return values
            -1 -> Internal error
            -2 -> TransactionHeader doesn't exists
            -3 -> Goods doesn't exists
            -4 -> TransactionDetail exists
            -5 -> Goods amount is not enough for selling
        */
        // TODO: Check if goods stock is enough or currency is enough
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

        /*
            Returns:
            1 -> Successfully update data
            -1 -> Transaction detail not found
            -2 -> Goods amount is not enough
            -3 -> Internal server error
        */
        public async Task<int> UpdateTransactionDetailAsync(
            GoodsTransactionDetail request)
        {
            var transactionDetail = await _context.GoodsTransactionsDetails
                .Include(d => d.TheGoods)
                .Include(d => d.TheGoodsTransactionHeader)
                .FirstOrDefaultAsync(g => g.Id == request.Id);

            if (transactionDetail == null)
            {
                return -1;
            }

            if (request.GoodsAmount > transactionDetail.TheGoods.StockAvailable
                && transactionDetail
                    .TheGoodsTransactionHeader.TransactionType == "sell")
                return -2;

            _context.Entry(transactionDetail).State = EntityState.Modified;
            transactionDetail.PricePerItem = request.PricePerItem;
            transactionDetail.GoodsAmount = request.GoodsAmount;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -3;
            }

            return 1;
        }

        /*
            Return value
            -1: internal server error
            >= 1: id of created header
        */
        public async Task<int> CreateTransactionHeaderAsync(
            GoodsTransactionHeader transactionHeader)
        {
            transactionHeader.CreatedDate = DateTime.Now;
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

            return HttpStatusCode.NoContent;
        }

        public async Task<GoodsTransactionHeader> GetGoodsTransactionHeaderByIdAsync(int id)
        {
            var goods = await _context.GoodsTransactionHeaders
                .Include(g => g.GoodsTransactionDetails)
                .ThenInclude(c => c.TheGoods)
                .FirstOrDefaultAsync(g => g.Id == id);

            return goods;
        }

        public async Task<PageWrapper<List<GoodsTransactionHeader>>> GetGoodsTransactionHeadersAsync(
            PageFilter pageFilter, FilterGoodsTransactionHeader filter)
        {
            var validPageFilter = new PageFilter(pageFilter.Page, pageFilter.RowsPerPage, pageFilter.SortBy, pageFilter.Descending, pageFilter.SearchText);

            var qry = from s in _context.GoodsTransactionHeaders
                      select new GoodsTransactionHeader
                      {
                          Id = s.Id,
                          CreatedDate = s.CreatedDate,
                          PurchaseDate = s.PurchaseDate,
                          ReceiveDate = s.ReceiveDate,
                          TransactionType = s.TransactionType,
                          Vendor = s.Vendor,
                          GoodsTransactionDetails = s.GoodsTransactionDetails
                      };

            if (filter != null)
            {
                var predicate = PredicateBuilder.True<GoodsTransactionHeader>();

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

            return await Pagination<GoodsTransactionHeader>.LoadPageAsync(
                qry, validPageFilter);
        }

        // TODO: Change currency for done transaction
        /*
            Returns:
            1 -> Successfully edited data
            -1 -> Header Not found
            -2 -> One of the goods stock is not enough
            -3 -> Internal server error
        */
        public async Task<int> UpdateTransactionHeaderAsync(GoodsTransactionHeader transactionHeader)
        {
            var result = await ModifyGoodsAndCurrencyData(transactionHeader.Id);

            if (result < 0)
                return result;

            _context.Entry(transactionHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -3;
            }

            return 1;
        }

        /*
            Returns:
            -2 -> Transaction header not found
            -3 -> Goods not found
            -4 -> Transaction detail exists
            -5 -> Goods amount is not enough
        */
        private async Task<int> CheckTransferDetailValidity(
            GoodsTransactionDetail transactionDetail)
        {
            var header = await _context.GoodsTransactionHeaders
                .FirstOrDefaultAsync(h => 
                    h.Id == transactionDetail.GoodsTransactionHeaderId);

            if (header == null)
            {
                return -2;
            }

            var goods = await _context.Goods
                .FirstOrDefaultAsync(g => g.Id == transactionDetail.GoodsId);

            if (goods == null)
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


            if (goods.StockAvailable < transactionDetail.GoodsAmount 
                && header.TransactionType == "sell")
            {
                return -5;
            }
            return 0;
        }

        /*
            Returns:
            -1 -> Header not found
            -2 -> One of goods stock is not enough
            -3 -> Internal server error
        */
        private async Task<int> ModifyGoodsAndCurrencyData(int headerId)
        {
            var header = await _context.GoodsTransactionHeaders
                .Include(h => h.GoodsTransactionDetails)
                .ThenInclude(d => d.TheGoods)
                .FirstOrDefaultAsync(h => h.Id == headerId);
            
            if (header == null)
            {
                return -1;
            }

            foreach(var detail in header.GoodsTransactionDetails)
            {
                if (header.TransactionType == "sell")
                {
                    if (detail.TheGoods.StockAvailable < detail.GoodsAmount)
                        return -2;
                    detail.TheGoods.StockAvailable -= detail.GoodsAmount;
                }
                else
                {
                    detail.TheGoods.StockAvailable += detail.GoodsAmount;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -3;
            }

            _context.Entry(header).State = EntityState.Detached;

            return 1;
        }
    }
}