using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.DAL;
using FoxholeIntelboard.Models;

namespace FoxholeIntelboard.Pages.Resources
{
    public class EditModel : PageModel
    {
        private readonly ResourceManager _resourceManager;

        public EditModel(ResourceManager resourceManager)
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
            if (resource == null)
            {
                return NotFound();
            }
            Resource = resource;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!await ResourceExists(Resource.Id))
            {
                return NotFound();
            }
            await _resourceManager.EditResourceAsync(Resource);
           
            return RedirectToPage("./Index");
        }

        private async Task<bool> ResourceExists(int id)
        {
            var resources = await _resourceManager.GetResourcesAsync();
            return resources.Any(e => e.Id == id);
        }
    }
}
