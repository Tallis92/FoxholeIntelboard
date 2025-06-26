using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntelboardCore.Models;
using IntelboardCore.DAL.Interfaces;

namespace FoxholeIntelboard.Pages.Materials
{
    public class IndexModel : PageModel
    {


        private readonly IMaterialManager _materialManager;

        public IndexModel(IMaterialManager materialManager)
        {
            _materialManager = materialManager;
        }

        public IList<Material> Material { get; set; }

        public async Task OnGetAsync()
        {
            Material = await _materialManager.GetMaterialsAsync();
        }

    }
}
