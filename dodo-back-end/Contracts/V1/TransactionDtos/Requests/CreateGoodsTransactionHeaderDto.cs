using System;

namespace DodoApp.Contracts.V1.Requests
{
    public class CreateGoodsTransactionHeaderDto
    {
        public DateTime PurchaseDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Vendor { get; set; }
        public string TransactionType { get; set; }

    }
}