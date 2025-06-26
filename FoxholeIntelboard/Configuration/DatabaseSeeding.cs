using IntelboardCore.DTO.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FoxholeIntelboard.Configuration
{
    public static class DatabaseSeeding
    {
        public static async Task SeedDatabaseOnStartupAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;  
            try
            {
                var _manager = services.GetRequiredService<IManagerDto>();

                await _manager.CategoryManager.SeedCategoriesAsync();
                await _manager.ResourceManager.SeedResourcesAsync();
                await _manager.MaterialManager.SeedMaterialsAsync();
                await _manager.AmmunitionManager.SeedAmmunitionsAsync();
                await _manager.WeaponManager.SeedWeaponsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("Seeded Database Successfully");
        }

    }


}
