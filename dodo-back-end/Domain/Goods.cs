using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DodoApp.Domain
{
    [Index(nameof(GoodsCode), IsUnique = true)]
    public class Goods
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string GoodsName { get; set; }
        [Required]
        [StringLength(256)]
        public string GoodsCode { get; set; }
        [Required]
        [StringLength(256)]
        public string CarType { get; set; }
        [Required]
        [StringLength(256)]
        public string PartNumber { get; set; }
        [Required]
        public int StockAvailable { get; set; }
        [Required]
        public int PurchasePrice { get; set; }
        [Required]
        public int MinimalAvailable { get; set; }
    }
}