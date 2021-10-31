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

        public DodoAppContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goods>().ToTable<Goods>("Goods");
            modelBuilder.Entity<GoodsStock>().ToTable<GoodsStock>("GoodsStock");
        }
        public DbSet<Goods> Goods;
        public DbSet<GoodsStock> GoodsStocks;
    }
}