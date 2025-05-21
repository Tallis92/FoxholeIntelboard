using FoxholeIntelboard.DAL;
using IntelboardAPI.Data;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FoxholeIntelboard.Pages.Lists
{
    public class CreateModel : PageModel
    {
        private readonly AmmunitionManager _ammunitionManager;
        private readonly MaterialManager _materialManager;
        private readonly ResourceManager _resourceManager;
        private readonly WeaponManager _weaponManager;
        private readonly CategoryManager _categoryManager;
        private readonly InventoryManager _inventoryManager;

        public CreateModel(AmmunitionManager ammunitionManager, MaterialManager materialManager,
            ResourceManager resourceManager, WeaponManager weaponManager, CategoryManager categoryManager, InventoryManager inventoryManager)
        {
            _ammunitionManager = ammunitionManager;
            _materialManager = materialManager;
            _resourceManager = resourceManager;
            _weaponManager = weaponManager;
            _categoryManager = categoryManager;
            _inventoryManager = inventoryManager;
        }

        public IList<Ammunition> Ammunitions { get; set; }
        public IList<Material> Materials { get; set; }
        public IList<Resource> Resources { get; set; }
        public IList<Weapon> Weapons { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<CraftableItem> CraftableItems { get; set; }
        [BindProperty]
        public Inventory Inventory { get; set; }
        [BindProperty]
        public string SelectedItems { get; set; }

        public async Task OnGetAsync()
        {
            Ammunitions = await _ammunitionManager.GetAmmunitionsAsync();
            Resources = await _resourceManager.GetResourcesAsync();
            Materials = await _materialManager.GetMaterialsAsync();
            Weapons = await _weaponManager.GetWeaponsAsync();
            Categories = await _categoryManager.GetCategoriesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var items = JsonSerializer.Deserialize<List<CratedItemInput>>(SelectedItems);
            Inventory.CratedItems = new List<CratedItem>();

            foreach (var input in items)
            {
                CraftableItem? item = await _ammunitionManager.GetAmmunitionByIdAsync(input.Id);
                if (item == null){
                    item = await _weaponManager.GetWeaponByIdAsync(input.Id);
                }

                if (item == null){
                    item = await _materialManager.GetMaterialByIdAsync(input.Id);
                }

                Inventory.CratedItems.Add(new CratedItem
                {
                    CraftableItem = item,
                    Amount = input.Amount,
                    Description = $"A crate of {input.Amount}x {item.Name}. Submit to a stockpile or seaport."
                });
            }

            await _inventoryManager.CreateInventoryAsync(Inventory);
         

            return RedirectToPage("./Index");
        }
        public class CratedItemInput
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Amount { get; set; }
        }
    }
}
