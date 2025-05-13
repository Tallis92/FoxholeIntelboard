using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Models;
using FoxholeIntelboard.DAL;

namespace FoxholeIntelboard.Pages.Ammunitions
{
    public class DetailsModel : PageModel
    {
        private readonly AmmunitionManager _ammunitionManager;
        public DetailsModel(AmmunitionManager ammunitionManager)
        {
            _ammunitionManager = ammunitionManager;
        }

        public Ammunition Ammunition { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ammunition = await _ammunitionManager.GetAmmunitionByIdAsync(id);

            if (ammunition is not null)
            {
                Ammunition = ammunition;

                return Page();
            }

            return NotFound();
        }
    }
}
