using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IntelboardCore.DAL;
//using IntelboardAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using IntelboardCore.DTO.Interfaces;

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

        public async Task <IActionResult> OnPostSeedDatabaseAsync()
        { 
            try
            {
                await _manager.CategoryManager.SeedCategoriesAsync();
                await _manager.ResourceManager.SeedResourcesAsync();
                await _manager.MaterialManager.SeedMaterialsAsync();
                await _manager.AmmunitionManager.SeedAmmunitionsAsync();
                await _manager.WeaponManager.SeedWeaponsAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
            
            Console.WriteLine("Seeded Database Successfully");
            return RedirectToPage("/Index");
        }
    }
}
