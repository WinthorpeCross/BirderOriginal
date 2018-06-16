using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class Tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_AspNetUsers_ApplicationUserId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_ApplicationUserId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Tag",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ApplicationUserId",
                table: "Tag",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_AspNetUsers_ApplicationUserId",
                table: "Tag",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
