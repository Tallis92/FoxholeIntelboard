using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoxholeIntelboard.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCraftableItemsandDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Costs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "Costs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Costs_MaterialId",
                table: "Costs",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_ResourceId",
                table: "Costs",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Materials_MaterialId",
                table: "Costs",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Resources_ResourceId",
                table: "Costs",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Materials_MaterialId",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Resources_ResourceId",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_MaterialId",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_ResourceId",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Costs");
        }
    }
}
