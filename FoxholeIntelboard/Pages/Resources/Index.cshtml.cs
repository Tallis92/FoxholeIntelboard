using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Models;
using FoxholeIntelboard.DAL;

namespace FoxholeIntelboard.Pages.Resources
{
    public class IndexModel : PageModel
    {
        private readonly ResourceManager _resourceManager;
        public IndexModel(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public IList<Resource> Resource { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Resource = await _resourceManager.GetResourcesAsync();
        }
        public async Task<IActionResult> OnPostSeedResourcesAsync()
        {
            await _resourceManager.SeedResourcesAsync();
            return RedirectToPage();
        }
    }
}
