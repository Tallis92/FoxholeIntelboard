using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Data;
using IntelboardAPI.Models;

namespace FoxholeIntelboard.Pages.Lists
{
    public class DetailsModel : PageModel
    {
        private readonly IntelboardAPI.Data.IntelboardDbContext _context;

        public DetailsModel(IntelboardAPI.Data.IntelboardDbContext context)
        {
            _context = context;
        }

        public Inventory Inventory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FirstOrDefaultAsync(m => m.InventoryId == id);

            if (inventory is not null)
            {
                Inventory = inventory;

                return Page();
            }

            return NotFound();
        }
    }
}
