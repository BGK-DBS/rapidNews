using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsMedia.Migrations
{
    public partial class publish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "NewsReport",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "NewsReport");
        }
    }
}
