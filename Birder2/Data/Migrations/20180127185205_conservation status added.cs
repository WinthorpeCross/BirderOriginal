using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class conservationstatusadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BirdConserverationStatusId",
                table: "Bird",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BirdConservationStatus",
                columns: table => new
                {
                    BirdConserverationStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConservationFlag = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirdConservationStatus", x => x.BirdConserverationStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bird_BirdConserverationStatusId",
                table: "Bird",
                column: "BirdConserverationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bird_BirdConservationStatus_BirdConserverationStatusId",
                table: "Bird",
                column: "BirdConserverationStatusId",
                principalTable: "BirdConservationStatus",
                principalColumn: "BirdConserverationStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bird_BirdConservationStatus_BirdConserverationStatusId",
                table: "Bird");

            migrationBuilder.DropTable(
                name: "BirdConservationStatus");

            migrationBuilder.DropIndex(
                name: "IX_Bird_BirdConserverationStatusId",
                table: "Bird");

            migrationBuilder.DropColumn(
                name: "BirdConserverationStatusId",
                table: "Bird");
        }
    }
}
