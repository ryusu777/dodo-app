using System;

namespace DodoApp.Contracts.V1.Requests
{
    public class CreateCurrencyDto
    {
        public int TransactionHeaderId { get; set; }
        public int CurrencyAmount { get; set; }
        public int ChangingAmount { get; set; }
        public DateTime? DateOfChange { get; set; }
        public string ChangeDescription { get; set; }
    }
}