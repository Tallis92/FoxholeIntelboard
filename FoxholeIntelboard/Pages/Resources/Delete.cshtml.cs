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
    public class DeleteModel : PageModel
    {
        private readonly IResourceManager _resourceManager;

        public DeleteModel(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        [BindProperty]
        public Resource Resource { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
          
            var resource = await _resourceManager.GetResourceByIdAsync(id);

            if (resource is not null)
            {
                Resource = resource;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _resourceManager.GetResourceByIdAsync(id);
            if (resource != null)
            {
                Resource = resource;
                await _resourceManager.DeleteResourceAsync(Resource.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
