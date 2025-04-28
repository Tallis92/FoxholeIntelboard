﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Data;
using FoxholeIntelboard.Models;

namespace FoxholeIntelboard.Pages.Resources
{
    public class DeleteModel : PageModel
    {
        private readonly FoxholeIntelboard.Data.IntelboardDBContext _context;

        public DeleteModel(FoxholeIntelboard.Data.IntelboardDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Resource Resource { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources.FirstOrDefaultAsync(m => m.Id == id);

            if (resource is not null)
            {
                Resource = resource;

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

            var resource = await _context.Resources.FindAsync(id);
            if (resource != null)
            {
                Resource = resource;
                _context.Resources.Remove(Resource);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
