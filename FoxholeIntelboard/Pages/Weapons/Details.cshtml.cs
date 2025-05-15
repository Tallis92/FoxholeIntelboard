using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Data;
using IntelboardAPI.Models;

namespace FoxholeIntelboard.Pages.Weapons
{
    public class DetailsModel : PageModel
    {
        private readonly IntelboardAPI.Data.IntelboardDbContext _context;

        public DetailsModel(IntelboardAPI.Data.IntelboardDbContext context)
        {
            _context = context;
        }

        public Weapon Weapon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapons.FirstOrDefaultAsync(m => m.Id == id);

            if (weapon is not null)
            {
                Weapon = weapon;

                return Page();
            }

            return NotFound();
        }
    }
}
