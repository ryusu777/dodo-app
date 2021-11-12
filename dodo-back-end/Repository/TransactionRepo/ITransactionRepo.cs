using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;

namespace DodoApp.Repository
{
    public interface ITransactionRepo
    {
        Task<int> CreateTransactionHeaderAsync(GoodsTransactionHeader transactionHeader);
        Task<HttpStatusCode> UpdateTransactionHeaderAsync(GoodsTransactionHeader transactionHeader);
        Task<HttpStatusCode> DeleteTransactionHeaderAsync(int id);
        Task<GoodsTransactionHeader> GetGoodsTransactionHeaderByIdAsync(int id);
        Task<PageWrapper<List<GoodsTransactionHeader>>> GetGoodsTransactionHeadersAsync(PageFilter pageFilter);
        Task<int> CreateTransactionDetail(GoodsTransactionDetail transactionDetail);
        Task<HttpStatusCode> UpdateTransactionDetail(GoodsTransactionDetail transactionDetail);
        Task<HttpStatusCode> DeleteTransactionDetail(int id);
        Task<List<GoodsTransactionDetail>> GetGoodsTransactionDetails(int headerId);
    }
}