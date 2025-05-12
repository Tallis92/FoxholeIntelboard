using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelboardAPI.Migrations
{
    /// <inheritdoc />
    public partial class APIDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FacilityMade",
                table: "Materials",
                type: "bit",
                nullable: true);

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
                name: "FacilityMade",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "LargeMaterial",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "TechMaterial",
                table: "Materials");
        }
    }
}
