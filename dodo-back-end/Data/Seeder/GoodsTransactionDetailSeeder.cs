using DodoApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace DodoApp.Data.Seeder
{
    public static class GoodsTransactionDetailSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<GoodsTransactionDetail>().HasData(
                new GoodsTransactionDetail
                {
                    Id = 1,
                    GoodsAmount = 2,
                    GoodsTransactionHeaderId = 1,
                    PricePerItem = 25000,
                    GoodsId = 2,
                },
                new GoodsTransactionDetail
                {
                    Id = 2,
                    GoodsAmount = 1,
                    GoodsTransactionHeaderId = 1,
                    PricePerItem = 100000,
                    GoodsId = 1,
                },
                new GoodsTransactionDetail
                {
                    Id = 3,
                    GoodsAmount = 1,
                    GoodsTransactionHeaderId = 2,
                    PricePerItem = 20000,
                    GoodsId = 3,
                },
                new GoodsTransactionDetail
                {
                    Id = 4,
                    GoodsAmount = 1,
                    GoodsTransactionHeaderId = 2,
                    PricePerItem = 70000,
                    GoodsId = 1,
                }
            );
        }
    }
}