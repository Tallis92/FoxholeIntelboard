using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoxholeIntelboard.Data;
using FoxholeIntelboard.Models;

namespace FoxholeIntelboard.Pages.Materials
{
    public class CreateModel : PageModel
    {
        private readonly FoxholeIntelboard.Data.IntelboardDBContext _context;
        private readonly FoxholeIntelboard.Services.IResourceService _resourceService;

        public CreateModel(FoxholeIntelboard.Data.IntelboardDBContext context, Services.IResourceService resourceService)
        {
            _context = context;
            _resourceService = resourceService;
        }
        [BindProperty]
        public Material Material { get; set; } = default!;
        [BindProperty]
        public List<Resource> Resources { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Resources = await _resourceService.GetResourcesAsync();
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

            _context.Materials.Add(Material);
          
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
