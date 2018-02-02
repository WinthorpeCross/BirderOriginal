using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class renameLocationCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lng",
                table: "Observation",
                newName: "LocationLongitude");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "Observation",
                newName: "LocationLatitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationLongitude",
                table: "Observation",
                newName: "lng");

            migrationBuilder.RenameColumn(
                name: "LocationLatitude",
                table: "Observation",
                newName: "lat");
        }
    }
}
