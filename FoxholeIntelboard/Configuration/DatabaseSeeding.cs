using FoxholeIntelboard.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FoxholeIntelboard.Configuration
{
    public class DatabaseSeeding
    {
        private readonly IManagerDto _manager;
        public DatabaseSeeding(IManagerDto manager)
        {
            _manager = manager;
        }
        public async Task SeedDatabaseOnStartupAsync()
        {
            try
            {
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
