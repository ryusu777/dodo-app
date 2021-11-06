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
    public class GoodsRepo : IGoodsRepo
    {
        private readonly DodoAppContext _context;

        public GoodsRepo(DodoAppContext context)
        {
            _context = context;
        }
        
        
        /* 
            returns the created Goods Id
            returns -1 for internal server error
            returns -2 for conflict error
        */
        public async Task<int> CreateGoodsAsync(Goods request)
        {
            if (await _context.Goods.FirstOrDefaultAsync(g => g.GoodsCode == request.GoodsCode) != null)
            {
                return -2;
            }

            await _context.Goods.AddAsync(request);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }

            return request.Id;
        }

        public async Task<HttpStatusCode> DeleteGoodsAsync(int id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods == null)
            {
                return HttpStatusCode.NotFound;
            }

            _context.Goods.Remove(goods);
            await _context.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        public async Task<PageWrapper<List<Goods>>> GetGoodsAsync(PageFilter pageFilter)
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

            return await Pagination<Goods>.LoadPageAsync(qry, validPageFilter);
        }

        public async Task<HttpStatusCode> UpdateGoodsAsync(int id, Goods request)
        {

            if (await _context.Goods.FirstOrDefaultAsync(g => 
                g.GoodsCode == request.GoodsCode && g.Id != request.Id) != null)
            {
                return HttpStatusCode.Conflict;
            }
            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await _context.Goods.FirstOrDefaultAsync(g => g.Id == id)) == null)
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

        public async Task<Goods> GetGoodsByIdAsync(int id)
        {
            var goods = await _context.Goods.FirstOrDefaultAsync(g => g.Id == id);

            return goods;
        }
    }
}