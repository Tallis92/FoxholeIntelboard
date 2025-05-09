using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Data;
using FoxholeIntelboard.Models;
using FoxholeIntelboard.Services;

namespace FoxholeIntelboard.Pages.Materials
{
    public class IndexModel : PageModel
    {
        private readonly FoxholeIntelboard.Data.IntelboardDBContext _context;
        private readonly IMaterialService _materialService;

        public IndexModel(FoxholeIntelboard.Data.IntelboardDBContext context, IMaterialService materialService)
        {
            _context = context;
            _materialService = materialService;
        }

        public IList<Material> Material { get; set; }

        public async Task OnGetAsync()
        {
            Material = await _context.Materials.ToListAsync();
        }

        public async Task<IActionResult> OnPostSeedMaterialsAsync()
        {
            await _materialService.SeedMaterialsAsync();
            return RedirectToPage();
        }

    }
}
