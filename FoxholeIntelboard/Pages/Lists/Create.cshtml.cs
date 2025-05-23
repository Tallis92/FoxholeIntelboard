using FoxholeIntelboard.DAL;
using IntelboardAPI.DTO;
using IntelboardAPI.Data;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var inputs = JsonSerializer.Deserialize<List<CratedItemInput>>(SelectedItems);
            var dto = new InventoryDto
            {
                Name = Inventory.Name,
                CratedItems = new List<CratedItemDto>()
            };

            foreach (var input in inputs)
            {
                CraftableItem? item = null;
                switch (input.Type)
                {
                    case "Ammunition":
                        item = await _ammunitionManager.GetAmmunitionByIdAsync(input.Id);
                        break;
                    case "Weapon":
                        item = await _weaponManager.GetWeaponByIdAsync(input.Id);
                        break;
                    case "Material":
                        item = await _materialManager.GetMaterialByIdAsync(input.Id);
                        break;
                    default:
                        ModelState.AddModelError(string.Empty, $"No item with ID {input.Id} was found.");
                        break;
                }
                //CraftableItem? item = await _ammunitionManager.GetAmmunitionByIdAsync(input.Id);
                //if (item == null)
                //    item = await _weaponManager.GetWeaponByIdAsync(input.Id);
                //if (item == null)
                //    item = await _materialManager.GetMaterialByIdAsync(input.Id);

                // Validation: Only add if item exists
                if (item == null)
                {
                    ModelState.AddModelError(string.Empty, $"Item with ID {input.Id} does not exist.");
                    continue;
                }

                dto.CratedItems.Add(new CratedItemDto
                {
                    CraftableItemId= item.Id,
                    Amount = input.Amount,
                    Description = $"A crate of {input.Amount}x {item.Name}. Submit to a stockpile or seaport."
                });
            }

            if (!ModelState.IsValid)
            {
                // Return to page and show errors
                return Page();
            }

            await _inventoryManager.CreateInventoryAsync(dto);
            return RedirectToPage("./Index");
        }

        public async Task SeedDataAsync()
        {
            await _ammunitionManager.SeedAmmunitionsAsync();
            await _materialManager.SeedMaterialsAsync();
            await _weaponManager.SeedWeaponsAsync();
        }

        public class CratedItemInput
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("amount")]
            public int Amount { get; set; }
            [JsonPropertyName("type")]
            public string Type { get; set; }
        }
       
    }
}
