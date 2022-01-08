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
using System;

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

        public async Task<IActionResult> CreateCurrencyReportAsync(
            CreateCurrencyDto request)
        {
            if (request.TransactionHeaderId != null && await _context.Currencies
                .FirstOrDefaultAsync(c => 
                    c.TransactionHeaderId == 
                        request.TransactionHeaderId) != null)
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
                    .ThenInclude(d => d.TheGoods)
                    .FirstOrDefaultAsync(h => 
                        h.Id == currency.TransactionHeaderId);

                if (header == null)
                    return new BadRequestObjectResult(new 
                    { 
                        errors = new string[] 
                        { "Transaksi barang tidak ditemukan" }
                    });
                
                if (header.TransactionType == "purchase") 
                {
                    currency.ChangingFundAmount = -1 * _mapper
                    .Map<ReadGoodsTransactionHeaderDto>(header).TotalPrice;

                    currency.ChangingProfitAmount = 0;
                }
                else
                {
                    currency.ChangingFundAmount = 0;
                    currency.ChangingProfitAmount = 0;
                    foreach (var detail in header.GoodsTransactionDetails)
                    {
                        currency.ChangingProfitAmount += 
                            (detail.PricePerItem - detail.TheGoods.PurchasePrice) * detail.GoodsAmount;
                        currency.ChangingFundAmount += 
                            detail.TheGoods.PurchasePrice * detail.GoodsAmount;
                    }
                }
            }

            var latestCurrency = await _context.Currencies
                .OrderBy(c => c.Id)
                .LastOrDefaultAsync();
            var latestProfitAmount = (int?)latestCurrency.ProfitAmount ?? 0;
            var latestFundAmount = (int?)latestCurrency.FundAmount ?? 0;

            currency.ProfitAmount = 
                latestProfitAmount + currency.ChangingProfitAmount;

            currency.FundAmount = latestFundAmount + currency.ChangingFundAmount;

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
                ChangingFundAmount = s.ChangingFundAmount,
                ChangingProfitAmount = s.ChangingProfitAmount,
                ProfitAmount = s.ProfitAmount,
                FundAmount = s.FundAmount,
                DateOfChange = s.DateOfChange,
                TransactionHeaderId = s.TransactionHeaderId
            });

            return new OkObjectResult(await Pagination<ReadCurrencyDto>.LoadPageAsync(qry, validPageFilter));
        }

        public async Task<IActionResult> GetSummaryAsync(GetCurrencySummaryDto request)
        {
            DateTime dateFrom = (DateTime)request.DateFrom;
            DateTime? dateTo = request.DateTo;
            
            if (dateTo != null) {
                dateTo = dateTo?.AddDays(1);
                var currencies = await _context.Currencies
                    .Where(c => 
                        c.DateOfChange >= dateFrom && 
                        c.DateOfChange < dateTo
                    )
                    .ToListAsync();
                
                var groupedCurrencies = currencies
                    .GroupBy(c => c.DateOfChange.Date)
                    .Select(c => new ReadCurrencySummaryPerDayDto
                    {
                        Day = c.First().DateOfChange.Date,
                        TotalFundChange = c.Aggregate(0, (sum, val) => sum + val.ChangingFundAmount),
                        TotalProfitChange = c.Aggregate(0, (sum, val) => sum + val.ChangingProfitAmount),
                        ProfitAmount = c.Last().ProfitAmount,
                        FundAmount = c.Last().FundAmount
                    })
                    .ToList();
                return new OkObjectResult(groupedCurrencies);
            }
            else
            {
                var currencies = await _context.Currencies
                    .Where(c => 
                        c.DateOfChange >= dateFrom && 
                        c.DateOfChange < dateFrom.AddDays(1)
                    )
                    .OrderBy(c => c.DateOfChange)
                    .Select(c => new ReadCurrencyDto
                    {
                        Id = c.Id,
                        ChangeDescription = c.ChangeDescription,
                        ChangingFundAmount = c.FundAmount,
                        ChangingProfitAmount = c.ChangingProfitAmount,
                        DateOfChange = c.DateOfChange,
                        FundAmount = c.FundAmount,
                        ProfitAmount = c.ProfitAmount,
                        TransactionHeaderId = c.TransactionHeaderId
                    })
                    .ToListAsync();
                return new OkObjectResult(currencies);
            }
        }
    }
}