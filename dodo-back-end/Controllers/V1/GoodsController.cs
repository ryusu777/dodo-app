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

namespace DodoApp.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly DodoAppContext _context;
        private readonly IGoodsRepo _goodsRepo;

        public GoodsController(DodoAppContext context, IGoodsRepo goodsRepo)
        {
            _context = context;
            _goodsRepo = goodsRepo;
        }

        // GET: api/Goods
        [HttpGet]
        public async Task<ActionResult<PageWrapper<List<Goods>>>> GetGoods([FromQuery] PageFilter pageFilter)
        {
            if (pageFilter == null)
                pageFilter = new PageFilter();

            return await _goodsRepo.GetGoods(pageFilter);
        }

        // GET: api/Goods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Goods>> GetGoods(int id)
        {
            var goods = await _context.Goods.FindAsync(id);

            if (goods == null)
            {
                return NotFound();
            }

            return goods;
        }

        // PUT: api/Goods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoods(int id, Goods goods)
        {
            if (id != goods.Id)
            {
                return BadRequest();
            }

            _context.Entry(goods).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Goods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Goods>> PostGoods(Goods goods)
        {
            _context.Goods.Add(goods);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoods", new { id = goods.Id }, goods);
        }

        // DELETE: api/Goods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoods(int id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods == null)
            {
                return NotFound();
            }

            _context.Goods.Remove(goods);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GoodsExists(int id)
        {
            return _context.Goods.Any(e => e.Id == id);
        }
    }
}
