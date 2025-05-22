using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelboardAPI.Migrations
{
    /// <inheritdoc />
    public partial class CraftedItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CratedItem_CraftableItems_CraftableItemId",
                table: "CratedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CratedItem_Inventories_InventoryId",
                table: "CratedItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CratedItem",
                table: "CratedItem");

            migrationBuilder.RenameTable(
                name: "CratedItem",
                newName: "CratedItems");

            migrationBuilder.RenameIndex(
                name: "IX_CratedItem_InventoryId",
                table: "CratedItems",
                newName: "IX_CratedItems_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CratedItem_CraftableItemId",
                table: "CratedItems",
                newName: "IX_CratedItems_CraftableItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CratedItems",
                table: "CratedItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CratedItems_CraftableItems_CraftableItemId",
                table: "CratedItems",
                column: "CraftableItemId",
                principalTable: "CraftableItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CratedItems_Inventories_InventoryId",
                table: "CratedItems",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CratedItems_CraftableItems_CraftableItemId",
                table: "CratedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CratedItems_Inventories_InventoryId",
                table: "CratedItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CratedItems",
                table: "CratedItems");

            migrationBuilder.RenameTable(
                name: "CratedItems",
                newName: "CratedItem");

            migrationBuilder.RenameIndex(
                name: "IX_CratedItems_InventoryId",
                table: "CratedItem",
                newName: "IX_CratedItem_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CratedItems_CraftableItemId",
                table: "CratedItem",
                newName: "IX_CratedItem_CraftableItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CratedItem",
                table: "CratedItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CratedItem_CraftableItems_CraftableItemId",
                table: "CratedItem",
                column: "CraftableItemId",
                principalTable: "CraftableItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CratedItem_Inventories_InventoryId",
                table: "CratedItem",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id");
        }
    }
}
