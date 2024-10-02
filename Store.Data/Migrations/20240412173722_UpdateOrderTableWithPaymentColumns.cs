using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Data.Migrations
{
    public partial class UpdateOrderTableWithPaymentColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductId",
                table: "OrderItems",
                newName: "ItemOrdered_ProductItemId");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductItemId",
                table: "OrderItems",
                newName: "ItemOrdered_ProductId");
        }
    }
}
