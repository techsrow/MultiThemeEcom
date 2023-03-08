using Microsoft.EntityFrameworkCore.Migrations;

namespace TomBase.Migrations
{
    public partial class aboutUsPageBanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PageBanner",
                table: "AboutUs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageBanner",
                table: "AboutUs");
        }
    }
}
