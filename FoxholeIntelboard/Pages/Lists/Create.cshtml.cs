using FoxholeIntelboard.DAL;
using IntelboardAPI.DTO;
using IntelboardAPI.Data;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using FoxholeIntelboard.DTO;

namespace FoxholeIntelboard.Pages.Lists
{
    public class CreateModel : PageModel
    {
        private readonly IManagerDto _manager;

        public CreateModel(IManagerDto manager)
        {
            _manager = manager;
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

        [BindProperty(SupportsGet = true)]
        public int? SelectedFactionId { get; set; }

        public async Task OnGetAsync()
        {
            Ammunitions = await _manager.AmmunitionManager.GetAmmunitionsAsync();
            Resources = await _manager.ResourceManager.GetResourcesAsync();
            Materials = await _manager.MaterialManager.GetMaterialsAsync();
            Weapons = await _manager.WeaponManager.GetWeaponsAsync();
            Categories = await _manager.CategoryManager.GetCategoriesAsync();
            CraftableItems = await _manager.CraftableItemManager.GetCraftableItemsAsync();
            if (SelectedFactionId == null)
                SelectedFactionId = 0;

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
                    RequiredAmount = input.RequiredAmount,
                    Description = $"A crate of {input.Amount}x {item.Name}. Submit to a stockpile or seaport."
                });
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _manager.InventoryManager.CreateInventoryAsync(dto);
            return RedirectToPage("./Index");
        }

       
       
    }
}
