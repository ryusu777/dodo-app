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
        // TODO: Deleting empty transaction interface & implementation
        // TODO: Purchasing goods should check if fund is available
        // TODO: Implement receive date and purchase date
        Task<int> CreateTransactionHeaderAsync(
            GoodsTransactionHeader transactionHeader);
        Task<int> UpdateTransactionHeaderAsync(
            GoodsTransactionHeader transactionHeader);
        Task<HttpStatusCode> DeleteTransactionHeaderAsync(int id);
        Task<GoodsTransactionHeader> GetGoodsTransactionHeaderByIdAsync(int id);
        Task<PageWrapper<List<GoodsTransactionHeader>>> GetGoodsTransactionHeadersAsync(
            PageFilter pageFilter, FilterGoodsTransactionHeader filter);
        Task<int> CreateTransactionDetailAsync(
            GoodsTransactionDetail transactionDetail);
        Task<int> UpdateTransactionDetailAsync(
            GoodsTransactionDetail transactionDetail);
        Task<HttpStatusCode> DeleteTransactionDetail(int id);
    }
}