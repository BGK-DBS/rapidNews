using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsMedia.Migrations
{
    public partial class removeguid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorGuid",
                table: "CommentItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorGuid",
                table: "CommentItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
