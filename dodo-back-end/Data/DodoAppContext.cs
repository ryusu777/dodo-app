using DodoApp.Data.Seeder;
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
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            GoodsSeeder.Seed(builder);
            GoodsTransactionDetailSeeder.Seed(builder);
            GoodsTransactionHeaderSeeder.Seed(builder);
            CurrencySeeder.Seed(builder);
        }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<GoodsTransactionDetail> GoodsTransactionsDetails { get; set; }
        public DbSet<GoodsTransactionHeader> GoodsTransactionHeaders { get; set; }
    }
}