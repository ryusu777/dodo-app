using System;

namespace DodoApp.Contracts.V1.Requests
{
    public class CreateCurrencyDto
    {
        public int? TransactionHeaderId { get; set; }
        public int? ChangingProfitAmount { get; set; }
        public int? ChangingFundAmount { get; set; }
        public string ChangeDescription { get; set; }
    }
}