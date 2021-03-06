using System;
using System.ComponentModel.DataAnnotations;

namespace DodoApp.Contracts.V1.Requests
{
    public class UpdateGoodsTransactionHeaderDto
    {
        // TODO: Transaction Title

        [Required]
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Vendor { get; set; }
        [Required]
        [RegularExpression("sell|purchase", 
            ErrorMessage = "Invalid Transaction Type")]
        public string TransactionType { get; set; }
    }
}