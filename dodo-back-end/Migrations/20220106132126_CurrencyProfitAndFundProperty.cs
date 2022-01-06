using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DodoApp.Migrations
{
    public partial class CurrencyProfitAndFundProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrencyAmount",
                table: "Currencies",
                newName: "ProfitAmount");

            migrationBuilder.RenameColumn(
                name: "ChangingAmount",
                table: "Currencies",
                newName: "FundAmount");

            migrationBuilder.AddColumn<int>(
                name: "ChangingFundAmount",
                table: "Currencies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChangingProfitAmount",
                table: "Currencies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ChangingFundAmount", "ChangingProfitAmount", "FundAmount", "ProfitAmount" },
                values: new object[] { 110000, 40000, 110000, 40000 });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ChangingFundAmount", "FundAmount", "ProfitAmount" },
                values: new object[] { -90000, 20000, 40000 });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ChangingProfitAmount", "FundAmount", "ProfitAmount" },
                values: new object[] { -25000, 20000, 15000 });

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 6, 20, 21, 26, 299, DateTimeKind.Local).AddTicks(124));

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 1, 6, 20, 21, 26, 299, DateTimeKind.Local).AddTicks(7507));

            migrationBuilder.UpdateData(
                table: "GoodsTransactionsDetails",
                keyColumn: "Id",
                keyValue: 3,
                column: "GoodsAmount",
                value: 1);

            migrationBuilder.UpdateData(
                table: "GoodsTransactionsDetails",
                keyColumn: "Id",
                keyValue: 4,
                column: "GoodsTransactionHeaderId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangingFundAmount",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "ChangingProfitAmount",
                table: "Currencies");

            migrationBuilder.RenameColumn(
                name: "ProfitAmount",
                table: "Currencies",
                newName: "CurrencyAmount");

            migrationBuilder.RenameColumn(
                name: "FundAmount",
                table: "Currencies",
                newName: "ChangingAmount");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ChangingAmount", "CurrencyAmount" },
                values: new object[] { 220000, 500000 });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ChangingAmount", "CurrencyAmount" },
                values: new object[] { -60000, 440000 });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ChangingAmount", "CurrencyAmount" },
                values: new object[] { -250000, 190000 });

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 12, 5, 19, 25, 35, 647, DateTimeKind.Local).AddTicks(5972));

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 12, 5, 19, 25, 35, 648, DateTimeKind.Local).AddTicks(3512));

            migrationBuilder.UpdateData(
                table: "GoodsTransactionsDetails",
                keyColumn: "Id",
                keyValue: 3,
                column: "GoodsAmount",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GoodsTransactionsDetails",
                keyColumn: "Id",
                keyValue: 4,
                column: "GoodsTransactionHeaderId",
                value: 1);
        }
    }
}
