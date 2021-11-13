using System.ComponentModel.DataAnnotations;

namespace DodoApp.Contracts.V1.Requests
{
    public class UpdateGoodsTransactionDetailDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int GoodsTransactionHeaderId { get; set; }
        [Required]
        public int GoodsId { get; set; }
        [Required]
        public int GoodsAmount { get; set; }
        [Required]
        public int PricePerItem { get; set; }
    }
}