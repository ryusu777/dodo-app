using System;
using System.ComponentModel.DataAnnotations;

namespace DodoApp.Contracts.V1.Requests
{
    public class CreateGoodsTransactionHeaderDto
    {
        // TODO: Transaction Title
        public string Vendor { get; set; }
        [Required]
        [RegularExpression("sell|purchase", 
            ErrorMessage = "Invalid Transaction Type")]
        public string TransactionType { get; set; }
    }
}