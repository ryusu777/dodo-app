using System;

namespace DodoApp.Contracts.V1.Responses
{
    public class ReadCurrencyDto
    {
        public int Id { get; set; }
        public int? TransactionHeaderId { get; set; }
        public int CurrencyAmount { get; set; }
        public int ChangingAmount { get; set; }
        public DateTime? DateOfChange { get; set; }
        public string ChangeDescription { get; set; }
        public ReadGoodsTransactionHeaderDto TheGoodsTransactionHeader { get; set; }
    }
}