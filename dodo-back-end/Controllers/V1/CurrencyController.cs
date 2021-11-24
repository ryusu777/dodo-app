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
            var result = await _repo
                .CreateCurrencyReportAsync(_mapper.Map<Currency>(request));
            
            if (result == -1)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            if (result == -2)
            {
                return NotFound(new { errors = new string[] 
                    { "Transaction header not found"}});
            }

            if (result == -3)
            {
                return Conflict(new { errors = new string[]
                    { "Currency report already exists"}});
            }

            return Ok(new { id = result });
        }

        [HttpGet]
        public async Task<ActionResult<PageWrapper<List<ReadCurrencyDto>>>> GetCurrencies(
            [FromQuery] PageFilter pageFilter
        )
        {
            var result = await _repo.GetCurrenciesAsync(pageFilter);

            return Ok(_mapper.Map<PageWrapper<List<ReadCurrencyDto>>>(result));
        }
    }
}