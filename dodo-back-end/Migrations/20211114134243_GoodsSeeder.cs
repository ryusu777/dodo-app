using Microsoft.EntityFrameworkCore.Migrations;

namespace DodoApp.Migrations
{
    public partial class GoodsSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "CarType", "GoodsCode", "GoodsName", "MinimalAvailable", "PartNumber", "PurchasePrice", "StockAvailable" },
                values: new object[] { 1, "S89", "J001", "Join Kopel", 3, "GUD88", 70000, 2 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "CarType", "GoodsCode", "GoodsName", "MinimalAvailable", "PartNumber", "PurchasePrice", "StockAvailable" },
                values: new object[] { 2, "J98", "B001", "Bearing", 10, "BED002", 20000, 11 });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "CarType", "GoodsCode", "GoodsName", "MinimalAvailable", "PartNumber", "PurchasePrice", "StockAvailable" },
                values: new object[] { 3, "ALL", "KR01", "Kampas Rem", 10, "-", 20000, 7 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
