using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Birder2.Data.Migrations
{
    public partial class newphotosetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photograph");

            migrationBuilder.AddColumn<bool>(
                name: "HasPhotos",
                table: "Observation",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPhotos",
                table: "Observation");

            migrationBuilder.CreateTable(
                name: "Photograph",
                columns: table => new
                {
                    PhotographId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ObservationId = table.Column<int>(nullable: false),
                    PhotographUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photograph", x => x.PhotographId);
                    table.ForeignKey(
                        name: "FK_Photograph_Observation_ObservationId",
                        column: x => x.ObservationId,
                        principalTable: "Observation",
                        principalColumn: "ObservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photograph_ObservationId",
                table: "Photograph",
                column: "ObservationId");
        }
    }
}
