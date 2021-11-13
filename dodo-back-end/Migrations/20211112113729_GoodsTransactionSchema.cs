using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DodoApp.Migrations
{
    public partial class GoodsTransactionSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodsTransactionHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    Vendor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsTransactionHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionHeaderId = table.Column<int>(type: "int", nullable: false),
                    CurrencyAmount = table.Column<int>(type: "int", nullable: false),
                    ChangingAmount = table.Column<int>(type: "int", nullable: false),
                    DateOfChange = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_GoodsTransactionHeaders_TransactionHeaderId",
                        column: x => x.TransactionHeaderId,
                        principalTable: "GoodsTransactionHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsTransactionsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        name: "FK_GoodsTransactionsDetails_GoodsTransactionHeaders_GoodsTransactionHeaderId",
                        column: x => x.GoodsTransactionHeaderId,
                        principalTable: "GoodsTransactionHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_TransactionHeaderId",
                table: "Currencies",
                column: "TransactionHeaderId");

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
                name: "GoodsTransactionHeaders");
        }
    }
}
