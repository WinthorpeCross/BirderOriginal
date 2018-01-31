using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class r : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BritishStatus",
                columns: table => new
                {
                    BritishStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BirderStatusInBritain = table.Column<string>(nullable: false),
                    BtoStatusInBritain = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BritishStatus", x => x.BritishStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ConservationStatus",
                columns: table => new
                {
                    ConserverationStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConservationStatus = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConservationStatus", x => x.ConserverationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Bird",
                columns: table => new
                {
                    BirdId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BritishStatusId = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Class = table.Column<string>(nullable: false),
                    ConserverationStatusId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    EnglishName = table.Column<string>(nullable: false),
                    Family = table.Column<string>(nullable: false),
                    Genus = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    InternationalName = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    Order = table.Column<string>(nullable: false),
                    PopulationSize = table.Column<string>(nullable: true),
                    Species = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bird", x => x.BirdId);
                    table.ForeignKey(
                        name: "FK_Bird_BritishStatus_BritishStatusId",
                        column: x => x.BritishStatusId,
                        principalTable: "BritishStatus",
                        principalColumn: "BritishStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bird_ConservationStatus_ConserverationStatusId",
                        column: x => x.ConserverationStatusId,
                        principalTable: "ConservationStatus",
                        principalColumn: "ConserverationStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Observation",
                columns: table => new
                {
                    ObservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    BirdId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    ObservationDateTime = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    lat = table.Column<double>(nullable: false),
                    lng = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observation", x => x.ObservationId);
                    table.ForeignKey(
                        name: "FK_Observation_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observation_Bird_BirdId",
                        column: x => x.BirdId,
                        principalTable: "Bird",
                        principalColumn: "BirdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservationTag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    ObervationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationTag", x => new { x.TagId, x.ObervationId });
                    table.ForeignKey(
                        name: "FK_ObservationTag_Observation_ObervationId",
                        column: x => x.ObervationId,
                        principalTable: "Observation",
                        principalColumn: "ObservationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObservationTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bird_BritishStatusId",
                table: "Bird",
                column: "BritishStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bird_ConserverationStatusId",
                table: "Bird",
                column: "ConserverationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_ApplicationUserId",
                table: "Observation",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_BirdId",
                table: "Observation",
                column: "BirdId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservationTag_ObervationId",
                table: "ObservationTag",
                column: "ObervationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ObservationTag");

            migrationBuilder.DropTable(
                name: "Observation");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Bird");

            migrationBuilder.DropTable(
                name: "BritishStatus");

            migrationBuilder.DropTable(
                name: "ConservationStatus");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
