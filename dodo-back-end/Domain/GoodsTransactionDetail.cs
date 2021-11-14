using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DodoApp.Domain
{
    public class GoodsTransactionDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int GoodsTransactionHeaderId { get; set; }
        [Required]
        public int GoodsId { get; set; }
        public int GoodsAmount { get; set; }
        [Required]
        public int PricePerItem { get; set; }

        [ForeignKey(nameof(GoodsTransactionHeaderId))]
        [InverseProperty(nameof(GoodsTransactionHeader.GoodsTransactionDetails))]
        public virtual GoodsTransactionHeader TheGoodsTransactionHeader { get; set; }

        [ForeignKey(nameof(GoodsId))]
        [InverseProperty(nameof(Goods.GoodsTransactionDetails))]
        public virtual Goods TheGoods { get; set; }
    }
}