using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DodoApp.Domain;
using DodoApp.Repository;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using AutoMapper;
using System.Net;

namespace DodoApp.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
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
            return await _goodsRepo.GetGoodsByIdAsync(id);
        }

        // PUT: api/Goods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoods(int id, UpdateGoodsDto goods)
        {
            if (id != goods.Id)
            {
                return BadRequest(new { errors = new string[] { "Permintaan bermasalah" }});
            }

            return await _goodsRepo.UpdateGoodsAsync(id, goods);
        }

        // POST: api/Goods
        [HttpPost]
        public async Task<IActionResult> PostGoods(CreateGoodsDto goods)
        {
            return await _goodsRepo.CreateGoodsAsync(goods);
        }

        // DELETE: api/Goods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoods(int id)
        {
            return await _goodsRepo.DeleteGoodsAsync(id);
        }
    }
}
