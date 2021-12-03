using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DodoApp.Migrations
{
    public partial class CurrencyNullableTransactionHeaderid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_GoodsTransactionHeaders_TransactionHeaderId",
                table: "Currencies");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionHeaderId",
                table: "Currencies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_GoodsTransactionHeaders_TransactionHeaderId",
                table: "Currencies",
                column: "TransactionHeaderId",
                principalTable: "GoodsTransactionHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_GoodsTransactionHeaders_TransactionHeaderId",
                table: "Currencies");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionHeaderId",
                table: "Currencies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 20, 9, 7, 7, 578, DateTimeKind.Local).AddTicks(7036));

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 11, 20, 9, 7, 7, 579, DateTimeKind.Local).AddTicks(4748));

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_GoodsTransactionHeaders_TransactionHeaderId",
                table: "Currencies",
                column: "TransactionHeaderId",
                principalTable: "GoodsTransactionHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
