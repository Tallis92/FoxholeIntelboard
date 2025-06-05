using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Models;
using FoxholeIntelboard.DAL;
using System.ComponentModel;
using System.Reflection;
using FoxholeIntelboard.DTO;

namespace FoxholeIntelboard.Pages.Ammunitions
{
    public class EditModel : PageModel
    {
        private readonly IManagerDto _manager;
        private readonly Ammunition _ammunition;

        public EditModel(IManagerDto manager, Ammunition ammunition)
        {
            _manager = manager;
            _ammunition = ammunition;
        }

        [BindProperty]
        public Ammunition Ammunition { get; set; } = default!;
        public List<Material> Materials { get; set; } = new();
        public List<SelectListItem> DamageTypeOptions { get; set; } = new();
        public List<SelectListItem> AmmoPropertiesOptions { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DamageTypeOptions = Enum.GetValues(typeof(DamageType))
               .Cast<DamageType>()
               .Select(d => new SelectListItem
               {
                   Value = d.ToString(),
                   Text = _ammunition.GetDamageDescription(d)
               }).ToList();

            AmmoPropertiesOptions = Enum.GetValues(typeof(AmmoProperties))
              .Cast<AmmoProperties>()
              .Select(p => new SelectListItem
              {
                  Value = p.ToString(),
                  Text = _ammunition.GetPropertyName(p)
              }).ToList();

            var ammunition = await _manager.AmmunitionManager.GetAmmunitionByIdAsync(id);
            var materials = await _manager.MaterialManager.GetMaterialsAsync();

            if (ammunition == null && materials == null)
            {
                return NotFound();
            }
            Materials = materials;
            Ammunition = ammunition;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!await AmmunitionExistsAsync(Ammunition.Id))
            {
                return NotFound();
            }
          
            await _manager.AmmunitionManager.EditAmmunitionAsync(Ammunition);


            return RedirectToPage("./Index");
        }

        private async Task<bool> AmmunitionExistsAsync(int id)
        {
            var ammunitions = await _manager.AmmunitionManager.GetAmmunitionsAsync();
            return  ammunitions.Any(m => m.Id == id);
        }

    }
}
