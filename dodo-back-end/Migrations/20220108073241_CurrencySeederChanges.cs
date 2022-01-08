using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DodoApp.Migrations
{
    public partial class CurrencySeederChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfChange",
                value: new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 8, 14, 32, 41, 191, DateTimeKind.Local).AddTicks(4726));

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 8, 14, 32, 41, 192, DateTimeKind.Local).AddTicks(2349));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfChange",
                value: new DateTime(2021, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 7, 16, 10, 19, 473, DateTimeKind.Local).AddTicks(4530));

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 7, 16, 10, 19, 474, DateTimeKind.Local).AddTicks(2301));
        }
    }
}
