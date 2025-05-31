using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelboardAPI.Migrations
{
    /// <inheritdoc />
    public partial class RequiredAmountInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequiredAmount",
                table: "CratedItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredAmount",
                table: "CratedItems");
        }
    }
}
