using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IntelboardAPI.Data;
using IntelboardAPI.Models;

namespace FoxholeIntelboard.Pages.Weapons
{
    public class CreateModel : PageModel
    {
        private readonly IntelboardAPI.Data.IntelboardDbContext _context;

        public CreateModel(IntelboardAPI.Data.IntelboardDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Weapon Weapon { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Weapons.Add(Weapon);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
