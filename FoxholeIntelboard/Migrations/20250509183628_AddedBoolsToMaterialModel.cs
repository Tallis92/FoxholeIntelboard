using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoxholeIntelboard.Migrations
{
    /// <inheritdoc />
    public partial class AddedBoolsToMaterialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LargeMaterial",
                table: "Materials",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TechMaterial",
                table: "Materials",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LargeMaterial",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "TechMaterial",
                table: "Materials");
        }
    }
}
