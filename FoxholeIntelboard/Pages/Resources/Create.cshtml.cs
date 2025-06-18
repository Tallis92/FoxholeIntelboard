using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IntelboardCore.DAL;
using IntelboardCore.Models;

namespace FoxholeIntelboard.Pages.Resources
{
    public class CreateModel : PageModel
    {
        private readonly IResourceManager _resourceManager;

        public CreateModel(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }
        [BindProperty]
        public Resource Resource { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _resourceManager.CreateResourceAsync(Resource);

            return RedirectToPage("./Index");
        }
    }
}
