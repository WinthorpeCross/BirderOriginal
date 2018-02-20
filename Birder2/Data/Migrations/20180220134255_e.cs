using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class e : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Network",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    FollowerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Network", x => new { x.ApplicationUserId, x.FollowerId });
                    table.ForeignKey(
                        name: "FK_Network_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Network_AspNetUsers_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Network_FollowerId",
                table: "Network",
                column: "FollowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Network");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "AspNetUsers");
        }
    }
}
