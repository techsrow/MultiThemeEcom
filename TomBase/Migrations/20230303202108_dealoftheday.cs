using Microsoft.EntityFrameworkCore.Migrations;

namespace TomBase.Migrations
{
    public partial class dealoftheday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DealOfTheDay",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DealOfTheDay",
                table: "Products");
        }
    }
}
