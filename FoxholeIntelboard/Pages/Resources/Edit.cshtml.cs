﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Data;
using FoxholeIntelboard.Models;

namespace FoxholeIntelboard.Pages.Resources
{
    public class EditModel : PageModel
    {
        private readonly FoxholeIntelboard.Data.IntelboardDBContext _context;

        public EditModel(FoxholeIntelboard.Data.IntelboardDBContext context)
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

            var resource =  await _context.Resources.FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }
            Resource = resource;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Resource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(Resource.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ResourceExists(int id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}
