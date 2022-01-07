using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;
using DodoApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DodoApp.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        // TODO: Converting profit to fund
        private readonly ICurrencyRepo _repo;
        private readonly IMapper _mapper;

        public CurrencyController(ICurrencyRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCurrencyReport(
            CreateCurrencyDto request)
        {
            return await _repo
                .CreateCurrencyReportAsync(request);
        }

        [HttpGet]
        public async Task<ActionResult<PageWrapper<List<ReadCurrencyDto>>>> GetCurrencies(
            [FromQuery] PageFilter pageFilter
        )
        {
            return await _repo.GetCurrenciesAsync(pageFilter);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<List<ReadCurrencyDto>>> GetSummary(
            [FromQuery] GetCurrencySummaryDto request)
        {
            return await _repo.GetSummaryAsync(request);
        }
    }
}