using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DodoApp.Migrations
{
    public partial class TransactionHeaderTitleProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "GoodsTransactionHeaders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2022, 1, 7, 16, 10, 19, 473, DateTimeKind.Local).AddTicks(4530), "Dimas kanjeng" });

            migrationBuilder.UpdateData(
                table: "GoodsTransactionHeaders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2022, 1, 7, 16, 10, 19, 474, DateTimeKind.Local).AddTicks(2301), "Ita jaya motor" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "GoodsTransactionHeaders");

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
        }
    }
}
