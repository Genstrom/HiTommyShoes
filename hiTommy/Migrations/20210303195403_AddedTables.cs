using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hiTommy.Migrations
{
    public partial class AddedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Brands",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Brands", x => x.Id); });

            migrationBuilder.CreateTable(
                "Customers",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>("nvarchar(max)", nullable: true),
                    LastName = table.Column<string>("nvarchar(max)", nullable: true),
                    Email = table.Column<string>("nvarchar(max)", nullable: true),
                    Address = table.Column<string>("nvarchar(max)", nullable: true),
                    PostalCode = table.Column<int>("int", nullable: false),
                    TelephoneNumber = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Customers", x => x.Id); });

            migrationBuilder.CreateTable(
                "Order",
                table => new
                {
                    OrderId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDateTime = table.Column<DateTime>("datetime2", nullable: false),
                    CustomerId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        "FK_Order_Customers_CustomerId",
                        x => x.CustomerId,
                        "Customers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Shoes",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>("decimal(18,2)", nullable: false),
                    IsOnSale = table.Column<bool>("bit", nullable: false),
                    BrandId = table.Column<int>("int", nullable: false),
                    SalePrice = table.Column<decimal>("decimal(18,2)", nullable: true),
                    OrderId = table.Column<int>("int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoes", x => x.Id);
                    table.ForeignKey(
                        "FK_Shoes_Brands_BrandId",
                        x => x.BrandId,
                        "Brands",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Shoes_Order_OrderId",
                        x => x.OrderId,
                        "Order",
                        "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Quantities",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<double>("float", nullable: false),
                    Quantities = table.Column<int>("int", nullable: false),
                    ShoeId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quantities", x => x.Id);
                    table.ForeignKey(
                        "FK_Quantities_Shoes_ShoeId",
                        x => x.ShoeId,
                        "Shoes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Order_CustomerId",
                "Order",
                "CustomerId");

            migrationBuilder.CreateIndex(
                "IX_Quantities_ShoeId",
                "Quantities",
                "ShoeId");

            migrationBuilder.CreateIndex(
                "IX_Shoes_BrandId",
                "Shoes",
                "BrandId");

            migrationBuilder.CreateIndex(
                "IX_Shoes_OrderId",
                "Shoes",
                "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Quantities");

            migrationBuilder.DropTable(
                "Brands");

            migrationBuilder.DropTable(
                "Order");

            migrationBuilder.DropTable(
                "Customers");
        }
    }
}