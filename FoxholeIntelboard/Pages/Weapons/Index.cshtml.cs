using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntelboardCore.Models;
using IntelboardCore.DAL;

namespace FoxholeIntelboard.Pages.Weapons
{
    public class IndexModel : PageModel
    {
        private readonly IWeaponManager _weaponManager;

        public IndexModel(IWeaponManager weaponManager)
        {
           _weaponManager = weaponManager;
        }

        public IList<Weapon> Weapons { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Weapons = await _weaponManager.GetWeaponsAsync();
        }
    }
}
