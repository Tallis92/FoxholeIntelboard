using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.DAL;
using FoxholeIntelboard.Models;

namespace FoxholeIntelboard.Pages.Materials
{
    public class DeleteModel : PageModel
    {
        private readonly MaterialManager _materialManager;

        public DeleteModel(MaterialManager materialManager)
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

            var material = await _materialManager.GetMaterialByIdAsync(id);

            if (material is not null)
            {
                Material = material;

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

            var material = await _materialManager.GetMaterialByIdAsync(id);
            if (material != null)
            {
                Material = material;
                await _materialManager.DeleteMaterialAsync(Material.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
