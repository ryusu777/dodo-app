using System;
using DodoApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace DodoApp.Data.Seeder
{
    public static class CurrencySeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Currency>().HasData(
                new Currency 
                {
                    Id = 1,
                    ChangeDescription = "Menjual Barang",
                    DateOfChange = new DateTime(2021, 11, 1),
                    ChangingAmount = 220000,
                    CurrencyAmount = 500000,
                    TransactionHeaderId = 1
                },
                new Currency
                {
                    Id = 2,
                    ChangeDescription = "Membeli Barang",
                    DateOfChange = new DateTime(2021, 11, 5),
                    ChangingAmount = -60000,
                    CurrencyAmount = 440000,
                    TransactionHeaderId = 2
                },
                new Currency
                {
                    Id = 3,
                    ChangeDescription = "Bayar Listrik",
                    DateOfChange = new DateTime(2021, 11, 25),
                    ChangingAmount = -250000,
                    CurrencyAmount = 190000
                }
            );
        }
    }
}