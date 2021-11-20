using System;

namespace DodoApp.Contracts.V1.Requests
{
    public class FilterGoodsTransactionHeader
    {
        public DateTime? PurchaseDateFrom { get; set; }
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? PurchaseDateTo { get; set; }
        public DateTime? ReceiveDateTo { get; set; }
    }
}