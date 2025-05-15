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

namespace FoxholeIntelboard.Pages.Ammunitions
{
    public class EditModel : PageModel
    {
        private readonly AmmunitionManager _ammunitionManager;

        public EditModel(AmmunitionManager ammunitionManager)
        {
            _ammunitionManager = ammunitionManager;
        }

        [BindProperty]
        public Ammunition Ammunition { get; set; } = default!;
        public List<SelectListItem> DamageTypeOptions { get; set; } = new();

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
                   Text = GetEnumDescription(d)
               }).ToList();

            var ammunition = await _ammunitionManager.GetAmmunitionByIdAsync(id);
            if (ammunition == null)
            {
                return NotFound();
            }
            Ammunition = ammunition;
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
            if (!await AmmunitionExistsAsync(Ammunition.Id))
            {
                return NotFound();
            }
          
            await _ammunitionManager.EditAmmunitionAsync(Ammunition);


            return RedirectToPage("./Index");
        }

        private async Task<bool> AmmunitionExistsAsync(int id)
        {

            var ammunitions = await _ammunitionManager.GetAmmunitionsAsync();
            return  ammunitions.Any(m => m.Id == id);
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }
    }
}
