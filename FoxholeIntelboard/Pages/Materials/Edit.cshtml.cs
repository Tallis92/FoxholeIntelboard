using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Models;
using FoxholeIntelboard.DAL;

namespace FoxholeIntelboard.Pages.Materials
{
    public class EditModel : PageModel
    {
        private readonly IMaterialManager _materialManager;

        public EditModel(IMaterialManager materialManager)
        {
            _materialManager = materialManager;
        }

        [BindProperty]
        public Material Material { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material =  await _materialManager.GetMaterialByIdAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            Material = material;
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
            await _materialManager.EditMaterialAsync(Material);
            return RedirectToPage("./Index");
        }

        private async Task<bool> MaterialExists(int id)
        {
            var materials = await _materialManager.GetMaterialsAsync();
            return materials.Any(e => e.Id == id);
        }
    }
}
