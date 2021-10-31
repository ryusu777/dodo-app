using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DodoApp.Domain
{
    [Index(nameof(Code), IsUnique = true)]
    public class Goods
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Category { get; set; }

        [InverseProperty("Goods")]
        public IEnumerable<GoodsStock> GoodsStock { get; set; }
    }
}