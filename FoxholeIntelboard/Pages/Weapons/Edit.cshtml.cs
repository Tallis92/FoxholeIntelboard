using FoxholeIntelboard.DAL;
using FoxholeIntelboard.DTO;
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
        private readonly IManagerDto _manager;
        private readonly Weapon _weapon;
        public EditModel(IManagerDto manager, Weapon weapon)
        {
            _manager = manager;
            _weapon = weapon;
        }

        [BindProperty]
        public Weapon Weapon { get; set; } = default!;
        public List<Material> Materials { get; set; } = new ();
        public List<SelectListItem> WeaponTypeOptions { get; set; } = new();
        public List<SelectListItem> WeaponPropertiesOptions { get; set; } = new();
        public List<Ammunition> AmmunitionsOptions { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _manager.WeaponManager.GetWeaponByIdAsync(id);
            var ammunitions = await _manager.AmmunitionManager.GetAmmunitionsAsync();
            var materials = await _manager.MaterialManager.GetMaterialsAsync();
            if (weapon == null && ammunitions == null)
            {
                return NotFound();
            }
            Weapon = weapon;
            AmmunitionsOptions = ammunitions;
            Materials = materials;

            WeaponTypeOptions = Enum.GetValues(typeof(WeaponType))
               .Cast<WeaponType>()
               .Select(d => new SelectListItem
               {
                   Value = d.ToString(),
                   Text = _weapon.GetWeaponName(d)
               }).ToList();

            WeaponPropertiesOptions = Enum.GetValues(typeof(WeaponProperties))
                .Cast<WeaponProperties>()
                .Select(p => new SelectListItem
                {
                    Value = p.ToString(),
                    Text = _weapon.GetWeaponName(p)
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

            await _manager.WeaponManager.EditWeaponAsync(Weapon);

            return RedirectToPage("./Index");
        }

        private async Task<bool> WeaponExistsAsync(int id)
        {
            var weapons = await _manager.WeaponManager.GetWeaponsAsync();
            return weapons.Any(e => e.Id == id);
        }

    }
}
