using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DodoApp.Domain
{
    public class GoodsTransactionHeader
    {
        [Key]
        public int Id { get; set; }
        // TODO: Purchase and Receive date can be null
        public DateTime PurchaseDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [StringLength(255)]
        public string Vendor { get; set; }
        [StringLength(10)]
        [Required]
        public string TransactionType { get; set; }

        [InverseProperty(nameof(GoodsTransactionDetail.TheGoodsTransactionHeader))]
        public virtual ICollection<GoodsTransactionDetail> GoodsTransactionDetails{ get; set;}
    }
}