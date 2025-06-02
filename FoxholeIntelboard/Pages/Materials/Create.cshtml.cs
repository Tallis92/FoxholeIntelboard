using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoxholeIntelboard.DAL;
using IntelboardAPI.Models;
using FoxholeIntelboard.DTO;

namespace FoxholeIntelboard.Pages.Materials
{
    public class CreateModel : PageModel
    {
        private readonly IManagerDto _manager;

        public CreateModel(IManagerDto manager)
        {

            _manager = manager;
        }

        [BindProperty]
        public Material Material { get; set; } = default!;
        [BindProperty]
        public List<Resource> Resources { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Resources = await _manager.ResourceManager.GetResourcesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Material.ProductionCost == null || !Material.ProductionCost.Any())
            {
                ModelState.AddModelError("", "At least one production cost is required.");
                return Page();
            }

            foreach (var cost in Material.ProductionCost)
            {
                cost.CraftableItem = Material;
            }

            await _manager.MaterialManager.CreateMaterialsAsync(Material);

            return RedirectToPage("./Index");
        }
    }
}
