using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DodoApp.Migrations
{
    public partial class HeaderAndDetailSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "GoodsTransactionHeaders");

            migrationBuilder.InsertData(
                table: "GoodsTransactionHeaders",
                columns: new[] { "Id", "CreatedDate", "PurchaseDate", "ReceiveDate", "TransactionType", "Vendor" },
                values: new object[] { 1, new DateTime(2021, 11, 14, 20, 59, 43, 527, DateTimeKind.Local).AddTicks(7073), new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "sell", null });

            migrationBuilder.InsertData(
                table: "GoodsTransactionHeaders",
                columns: new[] { "Id", "CreatedDate", "PurchaseDate", "ReceiveDate", "TransactionType", "Vendor" },
                values: new object[] { 2, new DateTime(2021, 11, 14, 20, 59, 43, 528, DateTimeKind.Local).AddTicks(5550), new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "purchase", null });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GoodsTransactionsDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GoodsTransactionsDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GoodsTransactionsDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GoodsTransactionsDetails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "TotalPrice",
                table: "GoodsTransactionHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
