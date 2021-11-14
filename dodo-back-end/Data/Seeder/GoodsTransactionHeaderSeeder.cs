using System;
using DodoApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace DodoApp.Data.Seeder
{
    public static class GoodsTransactionHeaderSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<GoodsTransactionHeader>().HasData(
                new GoodsTransactionHeader
                {
                    Id = 1,
                    PurchaseDate = new DateTime(2021, 11, 1),
                    ReceiveDate = new DateTime(2021, 11, 2),
                    CreatedDate = DateTime.Now,
                    TransactionType = "sell",
                },
                new GoodsTransactionHeader
                {
                    Id = 2,
                    PurchaseDate = new DateTime(2021, 11, 3),
                    ReceiveDate = new DateTime(2021, 11, 3),
                    CreatedDate = DateTime.Now,
                    TransactionType = "purchase",
                }
            );
        }
    }
}