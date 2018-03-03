using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class birdsongchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TweetDay");

            migrationBuilder.DropColumn(
                name: "InformationUrl",
                table: "TweetDay");

            migrationBuilder.DropColumn(
                name: "TweetUrl",
                table: "TweetDay");

            migrationBuilder.AddColumn<string>(
                name: "SongUrl",
                table: "Bird",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LifeListViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BtoStatus = table.Column<string>(nullable: true),
                    ConservationStatus = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    PopSize = table.Column<string>(nullable: true),
                    ScientificName = table.Column<string>(nullable: true),
                    Vernacular = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeListViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LifeListViewModel");

            migrationBuilder.DropColumn(
                name: "SongUrl",
                table: "Bird");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TweetDay",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InformationUrl",
                table: "TweetDay",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TweetUrl",
                table: "TweetDay",
                nullable: true);
        }
    }
}
