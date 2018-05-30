using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photograph");
        }
    }
}
