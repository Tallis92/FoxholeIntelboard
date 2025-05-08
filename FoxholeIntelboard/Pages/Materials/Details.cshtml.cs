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
    public class DetailsModel : PageModel
    {
        private readonly FoxholeIntelboard.Data.IntelboardDBContext _context;

        public DetailsModel(FoxholeIntelboard.Data.IntelboardDBContext context)
        {
            _context = context;
        }

        public Material Material { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == id);

            if (material is not null)
            {
                Material = material;

                return Page();
            }

            return NotFound();
        }
    }
}
