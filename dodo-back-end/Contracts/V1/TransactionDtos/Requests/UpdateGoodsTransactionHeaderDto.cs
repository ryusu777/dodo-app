using System;
using System.ComponentModel.DataAnnotations;

namespace DodoApp.Contracts.V1.Requests
{
    public class UpdateGoodsTransactionHeaderDto
    {

        [Required]
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public int TotalPrice { get; set; }
        public string Vendor { get; set; }
        [Required]
        public string TransactionType { get; set; }
    }
}