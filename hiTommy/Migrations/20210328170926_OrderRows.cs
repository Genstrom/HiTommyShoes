using Microsoft.EntityFrameworkCore.Migrations;

namespace hiTommy.Migrations
{
    public partial class OrderRows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               "OrderRows",
               table => new
               {
                   Id = table.Column<int>("int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   OrderItemName = table.Column<string>("nvarchar(max)", nullable: true),
                   OrderItemType = table.Column<string>("nvarchar(max)", nullable: true),
                   OrderItemPrice = table.Column<string>("nvarchar(max)", nullable: true),
                   OrderId = table.Column<int>("int", nullable: false)
               },
                        constraints: table =>
                        {
                            table.PrimaryKey("PK_Id", x => x.Id);
                            table.ForeignKey(
                                "FK_OrderRows_Order_OrderId",
                                x => x.OrderId,
                                "Order",
                                "OrderId",
                                onDelete: ReferentialAction.Cascade);
                        });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
