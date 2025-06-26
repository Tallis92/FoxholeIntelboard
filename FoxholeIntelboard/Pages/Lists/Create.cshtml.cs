using IntelboardCore.DAL;
using IntelboardCore.DTO;
//using IntelboardAPI.Data;
using IntelboardCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using IntelboardCore.DTO.Interfaces;

namespace FoxholeIntelboard.Pages.Lists
{
    public class CreateModel : PageModel
    {
        private readonly IManagerDto _manager;

        public CreateModel(IManagerDto manager)
        {
            _manager = manager;
        }

        public IList<Ammunition> Ammunitions { get; set; } = new List<Ammunition>();
        public IList<Material> Materials { get; set; } = new List<Material>();
        public IList<Resource> Resources { get; set; } = new List<Resource>();
        public IList<Weapon> Weapons { get; set; } = new List<Weapon>();
        public IList<Category> Categories { get; set; } = new List<Category>();
        public IList<CraftableItemDto> CraftableItems { get; set; } = new List<CraftableItemDto>();
        [BindProperty]
        public Inventory Inventory { get; set; }

        [BindProperty]
        public string SelectedItems { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedFactionId { get; set; } = 0;

        public async Task OnGetAsync()
        {
            await LoadDataAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(SelectedItems))
            {
                ModelState.Remove("SelectedItems"); // Clears any previous auto-errors
                ModelState.AddModelError("SelectedItems", "You must select at least one item.");
            }

            if (!ModelState.IsValid)
            {
                await LoadDataAsync();
                return Page();
            }

            // Converts SelectedItems string to a list of CratedItemInput so we can loop through all the items and add it to InventoryDto with
            // all the relevant data.
            var inputs = JsonSerializer.Deserialize<List<CratedItemInput>>(SelectedItems);

            var dto = new InventoryDto
            {
                Name = Inventory.Name,
                FactionId = SelectedFactionId,
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
                    Amount = 0,
                    RequiredAmount = input.Amount,
                    Description = $"A crate of {input.Amount}x {item.Name}(s). Submit to a stockpile or seaport."
                });
            }

            await _manager.InventoryManager.CreateInventoryAsync(dto);

            return RedirectToPage("./Index");
        }

        private async Task LoadDataAsync()
        {
            Ammunitions = await _manager.AmmunitionManager.GetAmmunitionsAsync();
            Resources = await _manager.ResourceManager.GetResourcesAsync();
            Materials = await _manager.MaterialManager.GetMaterialsAsync();
            Weapons = await _manager.WeaponManager.GetWeaponsAsync();
            Categories = await _manager.CategoryManager.GetCategoriesAsync();
            CraftableItems = await _manager.CraftableItemManager.GetCraftableItemsAsync();
        }

    }
}
