using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DodoApp.Migrations
{
    public partial class CurrencySeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "ChangeDescription", "ChangingAmount", "CurrencyAmount", "DateOfChange", "TransactionHeaderId" },
                values: new object[,]
                {
                    { 1, "Menjual Barang", 220000, 500000, new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Membeli Barang", -60000, 440000, new DateTime(2021, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Bayar Listrik", -250000, 190000, new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 12, 3, 10, 36, 21, 216, DateTimeKind.Local).AddTicks(4838));

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 12, 3, 10, 36, 21, 217, DateTimeKind.Local).AddTicks(3571));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 18, 14, 41, 947, DateTimeKind.Local).AddTicks(5252));

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 28, 18, 14, 41, 948, DateTimeKind.Local).AddTicks(2838));
        }
    }
}
