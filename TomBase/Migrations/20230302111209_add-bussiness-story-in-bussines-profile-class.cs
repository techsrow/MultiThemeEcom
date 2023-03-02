using Microsoft.EntityFrameworkCore.Migrations;

namespace TomBase.Migrations
{
    public partial class addbussinessstoryinbussinesprofileclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BussinessDescription",
                table: "BusinessProfile",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BussinessDescription",
                table: "BusinessProfile");
        }
    }
}
