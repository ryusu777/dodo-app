using System.Collections.Generic;
using System.Threading.Tasks;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;

namespace DodoApp.Repository
{
    public interface IGoodsRepo
    {
        Task<PageWrapper<List<Goods>>> GetGoods(PageFilter pageFilter);
        Task<bool> CreateGoods(Goods request);
        Task<bool> UpdateGoods(int id, Goods request);
        Task<bool> DeleteGoods(int id);
    }
}