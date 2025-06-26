using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntelboardCore.Models;
using IntelboardCore.DAL.Interfaces;

namespace FoxholeIntelboard.Pages.Resources
{
    public class IndexModel : PageModel
    {
        private readonly IResourceManager _resourceManager;
        public IndexModel(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public IList<Resource> Resource { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Resource = await _resourceManager.GetResourcesAsync();
        }
    }
}
