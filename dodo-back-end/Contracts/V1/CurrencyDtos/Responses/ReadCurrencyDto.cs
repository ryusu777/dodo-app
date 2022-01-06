using System;

namespace DodoApp.Contracts.V1.Responses
{
    public class ReadCurrencyDto
    {
        public int Id { get; set; }
        public int? TransactionHeaderId { get; set; }
        public int FundAmount { get; set; }
        public int ProfitAmount { get; set; }
        public int ChangingProfitAmount { get; set; }
        public int ChangingFundAmount { get; set; }
        public DateTime? DateOfChange { get; set; }
        public string ChangeDescription { get; set; }
        public ReadGoodsTransactionHeaderDto TheGoodsTransactionHeader { get; set; }
    }
}