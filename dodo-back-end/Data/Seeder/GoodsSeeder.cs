using DodoApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace DodoApp.Data.Seeder
{
    public static class GoodsSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Goods>().HasData(
                new Goods
                {
                    Id = 1,
                    GoodsName = "Join Kopel",
                    GoodsCode = "J001",
                    CarType = "S89",
                    PartNumber = "GUD88",
                    MinimalAvailable = 3,
                    PurchasePrice = 70000,
                    StockAvailable = 2
                },
                new Goods
                {
                    Id = 2,
                    GoodsName = "Bearing",
                    GoodsCode = "B001",
                    CarType = "J98",
                    PartNumber = "BED002",
                    MinimalAvailable = 10,
                    PurchasePrice = 20000,
                    StockAvailable = 11
                },
                new Goods
                {
                    Id = 3,
                    GoodsName = "Kampas Rem",
                    GoodsCode = "KR01",
                    CarType = "ALL",
                    PartNumber = "-",
                    MinimalAvailable = 10,
                    PurchasePrice = 20000,
                    StockAvailable = 7
                });
        }
    }
}