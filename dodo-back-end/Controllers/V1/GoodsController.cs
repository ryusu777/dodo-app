using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DodoApp.Data;
using DodoApp.Domain;
using DodoApp.Repository;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using AutoMapper;
using System.Net;
using DodoApp.Helpers;

namespace DodoApp.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ModelValidation]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsRepo _goodsRepo;
        private readonly IMapper _mapper;

        public GoodsController(IGoodsRepo goodsRepo, IMapper mapper)
        {
            _goodsRepo = goodsRepo;
            _mapper = mapper;
        }

        // GET: api/Goods
        [HttpGet]
        public async Task<ActionResult<PageWrapper<List<Goods>>>> GetGoods([FromQuery]PageFilter pageFilter)
        {
            return await _goodsRepo.GetGoodsAsync(pageFilter);
        }

        // GET: api/Goods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadGoodsDto>> GetGoods(int id)
        {
            var goods = await _goodsRepo.GetGoodsByIdAsync(id);

            if (goods == null)
            {
                return NotFound();
            }

            return _mapper.Map<ReadGoodsDto>(goods);
        }

        // PUT: api/Goods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoods(int id, UpdateGoodsDto goods)
        {
            if (id != goods.Id)
            {
                return BadRequest(new { errors = new string[] { "Id doesn't match" }});
            }

            var result = await _goodsRepo.UpdateGoodsAsync(id, _mapper.Map<Goods>(goods));

            if (result == HttpStatusCode.Conflict)
            {
                return BadRequest(new { errors = new string[] {"Code already exists"} });
            }

            return StatusCode((int)result);
        }

        // POST: api/Goods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostGoods(CreateGoodsDto goods)
        {
            var result = await _goodsRepo.CreateGoodsAsync(_mapper.Map<Goods>(goods));

            if (result == -2)
            {
                return Conflict(new { errors = new string[] { "Code already exists" }});
            }
            else if (result == -1)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return CreatedAtAction("GetGoods", new { id = result });
        }

        // DELETE: api/Goods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoods(int id)
        {
            var result = await _goodsRepo.DeleteGoodsAsync(id);

            return StatusCode((int)result);
        }
    }
}
