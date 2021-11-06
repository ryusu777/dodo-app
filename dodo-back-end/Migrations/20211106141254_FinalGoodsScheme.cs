using Microsoft.EntityFrameworkCore.Migrations;

namespace DodoApp.Migrations
{
    public partial class FinalGoodsScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsStock");

            migrationBuilder.DropIndex(
                name: "IX_Goods_Code",
                table: "Goods");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Goods",
                newName: "PartNumber");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Goods",
                newName: "GoodsName");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Goods",
                newName: "GoodsCode");

            migrationBuilder.AddColumn<string>(
                name: "CarType",
                table: "Goods",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PurchasePrice",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockAvailable",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Goods_GoodsCode",
                table: "Goods",
                column: "GoodsCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Goods_GoodsCode",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "CarType",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "StockAvailable",
                table: "Goods");

            migrationBuilder.RenameColumn(
                name: "PartNumber",
                table: "Goods",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "GoodsName",
                table: "Goods",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "GoodsCode",
                table: "Goods",
                newName: "Category");

            migrationBuilder.CreateTable(
                name: "GoodsStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountAvailable = table.Column<int>(type: "int", nullable: false),
                    GoodsId = table.Column<int>(type: "int", nullable: false),
                    PurchasePricePerItem = table.Column<int>(type: "int", nullable: false),
                    SellPricePerItem = table.Column<int>(type: "int", nullable: false),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsStock_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goods_Code",
                table: "Goods",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsStock_GoodsId",
                table: "GoodsStock",
                column: "GoodsId");
        }
    }
}
