using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoxholeIntelboard.Migrations
{
    /// <inheritdoc />
    public partial class CraftableItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    AmmunitionId = table.Column<int>(type: "int", nullable: true),
                    MaterialId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cost_Ammunitions_AmmunitionId",
                        column: x => x.AmmunitionId,
                        principalTable: "Ammunitions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cost_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cost_AmmunitionId",
                table: "Cost",
                column: "AmmunitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cost_MaterialId",
                table: "Cost",
                column: "MaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cost");
        }
    }
}
