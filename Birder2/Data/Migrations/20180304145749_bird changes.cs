using Microsoft.EntityFrameworkCore.Migrations;

namespace Birder2.Data.Migrations
{
    public partial class birdchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BtoStatusInBritain",
                table: "BritishStatus");

            migrationBuilder.AddColumn<string>(
                name: "BtoStatusInBritain",
                table: "Bird",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BtoStatusInBritain",
                table: "Bird");

            migrationBuilder.AddColumn<string>(
                name: "BtoStatusInBritain",
                table: "BritishStatus",
                nullable: false,
                defaultValue: "");
        }
    }
}
