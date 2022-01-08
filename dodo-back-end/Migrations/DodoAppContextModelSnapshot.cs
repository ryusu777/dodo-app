﻿// <auto-generated />
using System;
using DodoApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DodoApp.Migrations
{
    [DbContext(typeof(DodoAppContext))]
    partial class DodoAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("DodoApp.Domain.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ChangeDescription")
                        .HasColumnType("longtext");

                    b.Property<int>("ChangingFundAmount")
                        .HasColumnType("int");

                    b.Property<int>("ChangingProfitAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfChange")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FundAmount")
                        .HasColumnType("int");

                    b.Property<int>("ProfitAmount")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionHeaderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionHeaderId");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChangeDescription = "Menjual Barang",
                            ChangingFundAmount = 110000,
                            ChangingProfitAmount = 40000,
                            DateOfChange = new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FundAmount = 110000,
                            ProfitAmount = 40000,
                            TransactionHeaderId = 1
                        },
                        new
                        {
                            Id = 2,
                            ChangeDescription = "Membeli Barang",
                            ChangingFundAmount = -90000,
                            ChangingProfitAmount = 0,
                            DateOfChange = new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FundAmount = 20000,
                            ProfitAmount = 40000,
                            TransactionHeaderId = 2
                        },
                        new
                        {
                            Id = 3,
                            ChangeDescription = "Bayar Listrik",
                            ChangingFundAmount = 0,
                            ChangingProfitAmount = -25000,
                            DateOfChange = new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FundAmount = 20000,
                            ProfitAmount = 15000
                        });
                });

            modelBuilder.Entity("DodoApp.Domain.Goods", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CarType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("GoodsCode")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("GoodsName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("MinimalAvailable")
                        .HasColumnType("int");

                    b.Property<string>("PartNumber")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("PurchasePrice")
                        .HasColumnType("int");

                    b.Property<int>("StockAvailable")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GoodsCode")
                        .IsUnique();

                    b.ToTable("Goods");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CarType = "S89",
                            GoodsCode = "J001",
                            GoodsName = "Join Kopel",
                            MinimalAvailable = 3,
                            PartNumber = "GUD88",
                            PurchasePrice = 70000,
                            StockAvailable = 2
                        },
                        new
                        {
                            Id = 2,
                            CarType = "J98",
                            GoodsCode = "B001",
                            GoodsName = "Bearing",
                            MinimalAvailable = 10,
                            PartNumber = "BED002",
                            PurchasePrice = 20000,
                            StockAvailable = 11
                        },
                        new
                        {
                            Id = 3,
                            CarType = "ALL",
                            GoodsCode = "KR01",
                            GoodsName = "Kampas Rem",
                            MinimalAvailable = 10,
                            PartNumber = "-",
                            PurchasePrice = 20000,
                            StockAvailable = 7
                        });
                });

            modelBuilder.Entity("DodoApp.Domain.GoodsTransactionDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("GoodsAmount")
                        .HasColumnType("int");

                    b.Property<int>("GoodsId")
                        .HasColumnType("int");

                    b.Property<int>("GoodsTransactionHeaderId")
                        .HasColumnType("int");

                    b.Property<int>("PricePerItem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GoodsId");

                    b.HasIndex("GoodsTransactionHeaderId");

                    b.ToTable("GoodsTransactionsDetails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GoodsAmount = 2,
                            GoodsId = 2,
                            GoodsTransactionHeaderId = 1,
                            PricePerItem = 25000
                        },
                        new
                        {
                            Id = 2,
                            GoodsAmount = 1,
                            GoodsId = 1,
                            GoodsTransactionHeaderId = 1,
                            PricePerItem = 100000
                        },
                        new
                        {
                            Id = 3,
                            GoodsAmount = 1,
                            GoodsId = 3,
                            GoodsTransactionHeaderId = 2,
                            PricePerItem = 20000
                        },
                        new
                        {
                            Id = 4,
                            GoodsAmount = 1,
                            GoodsId = 1,
                            GoodsTransactionHeaderId = 2,
                            PricePerItem = 70000
                        });
                });

            modelBuilder.Entity("DodoApp.Domain.GoodsTransactionHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ReceiveDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Vendor")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("GoodsTransactionHeaders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2022, 1, 8, 14, 32, 41, 191, DateTimeKind.Local).AddTicks(4726),
                            PurchaseDate = new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReceiveDate = new DateTime(2021, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Dimas kanjeng",
                            TransactionType = "sell"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2022, 1, 8, 14, 32, 41, 192, DateTimeKind.Local).AddTicks(2349),
                            PurchaseDate = new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReceiveDate = new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Ita jaya motor",
                            TransactionType = "purchase"
                        });
                });

            modelBuilder.Entity("DodoApp.Domain.Currency", b =>
                {
                    b.HasOne("DodoApp.Domain.GoodsTransactionHeader", "TheGoodsTransactionHeader")
                        .WithMany()
                        .HasForeignKey("TransactionHeaderId");

                    b.Navigation("TheGoodsTransactionHeader");
                });

            modelBuilder.Entity("DodoApp.Domain.GoodsTransactionDetail", b =>
                {
                    b.HasOne("DodoApp.Domain.Goods", "TheGoods")
                        .WithMany("GoodsTransactionDetails")
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DodoApp.Domain.GoodsTransactionHeader", "TheGoodsTransactionHeader")
                        .WithMany("GoodsTransactionDetails")
                        .HasForeignKey("GoodsTransactionHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TheGoods");

                    b.Navigation("TheGoodsTransactionHeader");
                });

            modelBuilder.Entity("DodoApp.Domain.Goods", b =>
                {
                    b.Navigation("GoodsTransactionDetails");
                });

            modelBuilder.Entity("DodoApp.Domain.GoodsTransactionHeader", b =>
                {
                    b.Navigation("GoodsTransactionDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
