using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DodoApp.Domain
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        public int? TransactionHeaderId { get; set; }
        [Required]
        public int ProfitAmount { get; set; }
        [Required]
        public int FundAmount { get; set; }
        [Required]
        public int ChangingProfitAmount { get; set; }
        [Required]
        public int ChangingFundAmount { get; set; }
        [Required]
        public DateTime DateOfChange { get; set; }
        public string ChangeDescription { get; set; }

        [ForeignKey(nameof(TransactionHeaderId))]
        public virtual GoodsTransactionHeader TheGoodsTransactionHeader { get; set; }
    }
}