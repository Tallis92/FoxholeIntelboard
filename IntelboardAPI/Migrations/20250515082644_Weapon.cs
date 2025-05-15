using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelboardAPI.Migrations
{
    /// <inheritdoc />
    public partial class Weapon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FactionId = table.Column<int>(type: "int", nullable: false),
                    CrateAmount = table.Column<int>(type: "int", nullable: false),
                    WeaponType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmmunitionId = table.Column<int>(type: "int", nullable: false),
                    SpecialProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTeched = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_CraftableItems_Id",
                        column: x => x.Id,
                        principalTable: "CraftableItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weapons");
        }
    }
}
