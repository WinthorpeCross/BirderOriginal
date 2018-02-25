using Microsoft.EntityFrameworkCore.Migrations;

namespace Birder2.Data.Migrations
{
    public partial class changesToBird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Bird");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Bird",
                nullable: true);
        }
    }
}
