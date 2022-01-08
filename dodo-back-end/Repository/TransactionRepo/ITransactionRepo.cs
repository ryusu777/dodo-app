using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DodoApp.Repository
{
    public interface ITransactionRepo
    {
        // TODO: Purchasing goods should check if fund is available
        // TODO: Implement receive date and purchase date
        Task<IActionResult> CreateTransactionHeaderAsync(
            CreateGoodsTransactionHeaderDto request);
        Task<IActionResult> UpdateTransactionHeaderAsync(
            UpdateGoodsTransactionHeaderDto request);
        Task<IActionResult> DeleteTransactionHeaderAsync(int id);
        Task<ActionResult<ReadGoodsTransactionHeaderDto>> GetGoodsTransactionHeaderByIdAsync(int id);
        Task<ActionResult<PageWrapper<List<ReadGoodsTransactionHeaderDto>>>> GetGoodsTransactionHeadersAsync(
            PageFilter pageFilter, FilterGoodsTransactionHeader filter);
        Task<IActionResult> CreateTransactionDetailAsync(
            CreateGoodsTransactionDetailDto request);
        Task<IActionResult> UpdateTransactionDetailAsync(
            UpdateGoodsTransactionDetailDto request);
        Task<IActionResult> DeleteTransactionDetail(int id);
    }
}