using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Birder2.Data.Migrations
{
    public partial class BritishStatusToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bird_BritishStatus_BritishStatusId",
                table: "Bird");

            migrationBuilder.DropTable(
                name: "BritishStatus");

            migrationBuilder.DropTable(
                name: "SalesOrderItems");

            migrationBuilder.DropTable(
                name: "SalesOrder");

            migrationBuilder.DropIndex(
                name: "IX_Bird_BritishStatusId",
                table: "Bird");

            migrationBuilder.DropColumn(
                name: "BritishStatusId",
                table: "Bird");

            migrationBuilder.AddColumn<int>(
                name: "BirderStatus",
                table: "Bird",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirderStatus",
                table: "Bird");

            migrationBuilder.AddColumn<int>(
                name: "BritishStatusId",
                table: "Bird",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BritishStatus",
                columns: table => new
                {
                    BritishStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BirderStatusInBritain = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BritishStatus", x => x.BritishStatusId);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrder",
                columns: table => new
                {
                    SalesOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(nullable: true),
                    PONumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrder", x => x.SalesOrderId);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderItems",
                columns: table => new
                {
                    SalesOrderItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductCode = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    SalesOrderId = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderItems", x => x.SalesOrderItemId);
                    table.ForeignKey(
                        name: "FK_SalesOrderItems_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "SalesOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bird_BritishStatusId",
                table: "Bird",
                column: "BritishStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItems_SalesOrderId",
                table: "SalesOrderItems",
                column: "SalesOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bird_BritishStatus_BritishStatusId",
                table: "Bird",
                column: "BritishStatusId",
                principalTable: "BritishStatus",
                principalColumn: "BritishStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
