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
  
        public IList<InventoryDto> inventoryDtos { get; set; } 


        public async Task<IActionResult> OnGetAsync(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Ammunitions = await _ammunitionManager.GetAmmunitionsAsync();
            Resources = await _resourceManager.GetResourcesAsync();
            Materials = await _materialManager.GetMaterialsAsync();
            Weapons = await _weaponManager.GetWeaponsAsync();
            Categories = await _categoryManager.GetCategoriesAsync();

            var inventory = await _context.Inventories
                .Include(i => i.CratedItems)
                    .ThenInclude(ci => ci.CraftableItem) // <-- Den här lägger till namn etc.
                .FirstOrDefaultAsync(i => i.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }
            Inventory = inventory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(Inventory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InventoryExists(Guid id)
        {
            return _context.Inventories.Any(e => e.Id == id);
        }
    }
}
