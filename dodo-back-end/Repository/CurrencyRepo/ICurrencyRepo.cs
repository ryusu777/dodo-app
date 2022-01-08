using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DodoApp.Repository
{
    public interface ICurrencyRepo
    {
        // TODO: Convert profit to fund
        Task<IActionResult> CreateCurrencyReportAsync(CreateCurrencyDto request);
        Task<ActionResult<PageWrapper<List<ReadCurrencyDto>>>> GetCurrenciesAsync(
            PageFilter pageFilter);
        Task<IActionResult> GetSummaryAsync(
            GetCurrencySummaryDto request);
    }
}