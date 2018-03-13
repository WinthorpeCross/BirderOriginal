using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class Nav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TweetDay_BirdId",
                table: "TweetDay");

            migrationBuilder.CreateIndex(
                name: "IX_TweetDay_BirdId",
                table: "TweetDay",
                column: "BirdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TweetDay_BirdId",
                table: "TweetDay");

            migrationBuilder.CreateIndex(
                name: "IX_TweetDay_BirdId",
                table: "TweetDay",
                column: "BirdId",
                unique: true);
        }
    }
}
