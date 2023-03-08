using Microsoft.EntityFrameworkCore.Migrations;

namespace TomBase.Migrations
{
    public partial class pagesheaderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShopPageHeeaderImage",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AboutUsPageHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderImage = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsPageHeaders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUsPageHeaders");

            migrationBuilder.DropColumn(
                name: "ShopPageHeeaderImage",
                table: "Products");
        }
    }
}
