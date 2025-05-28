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
        private readonly CraftableItemManager _craftableItemManager;

        public CreateModel(AmmunitionManager ammunitionManager, MaterialManager materialManager,
            ResourceManager resourceManager, WeaponManager weaponManager, CategoryManager categoryManager, InventoryManager inventoryManager, CraftableItemManager craftableItemManager)
        {
            _ammunitionManager = ammunitionManager;
            _materialManager = materialManager;
            _resourceManager = resourceManager;
            _weaponManager = weaponManager;
            _categoryManager = categoryManager;
            _inventoryManager = inventoryManager;
            _craftableItemManager = craftableItemManager;
        }

        public List <CratedItemInput> CratedItemInputs { get; set; } = new List<CratedItemInput>();
        public IList<Ammunition> Ammunitions { get; set; }
        public IList<Material> Materials { get; set; }
        public IList<Resource> Resources { get; set; }
        public IList<Weapon> Weapons { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<CraftableItemDto> CraftableItems { get; set; }
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
            CraftableItems = await _craftableItemManager.GetCraftableItemsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var inputs = JsonSerializer.Deserialize<List<CratedItemInput>>(SelectedItems);
            CratedItemInputs = inputs; 

            var dto = new InventoryDto
            {
                Name = Inventory.Name,
                CratedItems = new List<CratedItemDto>()
            };

            foreach (var input in inputs)
            {             
                CraftableItem? item = await _inventoryManager.getInputItemAsync(input);

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
                return Page();
            }

            await _inventoryManager.CreateInventoryAsync(dto);
            return RedirectToPage("./Index");
        }

       
       
    }
}
