using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Products_ProductId",
                schema: "seller",
                table: "Inventories",
                column: "ProductId",
                principalSchema: "product",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Inventories_InventoryId",
                schema: "order",
                table: "Items",
                column: "InventoryId",
                principalSchema: "seller",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Inventories_InventoryId",
                schema: "order",
                table: "Items"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Products_ProductId",
                schema: "seller",
                table: "Inventories"
            );
        }

    }
}
