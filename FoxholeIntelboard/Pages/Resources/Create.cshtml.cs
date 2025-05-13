using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoxholeIntelboard.DAL;
using FoxholeIntelboard.Models;

namespace FoxholeIntelboard.Pages.Resources
{
    public class CreateModel : PageModel
    {
        private readonly ResourceManager _resourceManager;

        public CreateModel(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }


        [BindProperty]
        public Resource Resource { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
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
