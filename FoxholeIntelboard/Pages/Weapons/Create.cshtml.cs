using FoxholeIntelboard.DAL;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;

namespace FoxholeIntelboard.Pages.Weapons
{
    public class CreateModel : PageModel
    {
        private readonly MaterialManager _materialManager;
        private readonly WeaponManager _weaponManager;

        public CreateModel(WeaponManager weaponManager, MaterialManager materialManager)
        {
            _weaponManager = weaponManager;
            _materialManager = materialManager;
        }
        [BindProperty]
        public Weapon Weapon { get; set; } = default!;

        [BindProperty]
        public List<Material> Materials { get; set; } = default!;

        public List<SelectListItem> WeaponTypeOptions { get; set; } = new();
        public List<SelectListItem> SpecialPropertiesOptions { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {
            Materials = await _materialManager.GetMaterialsAsync();
            WeaponTypeOptions = Enum.GetValues(typeof(WeaponType))
                .Cast<WeaponType>()
                .Select(d => new SelectListItem
                {
                    Value = d.ToString(),
                    Text = GetEnumDescription(d)
                }).ToList();

            SpecialPropertiesOptions = Enum.GetValues(typeof(SpecialProperties))
                .Cast<SpecialProperties>()
                .Select(p => new SelectListItem
                {
                    Value = p.ToString(),
                    Text = GetEnumDescription(p)
                }).ToList();
            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Weapon.ProductionCost == null || !Weapon.ProductionCost.Any())
            {
                ModelState.AddModelError("", "At least one production cost is required.");
                return Page();
            }
            if (Weapon.WeaponType == null)
            {
                ModelState.AddModelError("", "A weapon typ must be selected.");
                return Page();
            }

            await _weaponManager.CreateWeaponAsync(Weapon);
            return RedirectToPage("./Index");
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }
    }
}
