using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Models;
using FoxholeIntelboard.DAL;

namespace FoxholeIntelboard.Pages.Ammunitions
{
    public class DeleteModel : PageModel
    {
        private readonly IAmmunitionManager _ammunitionManager;

        public DeleteModel(IAmmunitionManager ammunitionManger)
        {
           _ammunitionManager = ammunitionManger;
        }

        [BindProperty]
        public Ammunition Ammunition { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
          
            var ammunition = await _ammunitionManager.GetAmmunitionByIdAsync(id);

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

            await _ammunitionManager.DeleteAmmunitionAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
