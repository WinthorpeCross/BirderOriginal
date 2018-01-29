using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class Birdredesign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScientificName",
                table: "Bird",
                newName: "Species");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Bird",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Bird",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "Bird",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genus",
                table: "Bird",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Order",
                table: "Bird",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PopulationSize",
                table: "Bird",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Bird",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Bird");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "Bird");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "Bird");

            migrationBuilder.DropColumn(
                name: "Genus",
                table: "Bird");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Bird");

            migrationBuilder.DropColumn(
                name: "PopulationSize",
                table: "Bird");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bird");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Species",
                table: "Bird",
                newName: "ScientificName");
        }
    }
}
