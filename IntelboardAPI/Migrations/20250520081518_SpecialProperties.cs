using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelboardAPI.Migrations
{
    /// <inheritdoc />
    public partial class SpecialProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecialProperties",
                table: "Weapons",
                newName: "WeaponProperties");

            migrationBuilder.RenameColumn(
                name: "SpecialProperties",
                table: "Ammunitions",
                newName: "AmmoProperties");

            migrationBuilder.RenameColumn(
                name: "Damage",
                table: "Ammunitions",
                newName: "DamageType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeaponProperties",
                table: "Weapons",
                newName: "SpecialProperties");

            migrationBuilder.RenameColumn(
                name: "DamageType",
                table: "Ammunitions",
                newName: "Damage");

            migrationBuilder.RenameColumn(
                name: "AmmoProperties",
                table: "Ammunitions",
                newName: "SpecialProperties");
        }
    }
}
