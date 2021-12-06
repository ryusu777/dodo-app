using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DodoApp.Migrations
{
    public partial class MySqlInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GoodsName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsCode = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CarType = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PartNumber = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockAvailable = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<int>(type: "int", nullable: false),
                    MinimalAvailable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GoodsTransactionHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PurchaseDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ReceiveDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Vendor = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsTransactionHeaders", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransactionHeaderId = table.Column<int>(type: "int", nullable: true),
                    CurrencyAmount = table.Column<int>(type: "int", nullable: false),
                    ChangingAmount = table.Column<int>(type: "int", nullable: false),
                    DateOfChange = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ChangeDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_GoodsTransactionHeaders_TransactionHeaderId",
                        column: x => x.TransactionHeaderId,
                        principalTable: "GoodsTransactionHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GoodsTransactionsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GoodsTransactionHeaderId = table.Column<int>(type: "int", nullable: false),
                    GoodsId = table.Column<int>(type: "int", nullable: false),
                    GoodsAmount = table.Column<int>(type: "int", nullable: false),
                    PricePerItem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsTransactionsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsTransactionsDetails_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsTransactionsDetails_GoodsTransactionHeaders_GoodsTransa~",
                        column: x => x.GoodsTransactionHeaderId,
                        principalTable: "GoodsTransactionHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "ChangeDescription", "ChangingAmount", "CurrencyAmount", "DateOfChange", "TransactionHeaderId" },
                values: new object[] { 3, "Bayar Listrik", -250000, 190000, new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "CarType", "GoodsCode", "GoodsName", "MinimalAvailable", "PartNumber", "PurchasePrice", "StockAvailable" },
                values: new object[,]
                {
                    { 1, "S89", "J001", "Join Kopel", 3, "GUD88", 70000, 2 },
                    { 2, "J98", "B001", "Bearing", 10, "BED002", 20000, 11 },
                    { 3, "ALL", "KR01", "Kampas Rem", 10, "-", 20000, 7 }
                });

            migrationBuilder.InsertData(
                table: "GoodsTransactionHeaders",
                columns: new[] { "Id", "CreatedDate", "PurchaseDate", "ReceiveDate", "TransactionType", "Vendor" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 12, 5, 19, 25, 35, 647, DateTimeKind.Local).AddTicks(5972), new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "sell", null },
                    { 2, new DateTime(2021, 12, 5, 19, 25, 35, 648, DateTimeKind.Local).AddTicks(3512), new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "purchase", null }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "ChangeDescription", "ChangingAmount", "CurrencyAmount", "DateOfChange", "TransactionHeaderId" },
                values: new object[,]
                {
                    { 1, "Menjual Barang", 220000, 500000, new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Membeli Barang", -60000, 440000, new DateTime(2021, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "GoodsTransactionsDetails",
                columns: new[] { "Id", "GoodsAmount", "GoodsId", "GoodsTransactionHeaderId", "PricePerItem" },
                values: new object[,]
                {
                    { 1, 2, 2, 1, 25000 },
                    { 2, 1, 1, 1, 100000 },
                    { 4, 1, 1, 1, 70000 },
                    { 3, 3, 3, 2, 20000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_TransactionHeaderId",
                table: "Currencies",
                column: "TransactionHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_GoodsCode",
                table: "Goods",
                column: "GoodsCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsTransactionsDetails_GoodsId",
                table: "GoodsTransactionsDetails",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsTransactionsDetails_GoodsTransactionHeaderId",
                table: "GoodsTransactionsDetails",
                column: "GoodsTransactionHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "GoodsTransactionsDetails");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "GoodsTransactionHeaders");
        }
    }
}
