using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DodoApp.Domain
{
    public class GoodsStock
    {
        [Key]
        public int Id { get; set; }
        public int GoodsId { get; set; }
        public int AmountAvailable { get; set; }
        public string Vendor { get; set; }
        public int PurchasePricePerItem { get; set; }
        public int SellPricePerItem { get; set; }

        [ForeignKey("GoodsId")]
        public Goods Goods { get; set; }
    }
}