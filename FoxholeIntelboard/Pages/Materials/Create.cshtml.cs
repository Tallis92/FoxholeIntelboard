using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoxholeIntelboard.DAL;
using IntelboardAPI.Models;

namespace FoxholeIntelboard.Pages.Materials
{
    public class CreateModel : PageModel
    {
        private readonly ResourceManager _resourceManager;
        private readonly MaterialManager _materialManager;

        public CreateModel(ResourceManager resourceManager, MaterialManager materialManager)
        {
            _resourceManager = resourceManager;
            _materialManager = materialManager;
        }
        [BindProperty]
        public Material Material { get; set; } = default!;
        [BindProperty]
        public List<Resource> Resources { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Resources = await _resourceManager.GetResourcesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                if (Material.ProductionCost == null || !Material.ProductionCost.Any())
                {
                    Material.ProductionCost = new List<Cost> { new Cost() };
                }

                return Page();
            }

            foreach (var cost in Material.ProductionCost)
            {
                cost.CraftableItem = Material;
            }

            await _materialManager.CreateMaterialsAsync(Material);

            return RedirectToPage("./Index");
        }
    }
}
