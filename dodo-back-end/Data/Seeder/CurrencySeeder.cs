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
                    ProfitAmount = 40000,
                    FundAmount = 110000,
                    ChangingFundAmount = 110000,
                    ChangingProfitAmount = 40000,
                    TransactionHeaderId = 1
                },
                new Currency
                {
                    Id = 2,
                    ChangeDescription = "Membeli Barang",
                    DateOfChange = new DateTime(2021, 11, 1),
                    ChangingFundAmount = -90000,
                    ChangingProfitAmount = 0,
                    FundAmount = 20000,
                    ProfitAmount = 40000,
                    TransactionHeaderId = 2
                },
                new Currency
                {
                    Id = 3,
                    ChangeDescription = "Bayar Listrik",
                    DateOfChange = new DateTime(2021, 11, 25),
                    ChangingProfitAmount = -25000,
                    ChangingFundAmount = 0,
                    FundAmount = 20000,
                    ProfitAmount = 15000
                }
            );
        }
    }
}