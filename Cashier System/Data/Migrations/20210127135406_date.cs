using Microsoft.EntityFrameworkCore.Migrations;

namespace Cashier_System.Data.Migrations
{
    public partial class date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Taxes",
                table: "Bills",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "date",
                table: "Bills",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taxes",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "date",
                table: "Bills");
        }
    }
}
