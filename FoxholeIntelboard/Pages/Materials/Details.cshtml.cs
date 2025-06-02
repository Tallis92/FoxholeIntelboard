using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Models;
using FoxholeIntelboard.DAL;

namespace FoxholeIntelboard.Pages.Materials
{
    public class DetailsModel : PageModel
    {
        private readonly IMaterialManager _materialManager;

        public DetailsModel(IMaterialManager materialManager)
        {
            _materialManager = materialManager;
        }

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
    }
}
