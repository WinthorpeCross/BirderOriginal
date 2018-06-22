using Microsoft.EntityFrameworkCore.Migrations;

namespace Birder2.Data.Migrations
{
    public partial class Privacylevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedPrivacyLevel",
                table: "Observation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedPrivacyLevel",
                table: "Observation");
        }
    }
}
