using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntelboardCore.Models;
using IntelboardCore.DAL;
using IntelboardCore.DTO.Interfaces;

namespace FoxholeIntelboard.Pages.Materials
{
    public class EditModel : PageModel
    {
        private readonly IManagerDto _manager;

        public EditModel(IManagerDto manager)
        {
            _manager = manager;
        }

        [BindProperty]
        public Material Material { get; set; } = default!;
        public List<Resource> Resources { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material =  await _manager.MaterialManager.GetMaterialByIdAsync(id);
            var resources = await _manager.ResourceManager.GetResourcesAsync();
            if (material == null && resources == null)
            {
                return NotFound();
            }
            Material = material;
            Resources = resources;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!await MaterialExists(Material.Id))
            {
                return NotFound();
            }
            await _manager.MaterialManager.EditMaterialAsync(Material);
            return RedirectToPage("./Index");
        }

        private async Task<bool> MaterialExists(int id)
        {
            var materials = await _manager.MaterialManager.GetMaterialsAsync();
            return materials.Any(e => e.Id == id);
        }
    }
}
