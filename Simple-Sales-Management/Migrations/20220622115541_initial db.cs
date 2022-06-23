using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simple_Sales_Management.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    SellerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.SellerId);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerForeignKey = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sales_Sellers_SellerForeignKey",
                        column: x => x.SellerForeignKey,
                        principalTable: "Sellers",
                        principalColumn: "SellerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SellerForeignKey",
                table: "Sales",
                column: "SellerForeignKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Sellers");
        }
    }
}
