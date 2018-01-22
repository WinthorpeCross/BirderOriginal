using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class fk_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observation_AspNetUsers_ApplicationUserId",
                table: "Observation");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Observation",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Observation_AspNetUsers_ApplicationUserId",
                table: "Observation",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observation_AspNetUsers_ApplicationUserId",
                table: "Observation");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Observation",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Observation_AspNetUsers_ApplicationUserId",
                table: "Observation",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
