using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Models;
using FoxholeIntelboard.DAL;

namespace FoxholeIntelboard.Pages.Ammunitions
{
    public class DeleteModel : PageModel
    {
        private readonly AmmunitionManager _ammunitionManager;

        public DeleteModel(AmmunitionManager ammunitionManger)
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

            // TODO: Gör om detta till en metod i AmmunitionManager
            var ammunitions = await _ammunitionManager.GetAmmunitionsAsync();
            var ammunition = ammunitions.FirstOrDefault(c => c.Id == id);

            if (ammunition is not null)
            {
                Ammunition = ammunition;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _ammunitionManager.DeleteAmmunitionAsync(id);


            return RedirectToPage("./Index");
        }
    }
}
