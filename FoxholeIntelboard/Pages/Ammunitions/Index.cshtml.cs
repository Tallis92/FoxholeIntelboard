using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntelboardCore.Models;
using IntelboardCore.DAL;
using IntelboardCore.DTO;

namespace FoxholeIntelboard.Pages.Ammunitions
{
    public class IndexModel : PageModel
    {
        private readonly IAmmunitionManager _ammunitionManager;

        public IndexModel(IAmmunitionManager ammunitionManger)
        {
            _ammunitionManager = ammunitionManger;
        }

        public IList<Ammunition> Ammunitions { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Ammunitions = await _ammunitionManager.GetAmmunitionsAsync();
        }
    }
}
