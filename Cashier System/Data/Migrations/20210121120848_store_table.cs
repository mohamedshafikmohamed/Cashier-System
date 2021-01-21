using Microsoft.EntityFrameworkCore.Migrations;

namespace Cashier_System.Data.Migrations
{
    public partial class store_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    PurchasingPrice = table.Column<float>(nullable: false),
                    price = table.Column<float>(nullable: false),
                    SellingPrice = table.Column<float>(nullable: false),
                    Gain = table.Column<float>(nullable: false),
                    IsDeleted = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Store");
        }
    }
}
