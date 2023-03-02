using Microsoft.EntityFrameworkCore.Migrations;

namespace TomBase.Migrations
{
    public partial class addbottompagesclasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InformationPages",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeadingName = table.Column<string>(nullable: true),
                    PageName = table.Column<string>(nullable: true),
                    PageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformationPages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MyAccountPages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeadingName = table.Column<string>(nullable: true),
                    PageName = table.Column<string>(nullable: true),
                    PageUrl = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyAccountPages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InformationPages");

            migrationBuilder.DropTable(
                name: "MyAccountPages");
        }
    }
}
