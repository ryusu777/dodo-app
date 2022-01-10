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
    // TODO: Deleting empty transaction endpoint
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
            return await _transactionRepo
                .CreateTransactionHeaderAsync(request);
        }

        [HttpPost("header-filter")]
        public async Task<ActionResult<PageWrapper<List<ReadGoodsTransactionHeaderDto>>>> FilterGoodsHeader(
            [FromQuery] PageFilter pageFilter, 
            [FromBody] FilterGoodsTransactionHeader filter)
        {
            return await _transactionRepo.GetGoodsTransactionHeadersAsync(pageFilter, filter);
        }

        [HttpGet("header")]
        public async Task<ActionResult<PageWrapper<List<ReadGoodsTransactionHeaderDto>>>> GetGoodsTransactionHeaders(
            [FromQuery]PageFilter pageFilter)
        {
            return await _transactionRepo 
                .GetGoodsTransactionHeadersAsync(pageFilter, null);

        }

        [HttpGet("header/{id}")]
        public async Task<ActionResult<ReadGoodsTransactionHeaderDto>> GetGoodsTransactionHeaderById(int id)
        {
            return await _transactionRepo
                .GetGoodsTransactionHeaderByIdAsync(id);
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

            return await _transactionRepo.UpdateTransactionHeaderAsync(request);
        }

        [HttpDelete("header/{id}")]
        public async Task<IActionResult> DeleteGoodsTransactionHeader(int id)
        {
            return await _transactionRepo
                .DeleteTransactionHeaderAsync(id);
        }

        [HttpPost("detail")]
        public async Task<IActionResult> CreateGoodsTransactionDetail(
            CreateGoodsTransactionDetailDto request)
        {
            return await _transactionRepo
                .CreateTransactionDetailAsync(request);
        }

        [HttpPut("detail/{id}")]
        public async Task<IActionResult> UpdateGoodsTransactionDetail(
            int id, UpdateGoodsTransactionDetailDto request)
        {
            if (id != request.Id)
            {
                return BadRequest(new { errors = new string[] 
                    { "Id doesn't match "}}
                );
            }

            return await _transactionRepo.UpdateTransactionDetailAsync(request);
        }

        [HttpDelete("detail/{id}")]
        public async Task<IActionResult> DeleteGoodsTransactionDetail(int id)
        {
            return await _transactionRepo
                .DeleteTransactionDetail(id);
        }
    }
}