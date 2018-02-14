using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class ManyManyObservationBirdrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observation_Bird_BirdId",
                table: "Observation");

            migrationBuilder.DropIndex(
                name: "IX_Observation_BirdId",
                table: "Observation");

            migrationBuilder.DropColumn(
                name: "BirdId",
                table: "Observation");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Observation");

            migrationBuilder.CreateTable(
                name: "ObservationBird",
                columns: table => new
                {
                    BirdId = table.Column<int>(nullable: false),
                    ObervationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationBird", x => new { x.BirdId, x.ObervationId });
                    table.ForeignKey(
                        name: "FK_ObservationBird_Bird_BirdId",
                        column: x => x.BirdId,
                        principalTable: "Bird",
                        principalColumn: "BirdId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObservationBird_Observation_ObervationId",
                        column: x => x.ObervationId,
                        principalTable: "Observation",
                        principalColumn: "ObservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObservationBird_ObervationId",
                table: "ObservationBird",
                column: "ObervationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObservationBird");

            migrationBuilder.AddColumn<int>(
                name: "BirdId",
                table: "Observation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Observation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Observation_BirdId",
                table: "Observation",
                column: "BirdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Observation_Bird_BirdId",
                table: "Observation",
                column: "BirdId",
                principalTable: "Bird",
                principalColumn: "BirdId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
