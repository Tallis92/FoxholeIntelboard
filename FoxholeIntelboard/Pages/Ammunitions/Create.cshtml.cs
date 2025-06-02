using FoxholeIntelboard.DAL;
using FoxholeIntelboard.DTO;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FoxholeIntelboard.Pages.Ammunitions
{

    public class CreateModel : PageModel
    {

        private readonly IManagerDto _manager;
        private readonly Ammunition _ammunition;

        public CreateModel(IManagerDto manager, Ammunition ammunition)
        {
            _manager = manager;
            _ammunition = ammunition;
        }

        [BindProperty]
        public Ammunition Ammunition { get; set; } = default!;
        [BindProperty]
        public List<Material> Materials { get; set; } = new();
        public List<SelectListItem> DamageTypeOptions { get; set; } = new();
        public List<SelectListItem> AmmoPropertiesOptions { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Materials = await _manager.MaterialManager.GetMaterialsAsync();


            // Checks through the Enums in the Ammunition object, then retrieves the description attributes to display in a dropdown list in
            // the view page.
            DamageTypeOptions = Enum.GetValues(typeof(DamageType))
                .Cast<DamageType>()
                .Select(d => new SelectListItem
                {
                    Value = d.ToString(),
                    Text = _ammunition.GetDamageDescription(d)
                }).ToList();

            // Checks through the Enums in the Ammunition object, then retrieves the name attributes to display in a dropdown list in
            // the view page.
            AmmoPropertiesOptions = Enum.GetValues(typeof(AmmoProperties))
               .Cast<AmmoProperties>()
               .Select(p => new SelectListItem
               {
                   Value = p.ToString(),
                   Text = _ammunition.GetPropertyName(p)
               }).ToList();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // If the ammunitions Production cost is either 0 or null, return an error message.
            if (Ammunition.ProductionCost == null || !Ammunition.ProductionCost.Any())
            {
                ModelState.AddModelError("", "At least one production cost is required.");
                return Page();
            }

            foreach (var cost in Ammunition.ProductionCost)
            {
                cost.CraftableItem = Ammunition;
            }

            await _manager.AmmunitionManager.CreateAmmunitionAsync(Ammunition);

            return RedirectToPage("./Index");
        }

    }
}
