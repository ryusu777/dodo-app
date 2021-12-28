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
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> CreateCurrencyReportAsync(CreateCurrencyDto request)
        {
            if (request.TransactionHeaderId != null && await _context.Currencies
                .FirstOrDefaultAsync(c => 
                    c.TransactionHeaderId == request.TransactionHeaderId) != null)
            {
                return new BadRequestObjectResult(new { errors = new string[] 
                    { "Record keuangan sudah ada" }});
            }

            GoodsTransactionHeader header;
            var currency = _mapper.Map<Currency>(request);
            if (currency.TransactionHeaderId != null)
            {
                header = await _context.GoodsTransactionHeaders
                    .Include(h => h.GoodsTransactionDetails)
                    .FirstOrDefaultAsync(h => 
                        h.Id == currency.TransactionHeaderId);

                if (header == null)
                    return new BadRequestObjectResult(new 
                    { 
                        errors = new string[] { "Transaksi barang tidak ditemukan" }
                    });
                
                currency.ChangingAmount = _mapper
                    .Map<ReadGoodsTransactionHeaderDto>(header).TotalPrice;
                
                if (header.TransactionType == "purchase")
                    currency.ChangingAmount *= -1;
            }

            var latestCurrency = await _context.Currencies
                .OrderBy(c => c.Id)
                .LastOrDefaultAsync();
            var latestCurrencyAmount = (int?)latestCurrency.CurrencyAmount ?? 0;

            currency.CurrencyAmount = 
                latestCurrencyAmount + currency.ChangingAmount;
            await _context.Currencies.AddAsync(currency);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            return new OkObjectResult(new { id = currency.Id });
        }

        public async Task<ActionResult<PageWrapper<List<ReadCurrencyDto>>>> GetCurrenciesAsync(PageFilter pageFilter)
        {
            var validPageFilter = new PageFilter(
                pageFilter.Page,
                pageFilter.RowsPerPage, 
                pageFilter.SortBy, 
                pageFilter.Descending, 
                pageFilter.SearchText);

            var qry = _context.Currencies.AsQueryable().Select(s => new ReadCurrencyDto
            {
                Id = s.Id,
                ChangeDescription = s.ChangeDescription,
                ChangingAmount = s.ChangingAmount,
                CurrencyAmount = s.CurrencyAmount,
                DateOfChange = s.DateOfChange,
                TransactionHeaderId = s.TransactionHeaderId
            });

            return new OkObjectResult(await Pagination<ReadCurrencyDto>.LoadPageAsync(qry, validPageFilter));
        }
    }
}