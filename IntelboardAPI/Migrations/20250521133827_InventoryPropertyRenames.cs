using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelboardAPI.Migrations
{
    /// <inheritdoc />
    public partial class InventoryPropertyRenames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "Inventories",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Inventories",
                newName: "InventoryId");
        }
    }
}
