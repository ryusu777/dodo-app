using System;
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
    // TODO: More efficient at getting transaction data
    [Route("/api/v1/[controller]")]
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
        public async Task<IActionResult> CreateGoodsTransactionHeader(
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

            return StatusCode((int)HttpStatusCode.Created, new { id = result });
        }

        [HttpPost("header-filter")]
        public async Task<ActionResult<PageWrapper<List<ReadGoodsTransactionHeaderDto>>>> FilterGoodsHeader(
            [FromQuery] PageFilter pageFilter, 
            [FromBody] FilterGoodsTransactionHeader filter)
        {
            return Ok(_mapper.Map<PageWrapper<List<ReadGoodsTransactionHeaderDto>>>(
                await _transactionRepo
                .GetGoodsTransactionHeadersAsync(pageFilter, filter)));
        }

        [HttpGet("header")]
        public async Task<ActionResult<PageWrapper<List<ReadGoodsTransactionHeaderDto>>>> GetGoodsTransactionHeaders(
            [FromQuery]PageFilter pageFilter)
        {
            return Ok(_mapper.Map<PageWrapper<List<ReadGoodsTransactionHeaderDto>>>(
                await _transactionRepo
                .GetGoodsTransactionHeadersAsync(pageFilter, null)));

        }

        [HttpGet("header/{id}")]
        public async Task<ActionResult<ReadGoodsTransactionHeaderDto>> GetGoodsTransactionHeaderById(int id)
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

            if (result == -1)
            {
                return NotFound(new { errors = new string[] {
                    "Transaction header not found"
                }});
            }

            if (result == -2)
            {
                return BadRequest(new { errors = new string[] {
                    "One of the goods stock is not enough"
                }});
            }

            if (result == -3 )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return NoContent();
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
            else if (result == -2)
            {
                return BadRequest(new { errors = new string[] 
                    { "Transaction Header doesn't exists" }});
            }
            else if (result == -3)
            {
                return BadRequest(new { errors = new string[] 
                    { "Goods doesn't exists" }});
            }
            else if (result == -4)
            {
                return BadRequest(new { errors = new string[] 
                    { "Transaction Detail already exists" }});
            }
            else if (result == -5)
            {
                return BadRequest(new { errors = new string[] 
                    { "Goods available stock is not enough" }});
            }

            return StatusCode((int)HttpStatusCode.Created, new { id = result });
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
                .DeleteTransactionDetail(id);
            
            return StatusCode((int)result);
        }
    }
}