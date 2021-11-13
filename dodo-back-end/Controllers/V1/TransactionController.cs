using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DodoApp.Contracts.V1;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;
using DodoApp.Helpers;
using DodoApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DodoApp.Controllers.V1
{
    [Route("/api/v1/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IMapper _mapper;

        public TransactionController(
            ITransactionRepo transactionRepo, 
            IMapper mapper)
        {
            _transactionRepo = transactionRepo;
            _mapper = mapper;
        }

        [HttpPost("header")]
        public async Task<ActionResult<int>> CreateGoodsTransactionHeader(
            CreateGoodsTransactionHeaderDto request)
        {
            var result = await _transactionRepo
                .CreateTransactionHeaderAsync(
                    _mapper.Map<GoodsTransactionHeader>(request)
                );
            if (result == -1)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return CreatedAtAction("GetGoodsTransactionHeader", new 
                { id = result }
            );
        }

        [HttpGet("header")]
        public async Task<ActionResult<PageWrapper<List<GoodsTransactionHeader>>>> GetGoodsTransactionHeaders(
            [FromQuery]PageFilter pageFilter)
        {
            return Ok(await _transactionRepo
                .GetGoodsTransactionHeadersAsync(pageFilter));
        }

        [HttpGet("header/{id}")]
        public async Task<ActionResult<ReadGoodsTransactionHeaderDto>> GetGoodsTransactionHeaderById(
            int id)
        {
            var result = await _transactionRepo
                .GetGoodsTransactionHeaderByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReadGoodsTransactionHeaderDto>(result));
        }

        [HttpPut("header/{id}")]
        public async Task<IActionResult> UpdateGoodsTransactionHeader(
            int id, 
            [FromBody]UpdateGoodsTransactionHeaderDto request)
        {
            if (id != request.Id)
            {
                return BadRequest(new { errors = new string[] 
                    { "Id doesn't match "}}
                );
            }

            var result = await _transactionRepo.UpdateTransactionHeaderAsync(
                _mapper.Map<GoodsTransactionHeader>(request)
            );

            return StatusCode((int)result);
        }

        [HttpDelete("header/{id}")]
        public async Task<IActionResult> DeleteGoodsTransactionHeader(int id)
        {
            var result = await _transactionRepo
                .DeleteTransactionHeaderAsync(id);
            
            return StatusCode((int)result);
        }

        [HttpPost("detail")]
        public async Task<IActionResult> CreateGoodsTransactionDetail(
            CreateGoodsTransactionDetailDto request)
        {
            var result = await _transactionRepo
                .CreateTransactionDetailAsync(
                    _mapper.Map<GoodsTransactionDetail>(request)
                );
            if (result == -1)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return CreatedAtAction("GetGoodsTransactionHeader", new 
                { id = result }
            );

        }

        [HttpPut("detail/{id}")]
        public async Task<IActionResult> UpdateGoodsTransactionDetail(
            int id, UpdateGoodsTransactionDetailDto request
        )
        {
            if (id != request.Id)
            {
                return BadRequest(new { errors = new string[] 
                    { "Id doesn't match "}}
                );
            }

            var result = await _transactionRepo.UpdateTransactionDetailAsync(
                _mapper.Map<GoodsTransactionDetail>(request)
            );

            return StatusCode((int)result);
        }

        [HttpDelete("detail/{id}")]
        public async Task<IActionResult> DeleteGoodsTransactionDetail(int id)
        {
            var result = await _transactionRepo
                .DeleteTransactionHeaderAsync(id);
            
            return StatusCode((int)result);
        }
    }
}