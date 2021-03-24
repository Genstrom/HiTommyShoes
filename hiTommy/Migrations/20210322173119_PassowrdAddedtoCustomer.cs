using Microsoft.EntityFrameworkCore.Migrations;

namespace hiTommy.Migrations
{
    public partial class PassowrdAddedtoCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<string>(
                name: "Passowrd",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Passowrd",
                table: "Customers");
        }
    }
}
