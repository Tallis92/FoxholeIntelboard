using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Models;

namespace FoxholeIntelboard.Pages.Materials
{
    public class IndexModel : PageModel
    {


        public IndexModel()
        {

        }

        public IList<Material> Material { get; set; }

        public async Task OnGetAsync()
        {
            //Material = await Materials.ToListAsync();
        }

        public async Task<IActionResult> OnPostSeedMaterialsAsync()
        {
            //await  SeedMaterialsAsync();
            return RedirectToPage();
        }
    }
}
