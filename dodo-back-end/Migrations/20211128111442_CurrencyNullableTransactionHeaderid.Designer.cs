﻿// <auto-generated />
using System;
using DodoApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DodoApp.Migrations
{
    [DbContext(typeof(DodoAppContext))]
    [Migration("20211128111442_CurrencyNullableTransactionHeaderid")]
    partial class CurrencyNullableTransactionHeaderid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DodoApp.Domain.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChangeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChangingAmount")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfChange")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TransactionHeaderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionHeaderId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("DodoApp.Domain.Goods", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("GoodsCode")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("GoodsName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("MinimalAvailable")
                        .HasColumnType("int");

                    b.Property<string>("PartNumber")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                            GoodsAmount = 3,
                            GoodsId = 3,
                            GoodsTransactionHeaderId = 2,
                            PricePerItem = 20000
                        },
                        new
                        {
                            Id = 4,
                            GoodsAmount = 1,
                            GoodsId = 1,
                            GoodsTransactionHeaderId = 1,
                            PricePerItem = 70000
                        });
                });

            modelBuilder.Entity("DodoApp.Domain.GoodsTransactionHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReceiveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Vendor")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("GoodsTransactionHeaders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2021, 11, 28, 18, 14, 41, 947, DateTimeKind.Local).AddTicks(5252),
                            PurchaseDate = new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReceiveDate = new DateTime(2021, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransactionType = "sell"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2021, 11, 28, 18, 14, 41, 948, DateTimeKind.Local).AddTicks(2838),
                            PurchaseDate = new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReceiveDate = new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
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
