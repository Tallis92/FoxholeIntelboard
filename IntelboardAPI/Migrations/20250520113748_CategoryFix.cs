using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelboardAPI.Migrations
{
    /// <inheritdoc />
    public partial class CategoryFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoriId",
                table: "Weapons",
                newName: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Weapons",
                newName: "CategoriId");
        }
    }
}
