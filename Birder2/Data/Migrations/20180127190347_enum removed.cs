using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class enumremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConservationFlag",
                table: "BirdConservationStatus");

            migrationBuilder.AddColumn<string>(
                name: "ConservationStatus",
                table: "BirdConservationStatus",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConservationStatus",
                table: "BirdConservationStatus");

            migrationBuilder.AddColumn<int>(
                name: "ConservationFlag",
                table: "BirdConservationStatus",
                nullable: false,
                defaultValue: 0);
        }
    }
}
