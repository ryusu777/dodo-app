using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DodoApp.Contracts.V1.Requests;
using DodoApp.Contracts.V1.Responses;
using DodoApp.Domain;

namespace DodoApp.Repository
{
    public interface ICurrencyRepo
    {
        Task<int> CreateCurrencyReportAsync(Currency request);
        Task<PageWrapper<List<Currency>>> GetCurrenciesAsync(
            PageFilter pageFilter);
    }
}