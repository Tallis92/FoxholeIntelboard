using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoxholeIntelboard.DAL;
using IntelboardAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FoxholeIntelboard.Pages
{
    public class AdminModel : PageModel
    {
        private readonly AmmunitionManager _ammunitionManager;
        private readonly MaterialManager _materialManager;
        private readonly ResourceManager _resourceManager;
        private readonly WeaponManager _weaponManager;
        public AdminModel(AmmunitionManager ammunitionManager, MaterialManager materialManager, ResourceManager resourceManager, WeaponManager weaponManager)
        {
            _ammunitionManager = ammunitionManager;
            _materialManager = materialManager;
            _resourceManager = resourceManager;
            _weaponManager = weaponManager;
        }
        public void OnGet()
        {
        }

        public async Task OnPostSeedDatabaseAsync()
        {
            await _resourceManager.SeedResourcesAsync();
            Console.WriteLine("Finished saving Resources successfully!");
            await _materialManager.SeedMaterialsAsync();
            Console.WriteLine("Finished saving Materials successfully!");
            await _ammunitionManager.SeedAmmunitionsAsync();
            Console.WriteLine("Finished saving Ammunitions successfully!");
            await _weaponManager.SeedWeaponsAsync();
            Console.WriteLine("Finished saving Weapons successfully!");
        }
    }
}
