using FoxholeIntelboard.DAL;

namespace FoxholeIntelboard.Configuration
{
    public static class SeedExtensions
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var ammunitionManager = services.GetRequiredService<AmmunitionManager>();
            var materialManager = services.GetRequiredService<MaterialManager>();
            var weaponManager = services.GetRequiredService<WeaponManager>();

            await ammunitionManager.SeedAmmunitionsAsync();
            await materialManager.SeedMaterialsAsync();
            await weaponManager.SeedWeaponsAsync();
        }
    }
}
