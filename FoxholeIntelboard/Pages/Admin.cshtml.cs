using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoxholeIntelboard.DAL;
using IntelboardAPI.Data;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.DTO;

namespace FoxholeIntelboard.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IManagerDto _manager;
        public AdminModel(IManagerDto manager)
        {
           _manager = manager;
        }
        public void OnGet()
        {
        }

        public async Task OnPostSeedDatabaseAsync()
        {
            await _manager.CategoryManager.SeedCategoriesAsync();
            Console.WriteLine("Finished saving Categories successfully");
            await _manager.ResourceManager.SeedResourcesAsync();
            Console.WriteLine("Finished saving Resources successfully!");
            await _manager.MaterialManager.SeedMaterialsAsync();
            Console.WriteLine("Finished saving Materials successfully!");
            await _manager.AmmunitionManager.SeedAmmunitionsAsync();
            Console.WriteLine("Finished saving Ammunitions successfully!");
            await _manager.WeaponManager.SeedWeaponsAsync();
            Console.WriteLine("Finished saving Weapons successfully!");
        }
    }
}
