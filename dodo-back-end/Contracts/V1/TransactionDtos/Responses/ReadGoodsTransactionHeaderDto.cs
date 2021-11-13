using System;
using System.Collections.Generic;

namespace DodoApp.Contracts.V1.Responses
{
    public class ReadGoodsTransactionHeaderDto
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalPrice { get; set; }
        public string Vendor { get; set; }
        public string TransactionType { get; set; }
        public List<ReadGoodsTransactionDetailDto> GoodsTransactionDetails { get; set; }
    }
}