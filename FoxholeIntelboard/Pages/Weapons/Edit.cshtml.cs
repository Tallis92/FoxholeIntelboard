using FoxholeIntelboard.DAL;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;

namespace FoxholeIntelboard.Pages.Weapons
{
    public class EditModel : PageModel
    {
        private readonly IWeaponManager _weaponManager;

        public EditModel(IWeaponManager weaponManager)
        {
            _weaponManager = weaponManager;
        }

        [BindProperty]
        public Weapon Weapon { get; set; } = default!;
        public List<SelectListItem> WeaponTypeOptions { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _weaponManager.GetWeaponByIdAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }
            Weapon = weapon;

            WeaponTypeOptions = Enum.GetValues(typeof(WeaponType))
               .Cast<WeaponType>()
               .Select(d => new SelectListItem
               {
                   Value = d.ToString(),
                   Text = GetEnumDescription(d)
               }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!await WeaponExistsAsync(Weapon.Id))
            {
                return NotFound();
            }

            await _weaponManager.EditWeaponAsync(Weapon);

            return RedirectToPage("./Index");
        }

        private async Task<bool> WeaponExistsAsync(int id)
        {
            var weapons = await _weaponManager.GetWeaponsAsync();
            return weapons.Any(e => e.Id == id);
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }
    }
}
