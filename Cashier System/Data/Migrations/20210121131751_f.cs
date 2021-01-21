using Microsoft.EntityFrameworkCore.Migrations;

namespace Cashier_System.Data.Migrations
{
    public partial class f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "Store");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "price",
                table: "Store",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
