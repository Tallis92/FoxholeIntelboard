using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Data;
using FoxholeIntelboard.Models;

namespace FoxholeIntelboard.Pages.Materials
{
    public class IndexModel : PageModel
    {
        private readonly FoxholeIntelboard.Data.IntelboardDBContext _context;

        public IndexModel(FoxholeIntelboard.Data.IntelboardDBContext context)
        {
            _context = context;
        }

        public IList<Material> Material { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Material = await _context.Materials.ToListAsync();
        }
    }
}
