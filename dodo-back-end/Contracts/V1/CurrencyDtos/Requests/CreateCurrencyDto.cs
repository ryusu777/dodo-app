using System;

namespace DodoApp.Contracts.V1.Requests
{
    public class CreateCurrencyDto
    {
        // TODO: Profit or funding changes
        public int? TransactionHeaderId { get; set; }
        public int? ChangingAmount { get; set; }
        public DateTime? DateOfChange { get; set; }
        public string ChangeDescription { get; set; }
    }
}