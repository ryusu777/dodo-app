
using System.ComponentModel.DataAnnotations;

namespace DodoApp.Contracts.V1.Requests
{
    public class UpdateGoodsDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string GoodsName { get; set; }
        [Required]
        public string GoodsCode { get; set; }
        [Required]
        public string CarType { get; set; }
        [Required]
        public string PartNumber { get; set; }
        [Required]
        public int PurchasePrice { get; set; }
        [Required]
        public int StockAvailable { get; set; }
        [Required]
        public int MinimalAvailable { get; set; }
    }
}