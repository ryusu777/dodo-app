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
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        [StringLength(256)]
        public string Code { get; set; }
        [Required]
        [StringLength(256)]
        public string Category { get; set; }
        [Required]
        public int MinimalAvailable { get; set; }

        [InverseProperty("Goods")]
        public virtual IEnumerable<GoodsStock> GoodsStock { get; set; }
    }
}