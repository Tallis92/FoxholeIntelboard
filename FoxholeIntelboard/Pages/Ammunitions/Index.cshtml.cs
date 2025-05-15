using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Models;
using FoxholeIntelboard.DAL;

namespace FoxholeIntelboard.Pages.Ammunitions
{
    public class IndexModel : PageModel
    {
        private readonly AmmunitionManager _ammunitionManager;

        public IndexModel(AmmunitionManager ammunitionManger)
        {
            _ammunitionManager = ammunitionManger;
        }

        public IList<Ammunition> Ammunition { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Ammunition = await _ammunitionManager.GetAmmunitionsAsync();
        }

        public async Task<IActionResult> OnPostSeedAmmunitionsAsync()
        {
            await _ammunitionManager.SeedAmmunitionsAsync();
            return RedirectToPage();
        }
    }
}
