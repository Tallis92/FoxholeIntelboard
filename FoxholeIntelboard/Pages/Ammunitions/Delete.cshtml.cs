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
    public class DeleteModel : PageModel
    {
        private readonly FoxholeIntelboard.Data.IntelboardDBContext _context;

        public DeleteModel(FoxholeIntelboard.Data.IntelboardDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ammunition Ammunition { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ammunition = await _context.Ammunitions.FirstOrDefaultAsync(m => m.Id == id);

            if (ammunition is not null)
            {
                Ammunition = ammunition;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ammunition = await _context.Ammunitions.FindAsync(id);
            if (ammunition != null)
            {
                Ammunition = ammunition;
                _context.Ammunitions.Remove(Ammunition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
