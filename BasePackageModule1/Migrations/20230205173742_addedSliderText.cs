using Microsoft.EntityFrameworkCore.Migrations;

namespace BasePackageModule1.Migrations
{
    public partial class addedSliderText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BtnLink",
                table: "SliderImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BtnText",
                table: "SliderImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SliderImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SliderTitle",
                table: "SliderImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BtnLink",
                table: "SliderImages");

            migrationBuilder.DropColumn(
                name: "BtnText",
                table: "SliderImages");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SliderImages");

            migrationBuilder.DropColumn(
                name: "SliderTitle",
                table: "SliderImages");
        }
    }
}
