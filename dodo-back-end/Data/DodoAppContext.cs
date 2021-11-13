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
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<GoodsTransactionDetail> GoodsTransactionsDetails { get; set; }
        public DbSet<GoodsTransactionHeader> GoodsTransactionHeaders { get; set; }
    }
}