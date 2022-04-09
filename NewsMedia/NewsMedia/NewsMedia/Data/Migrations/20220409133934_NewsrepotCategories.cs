using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsMedia.Data.Migrations
{
    public partial class NewsrepotCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "NewsReport",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "NewsReport");
        }
    }
}
