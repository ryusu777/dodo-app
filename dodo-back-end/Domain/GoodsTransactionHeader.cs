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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PurchaseDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReceiveDate { get; set; }
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