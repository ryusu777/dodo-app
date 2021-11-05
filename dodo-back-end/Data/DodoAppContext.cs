using DodoApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace DodoApp.Data
{
    public class DodoAppContext : DbContext
    {
        public DodoAppContext(DbContextOptions<DodoAppContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Goods>();
            modelBuilder.Entity<GoodsStock>();
        }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<GoodsStock> GoodsStocks { get; set; }
    }
}