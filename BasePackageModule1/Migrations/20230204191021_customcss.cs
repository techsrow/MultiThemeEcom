using Microsoft.EntityFrameworkCore.Migrations;

namespace BasePackageModule1.Migrations
{
    public partial class customcss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomCsses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainColorOne = table.Column<string>(nullable: true),
                    SecondaryColor = table.Column<string>(nullable: true),
                    HeadingColor = table.Column<string>(nullable: true),
                    ParagraphColor = table.Column<string>(nullable: true),
                    HeadingFont = table.Column<string>(nullable: true),
                    BodyFont = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCsses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomCsses");
        }
    }
}
