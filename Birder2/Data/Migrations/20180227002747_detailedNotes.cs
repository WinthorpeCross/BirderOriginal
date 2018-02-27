using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class detailedNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Observation",
                newName: "NoteWeather");

            migrationBuilder.AddColumn<string>(
                name: "NoteAppearance",
                table: "Observation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoteBehaviour",
                table: "Observation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoteGeneral",
                table: "Observation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoteHabitat",
                table: "Observation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoteVocalisation",
                table: "Observation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteAppearance",
                table: "Observation");

            migrationBuilder.DropColumn(
                name: "NoteBehaviour",
                table: "Observation");

            migrationBuilder.DropColumn(
                name: "NoteGeneral",
                table: "Observation");

            migrationBuilder.DropColumn(
                name: "NoteHabitat",
                table: "Observation");

            migrationBuilder.DropColumn(
                name: "NoteVocalisation",
                table: "Observation");

            migrationBuilder.RenameColumn(
                name: "NoteWeather",
                table: "Observation",
                newName: "Note");
        }
    }
}
