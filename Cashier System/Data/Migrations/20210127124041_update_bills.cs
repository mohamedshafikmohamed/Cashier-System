using Microsoft.EntityFrameworkCore.Migrations;

namespace Cashier_System.Data.Migrations
{
    public partial class update_bills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "products_names",
                table: "Bills");

            migrationBuilder.AddColumn<string>(
                name: "products_ids",
                table: "Bills",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "products_ids",
                table: "Bills");

            migrationBuilder.AddColumn<string>(
                name: "products_names",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
