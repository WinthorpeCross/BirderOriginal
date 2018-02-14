using Microsoft.EntityFrameworkCore.Migrations;

namespace Birder2.Data.Migrations
{
    public partial class QtymovedtoObservationBird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Observation");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ObservationBird",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ObservationBird");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Observation",
                nullable: false,
                defaultValue: 0);
        }
    }
}
