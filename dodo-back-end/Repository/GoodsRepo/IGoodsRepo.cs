using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;

namespace DodoApp.Repository
{
    public interface IGoodsRepo
    {
        Task<PageWrapper<List<Goods>>> GetGoodsAsync(PageFilter pageFilter);
        Task<Goods> GetGoodsByIdAsync(int id);
        Task<int> CreateGoodsAsync(Goods request);
        Task<HttpStatusCode> UpdateGoodsAsync(int id, Goods request);
        Task<HttpStatusCode> DeleteGoodsAsync(int id);
    }
}