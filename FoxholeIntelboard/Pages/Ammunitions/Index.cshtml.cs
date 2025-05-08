using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Data;
using FoxholeIntelboard.Models;

namespace FoxholeIntelboard.Pages.Ammunitions
{
    public class IndexModel : PageModel
    {
        private readonly FoxholeIntelboard.Data.IntelboardDBContext _context;

        public IndexModel(FoxholeIntelboard.Data.IntelboardDBContext context)
        {
            _context = context;
        }

        public IList<Ammunition> Ammunition { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Ammunition = await _context.Ammunitions.ToListAsync();
        }
    }
}
