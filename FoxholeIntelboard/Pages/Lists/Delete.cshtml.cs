using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
//using IntelboardAPI.Data;
using IntelboardCore.Models;
using IntelboardCore.DTO;
using IntelboardCore.DAL.Interfaces;

namespace FoxholeIntelboard.Pages.Lists
{
    public class DeleteModel : PageModel
    {
        private readonly IInventoryManager _inventoryManager;

        public DeleteModel(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        [BindProperty]
        public InventoryDto Inventory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _inventoryManager.GetInventoryByIdAsync(id);

            if (inventory is not null)
            {
                Inventory = inventory;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _inventoryManager.DeleteInventoryAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
