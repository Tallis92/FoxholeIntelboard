using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoxholeIntelboard.Data;
using FoxholeIntelboard.Models;
using System.ComponentModel;
using System.Reflection;

namespace FoxholeIntelboard.Pages.Ammunitions
{
    public class CreateModel : PageModel
    {
        private readonly FoxholeIntelboard.Data.IntelboardDBContext _context;
        [BindProperty]
        public Ammunition Ammunition { get; set; }
        public List<SelectListItem> DamageTypeOptions { get; set; } // För dropdown

        public CreateModel(FoxholeIntelboard.Data.IntelboardDBContext context)
        {
            _context = context;
            Ammunition = new Ammunition();

            // Hämta DamageType-värden och deras beskrivningar
            DamageTypeOptions = Enum.GetValues(typeof(DamageType))
                .Cast<DamageType>()
                .Select(dt => new SelectListItem
                {
                    Value = dt.ToString(),
                    Text = dt.GetType()
                        .GetField(dt.ToString())
                        .GetCustomAttribute<DescriptionAttribute>()
                        ?.Description ?? dt.ToString()
                })
                .ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Återskapa DamageTypeOptions vid valideringsfel
                DamageTypeOptions = Enum.GetValues(typeof(DamageType))
                    .Cast<DamageType>()
                    .Select(dt => new SelectListItem
                    {
                        Value = dt.ToString(),
                        Text = dt.GetType()
                            .GetField(dt.ToString())
                            .GetCustomAttribute<DescriptionAttribute>()
                            ?.Description ?? dt.ToString()
                    })
                    .ToList();
                return Page();
            }

            Ammunition.ItemName = Ammunition.Name ?? "Default Item"; // Sätt ItemName
            _context.Ammunitions.Add(Ammunition);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
