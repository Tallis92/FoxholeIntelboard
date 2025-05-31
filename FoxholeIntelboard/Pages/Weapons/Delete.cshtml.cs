using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.DAL;
using IntelboardAPI.Models;

namespace FoxholeIntelboard.Pages.Weapons
{
    public class DeleteModel : PageModel
    {
        private readonly IWeaponManager _weaponManager;

        public DeleteModel(IWeaponManager weaponManager)
        {
            _weaponManager = weaponManager;
        }

        [BindProperty]
        public Weapon Weapon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _weaponManager.GetWeaponByIdAsync(id);

            if (weapon is not null)
            {
                Weapon = weapon;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _weaponManager.GetWeaponByIdAsync(id);
            if (weapon != null)
            {
                Weapon = weapon;
                await _weaponManager.DeleteWeaponAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
