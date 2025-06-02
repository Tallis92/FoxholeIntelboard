using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Data;
using IntelboardAPI.Models;
using FoxholeIntelboard.DAL;
using IntelboardAPI.DTO;
using FoxholeIntelboard.DTO;

namespace FoxholeIntelboard.Pages.Lists
{
    public class DetailsModel : PageModel
    {
        private readonly IManagerDto _manager;

        public DetailsModel(IManagerDto manager)
        {
           _manager = manager;
        }

        public InventoryDto Inventory { get; set; } = default!;
        public IList<CraftableItemDto> CraftableItems { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _manager.InventoryManager.GetInventoryByIdAsync(id);
            var craftableItems = await _manager.CraftableItemManager.GetCraftableItemsAsync();

            if (inventory is not null && craftableItems is not null)
            {
                Inventory = inventory;
                CraftableItems = craftableItems;

                return Page();
            }

            return NotFound();
        }
    }
}
