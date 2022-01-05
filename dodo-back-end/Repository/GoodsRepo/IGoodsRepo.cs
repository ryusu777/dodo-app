using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DodoApp.Repository
{
    public interface IGoodsRepo
    {
        Task<ActionResult<PageWrapper<List<Goods>>>> GetGoodsAsync(PageFilter pageFilter);
        Task<ActionResult<ReadGoodsDto>> GetGoodsByIdAsync(int id);
        Task<IActionResult> CreateGoodsAsync(CreateGoodsDto request);
        Task<IActionResult> UpdateGoodsAsync(int id, UpdateGoodsDto request);
        Task<IActionResult> DeleteGoodsAsync(int id);
    }
}