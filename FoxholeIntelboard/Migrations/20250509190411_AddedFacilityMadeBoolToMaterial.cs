using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoxholeIntelboard.Migrations
{
    /// <inheritdoc />
    public partial class AddedFacilityMadeBoolToMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FacilityMade",
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
        }
    }
}
