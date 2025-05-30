using FoxholeIntelboard.DAL;
using IntelboardAPI.Data;
using IntelboardAPI.DTO;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoxholeIntelboard.Pages.Lists
{
    public class EditModel : PageModel
    {
        private readonly AmmunitionManager _ammunitionManager;
        private readonly MaterialManager _materialManager;
        private readonly ResourceManager _resourceManager;
        private readonly WeaponManager _weaponManager;
        private readonly CategoryManager _categoryManager;
        private readonly InventoryManager _inventoryManager;
        private readonly CraftableItemManager _craftableItemManager;

        public EditModel(AmmunitionManager ammunitionManager, MaterialManager materialManager, ResourceManager resourceManager,
            WeaponManager weaponManager, CategoryManager categoryManager, InventoryManager inventoryManager, CraftableItemManager craftableItemManager)
        {
            _ammunitionManager = ammunitionManager;
            _materialManager = materialManager;
            _resourceManager = resourceManager;
            _weaponManager = weaponManager;
            _categoryManager = categoryManager;
            _inventoryManager = inventoryManager;
            _craftableItemManager = craftableItemManager;
        }

        public IList<Ammunition> Ammunitions { get; set; }
        public IList<Material> Materials { get; set; }
        public IList<Resource> Resources { get; set; }
        public IList<Weapon> Weapons { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<CraftableItemDto> CraftableItems { get; set; }
        [BindProperty]
        public InventoryDto Inventory { get; set; }
        [BindProperty]
        public string SelectedItems { get; set; }
        [BindNever]
        public List<CratedItemInput> CratedItems { get; set; } = new List<CratedItemInput>();

        [BindProperty(SupportsGet = true)]
        public int? SelectedFactionId { get; set; }

        public string FactionName => SelectedFactionId == 0 ? "Wardens" : "Colonials";


        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            var inventory = await _inventoryManager.GetInventoryByIdAsync(id);
            if (inventory == null)
                return NotFound();

            Inventory = inventory;
            Weapons = await _weaponManager.GetWeaponsAsync();

            //
            if (inventory.CratedItems != null && inventory.CratedItems.Any())
            {
                // Get first weaponid from CratedItems
                var firstCratedItem = inventory.CratedItems.FirstOrDefault();
                if (firstCratedItem != null)
                {
                    var weapon = Weapons.FirstOrDefault(w => w.Id == firstCratedItem.CraftableItemId);
                    if (weapon != null)
                        SelectedFactionId = weapon.FactionId;
                }
            }

            
            if (SelectedFactionId == null)
                SelectedFactionId = 0;

            // Filter weapons depending on faction
            Weapons = Weapons.Where(w => w.FactionId == SelectedFactionId).ToList();

            Ammunitions = await _ammunitionManager.GetAmmunitionsAsync();
            Resources = await _resourceManager.GetResourcesAsync();
            Materials = await _materialManager.GetMaterialsAsync();
            Categories = await _categoryManager.GetCategoriesAsync();
            CraftableItems = await _craftableItemManager.GetCraftableItemsAsync();

            
            foreach (var item in inventory.CratedItems)
            {
                CratedItemInput selectItem = new CratedItemInput();
                selectItem.Id = item.CraftableItemId;
                selectItem.Amount = item.Amount;
                selectItem.Type = item.Type;
                selectItem.Name = CraftableItems.Where(c => c.CraftableItemId == item.CraftableItemId).Select(c => c.Name).FirstOrDefault();
                CratedItems.Add(selectItem);
            }

            JsonSerializer.Serialize<List<CratedItemInput>>(CratedItems);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Inventory.CratedItems");
            if (!ModelState.IsValid)
                return Page();

            var inputs = JsonSerializer.Deserialize<List<CratedItemInput>>(SelectedItems);

            var dto = new InventoryDto
            {
                InventoryId = Inventory.InventoryId,
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
                    CraftableItemId = item.Id,
                    Amount = input.Amount,
                    Description = $"A crate of {input.Amount}x {item.Name}. Submit to a stockpile or seaport.",
                });
            }
            if (!await InventoryExistsAsync(Inventory.InventoryId))
            {
                return NotFound();
            }

            await _inventoryManager.EditInventoryAsync(dto);

            return RedirectToPage("./Index");
        }

        private async Task<bool> InventoryExistsAsync(Guid id)
        {
            var inventories = await _inventoryManager.GetInventoriesAsync();
            return inventories.Any(e => e.InventoryId == id);
        }
    }
}
