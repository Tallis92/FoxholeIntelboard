﻿using IntelboardCore.DAL;
using IntelboardCore.DTO;
using IntelboardCore.DTO.Interfaces;

//using IntelboardAPI.Data;
using IntelboardCore.Models;
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
        private readonly IManagerDto _manager;

        public EditModel(IManagerDto manager)
        {
            _manager = manager;
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
        public int? SelectedFactionId { get; set; } = 0;


        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            var inventory = await _manager.InventoryManager.GetInventoryByIdAsync(id);
            if (inventory == null)
                return NotFound();

            Inventory = inventory;
            Weapons = await _manager.WeaponManager.GetWeaponsAsync();

            // Get FactionId to determine what faction items to populate the dropdowns with.
            SelectedFactionId = Inventory.FactionId;
            Weapons = Weapons.Where(w => w.FactionId == SelectedFactionId).ToList();

            Ammunitions = await _manager.AmmunitionManager.GetAmmunitionsAsync();
            Resources = await _manager.ResourceManager.GetResourcesAsync();
            Materials = await _manager.MaterialManager.GetMaterialsAsync();
            Categories = await _manager.CategoryManager.GetCategoriesAsync();
            CraftableItems = await _manager.CraftableItemManager.GetCraftableItemsAsync();

            
            foreach (var item in inventory.CratedItems)
            {
                CratedItemInput selectItem = new CratedItemInput();
                selectItem.Id = item.CraftableItemId;
                selectItem.Amount = item.Amount;
                selectItem.RequiredAmount = item.RequiredAmount;
                selectItem.Type = item.Type;
                selectItem.Name = CraftableItems.Where(c => c.CraftableItemId == item.CraftableItemId).Select(c => c.Name).FirstOrDefault();
                CratedItems.Add(selectItem);
            }

            JsonSerializer.Serialize<List<CratedItemInput>>(CratedItems);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Removes Crateitems from modelstate as it only caused issues. Still saves correctly into db after removal.
            ModelState.Remove("Inventory.CratedItems");
            if (string.IsNullOrWhiteSpace(SelectedItems))
            {
                ModelState.Remove("SelectedItems"); // Clears any previous auto-errors
                ModelState.AddModelError("SelectedItems", "You must select at least one item.");
            }
            if (!ModelState.IsValid)
                return Page();

            if (!await InventoryExistsAsync(Inventory.InventoryId))
            {
                return NotFound();
            }
            var inputs = JsonSerializer.Deserialize<List<CratedItemInput>>(SelectedItems);

            var dto = new InventoryDto
            {
                InventoryId = Inventory.InventoryId,
                Name = Inventory.Name,
                CratedItems = new List<CratedItemDto>()
            };

            foreach (var input in inputs)
            {
                CraftableItem? item = await _manager.InventoryManager.getInputItemAsync(input);

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
                    RequiredAmount = input.RequiredAmount,
                    Description = $"A crate of {input.Amount}x {item.Name}. Submit to a stockpile or seaport.",
                });
            }

            await _manager.InventoryManager.EditInventoryAsync(dto);

            return RedirectToPage("./Index");
        }

        private async Task<bool> InventoryExistsAsync(Guid id)
        {
            var inventories = await _manager.InventoryManager.GetInventoriesAsync();
            return inventories.Any(e => e.InventoryId == id);
        }
    }
}
