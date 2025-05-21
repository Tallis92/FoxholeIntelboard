using FoxholeIntelboard.DAL;
using IntelboardAPI.Data;
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
    public class CreateModel : PageModel
    {
        private readonly AmmunitionManager _ammunitionManager;
        private readonly MaterialManager _materialManager;
        private readonly ResourceManager _resourceManager;
        private readonly WeaponManager _weaponManager;
        private readonly IntelboardDbContext _context;

        public IList<Ammunition> Ammunitions { get; set; }
        public IList<Material> Materials { get; set; }
        public IList<Resource> Resources { get; set; }
        public IList<Weapon> Weapons { get; set; }
        public IList<Category> Categories { get; set; }
        [BindProperty]
        public Inventory Inventory { get; set; }
        [BindProperty]
        public List<CratedItem> SelectedItems { get; set; } = new();

        public CreateModel(AmmunitionManager ammunitionManager, IntelboardDbContext context, MaterialManager materialManager, ResourceManager resourceManager, WeaponManager weaponManager)
        {
            _ammunitionManager = ammunitionManager;
            _context = context;
            _materialManager = materialManager;
            _resourceManager = resourceManager;
            _weaponManager = weaponManager;
        }

        public async Task OnGetAsync()
        {
            Ammunitions = await _ammunitionManager.GetAmmunitionsAsync();
            Resources = await _resourceManager.GetResourcesAsync();
            Materials = await _materialManager.GetMaterialsAsync();
            Weapons = await _weaponManager.GetWeaponsAsync();
            Categories = await _context.Categories.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Inventories.Add(Inventory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public class CratedItemInput
        {
            public int Id { get; set; }
            public int Amount { get; set; }
        }
    }
}
