using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntelboardCore.Models;
using IntelboardCore.DAL.Interfaces;

namespace FoxholeIntelboard.Pages.Weapons
{
    public class DetailsModel : PageModel
    {
        private readonly IWeaponManager _weaponManager;

        public DetailsModel(IWeaponManager weaponManager)
        {
            _weaponManager = weaponManager;
        }

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
    }
}
