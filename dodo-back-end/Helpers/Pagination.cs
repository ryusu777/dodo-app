using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DodoApp.Helpers
{
    public static class Pagination<T> where T : class
    {
        public static async Task<PageWrapper<List<T>>> LoadPageAsync(IQueryable<T> sourceQry, PageFilter filter, string defaultSortId = "Id")
        {
            var sortBy = LinqExtension.ConvertSortByParams(filter.SortBy, filter.Descending);
            if (sortBy == "")
                sortBy = $"{defaultSortId} DESC";
            var qry = LinqExtension.OrderBy(sourceQry, sortBy).AsNoTracking();

            var itemCount = await sourceQry.CountAsync();
            var pageCount = (int)Math.Ceiling(itemCount / (double)filter.RowsPerPage);
            var pageIndex = filter.Page > pageCount ? pageCount : filter.Page;

            if (pageIndex > 0)
            {
                qry = qry.Skip((pageIndex - 1) * filter.RowsPerPage)
                    .Take(filter.RowsPerPage);
            }
            else
            {
                qry = qry.Take(filter.RowsPerPage);
            }
            var rows = await qry.ToListAsync();

            return new PageWrapper<List<T>>
            {
                Data = rows,
                PageNumber = pageIndex,
                TotalPage = pageCount,
                ItemPerPage = filter.RowsPerPage,
                TotalItem = itemCount,
                SortBy = filter.SortBy,
                Descending = filter.Descending,
                SearchText = filter.SearchText
            };
        }
    }
}
