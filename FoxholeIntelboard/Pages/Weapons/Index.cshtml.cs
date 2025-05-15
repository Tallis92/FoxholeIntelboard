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
    public class IndexModel : PageModel
    {
        private readonly IntelboardAPI.Data.IntelboardDbContext _context;

        public IndexModel(IntelboardAPI.Data.IntelboardDbContext context)
        {
            _context = context;
        }

        public IList<Weapon> Weapon { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Weapon = await _context.Weapons.ToListAsync();
        }
    }
}
