using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Data;
using FoxholeIntelboard.Models;
using FoxholeIntelboard.Services;

namespace FoxholeIntelboard.Pages.Resources
{
    public class IndexModel : PageModel
    {
        private readonly IResourceService _resourceService;
        private readonly IntelboardDBContext _context;

        public IndexModel(IntelboardDBContext context, IResourceService resourceService)
        {
            _context = context;
            _resourceService = resourceService;
        }

        public IList<Resource> Resource { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Resource = await _resourceService.GetResourcesAsync();
        }
        public async Task<IActionResult> OnPostSeedResourcesAsync()
        {
            await _resourceService.SeedResourcesAsync();
            return RedirectToPage();
        }
    }
}
