using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class coordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "ConservationStatus",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "Genus",
                table: "Bird",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DefaultLocationLatitude",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DefaultLocationLongitude",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultLocationLatitude",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DefaultLocationLongitude",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ConservationStatus",
                newName: "Note");

            migrationBuilder.AlterColumn<string>(
                name: "Genus",
                table: "Bird",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
