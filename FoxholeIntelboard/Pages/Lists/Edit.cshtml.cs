using FoxholeIntelboard.DAL;
using IntelboardAPI.Data;
using IntelboardAPI.DTO;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IntelboardDbContext _context;
        private readonly AmmunitionManager _ammunitionManager;
        private readonly MaterialManager _materialManager;
        private readonly ResourceManager _resourceManager;
        private readonly WeaponManager _weaponManager;
        private readonly CategoryManager _categoryManager;
        private readonly InventoryManager _inventoryManager;

        public EditModel(AmmunitionManager ammunitionManager, IntelboardDbContext context, MaterialManager materialManager,
            ResourceManager resourceManager, WeaponManager weaponManager, CategoryManager categoryManager, InventoryManager inventoryManager)
        {
            _ammunitionManager = ammunitionManager;
            _materialManager = materialManager;
            _resourceManager = resourceManager;
            _weaponManager = weaponManager;
            _categoryManager = categoryManager;
            _inventoryManager = inventoryManager;
            _context = context;
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

        public List<CratedItemInput> CratedItems { get; set; } = new List<CratedItemInput>();


        public async Task<IActionResult> OnGetAsync(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.CratedItems)
                    .ThenInclude(ci => ci.CraftableItem)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventory == null)
            {
                return NotFound();
            }

            Inventory = inventory;
            Ammunitions = await _ammunitionManager.GetAmmunitionsAsync();
            Resources = await _resourceManager.GetResourcesAsync();
            Materials = await _materialManager.GetMaterialsAsync();
            Weapons = await _weaponManager.GetWeaponsAsync();
            Categories = await _categoryManager.GetCategoriesAsync();

            foreach (var item in inventory.CratedItems)
            {
                CratedItemInput selectItem = new CratedItemInput();
                selectItem.Id = item.CraftableItem.Id;
                selectItem.Name = item.CraftableItem.Name;
                selectItem.Amount = item.Amount;
                switch (item.CraftableItem)
                {
                    case Ammunition ammo:
                        selectItem.Type = "Ammunition";
                        break;
                    case Weapon weapon:
                        selectItem.Type = "Weapon";
                        break;
                    case Material material:
                        selectItem.Type = "Material";
                        break;
                    default:
                        Console.WriteLine("Itemtype could not be found");
                        break;
                }
                CratedItems.Add(selectItem);
            }
            JsonSerializer.Serialize<List<CratedItemInput>>(CratedItems);
           
            return Page();
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
                InventoryId = Inventory.Id,
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
                    Description = $"A crate of {input.Amount}x {item.Name}. Submit to a stockpile or seaport."
                });
            }
            if (!await InventoryExistsAsync(Inventory.Id))
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
