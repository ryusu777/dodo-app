using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Data;
using DodoApp.Domain;
using DodoApp.Helpers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace DodoApp.Repository
{
    public class CurrencyRepo : ICurrencyRepo
    {
        private readonly DodoAppContext _context;
        private readonly IMapper _mapper;

        public CurrencyRepo(
            DodoAppContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /*
            Returns:
            -1 -> Internal server error
            -2 -> Transaction Header Not Found
            -3 -> Transaction Header Already Exists
        */
        public async Task<int> CreateCurrencyReportAsync(Currency request)
        {
            if (request.TransactionHeaderId != null && await _context.Currencies
                .FirstOrDefaultAsync(c => 
                    c.TransactionHeaderId == request.TransactionHeaderId) != null)
            {
                return -3;
            }

            GoodsTransactionHeader header;
            if (request.TransactionHeaderId != null)
            {
                header = await _context.GoodsTransactionHeaders
                    .Include(h => h.GoodsTransactionDetails)
                    .FirstOrDefaultAsync(h => 
                        h.Id == request.TransactionHeaderId);

                if (header == null)
                    return -2;
                
                request.ChangingAmount = _mapper
                    .Map<ReadGoodsTransactionHeaderDto>(header).TotalPrice;
                
                if (header.TransactionType == "purchase")
                    request.ChangingAmount *= -1;
            }

            var latestCurrency = await _context.Currencies
                .OrderBy(c => c.Id)
                .LastOrDefaultAsync();
            var latestCurrencyAmount = (int?)latestCurrency.CurrencyAmount ?? 0;

            request.CurrencyAmount = 
                latestCurrencyAmount + request.ChangingAmount;
            await _context.Currencies.AddAsync(request);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }

            return request.Id;
        }

        public async Task<PageWrapper<List<Currency>>> GetCurrenciesAsync(PageFilter pageFilter)
        {
            var validPageFilter = new PageFilter(
                pageFilter.Page,
                pageFilter.RowsPerPage, 
                pageFilter.SortBy, 
                pageFilter.Descending, 
                pageFilter.SearchText);

            var qry = _context.Currencies.AsQueryable();
            qry = qry.Select(s => new Currency {
                Id = s.Id,
                ChangeDescription = s.ChangeDescription,
                ChangingAmount = s.ChangingAmount,
                CurrencyAmount = s.CurrencyAmount,
                DateOfChange = s.DateOfChange,
                TransactionHeaderId = s.TransactionHeaderId
            });

            return await Pagination<Currency>.LoadPageAsync(qry, validPageFilter);
        }
    }
}