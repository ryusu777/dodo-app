using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Data;
using DodoApp.Domain;
using DodoApp.Helpers;

namespace DodoApp.Repository
{
    public class GoodsRepo : IGoodsRepo
    {
        private readonly DodoAppContext _context;

        public GoodsRepo(DodoAppContext context)
        {
            _context = context;
        }
        public Task<bool> CreateGoods(Goods request)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteGoods(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PageWrapper<List<Goods>>> GetGoods(PageFilter pageFilter)
        {
            var validPageFilter = new PageFilter(pageFilter.Page, pageFilter.RowsPerPage, pageFilter.SortBy, pageFilter.Descending, pageFilter.SearchText);

            var qry = _context.Goods.AsQueryable();

            if (!String.IsNullOrEmpty(validPageFilter.SearchText))
            {
                qry = qry.Where(
                    k => k.Name.Contains(validPageFilter.SearchText)
                    ||   k.Code.Contains(validPageFilter.SearchText)
                );
            }

            return await Pagination<Goods>.LoadPageAsync(qry, validPageFilter);
        }

        public Task<bool> UpdateGoods(int id, Goods request)
        {
            throw new System.NotImplementedException();
        }
    }
}