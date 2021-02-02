using Microsoft.EntityFrameworkCore.Migrations;

namespace Cashier_System.Data.Migrations
{
    public partial class Billsbackup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Bills",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Bills");
        }
    }
}
