using FoxholeIntelboard.Data;
using FoxholeIntelboard.Models;
using FoxholeIntelboard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;

namespace FoxholeIntelboard.Pages.Ammunitions
{

    public class CreateModel : PageModel
    {
        private readonly IntelboardDBContext _context;
        private readonly IMaterialService _materialService;

        public CreateModel(IntelboardDBContext context, IMaterialService materialService)
        {
            _context = context;
            _materialService = materialService;
        }

        [BindProperty]
        public Ammunition Ammunitions { get; set; } = default!;

        [BindProperty]
        public List<Material> Materials { get; set; } = new();

        public List<SelectListItem> DamageTypeOptions { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Materials = await _materialService.GetMaterialsAsync();

            DamageTypeOptions = Enum.GetValues(typeof(DamageType))
                .Cast<DamageType>()
                .Select(d => new SelectListItem
                {
                    Value = d.ToString(),
                    Text = GetEnumDescription(d)
                }).ToList();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                if (Ammunitions.ProductionCost == null || !Ammunitions.ProductionCost.Any())
                {
                    Ammunitions.ProductionCost = new List<Cost> { new Cost() };
                }

                DamageTypeOptions = Enum.GetValues(typeof(DamageType))
                    .Cast<DamageType>()
                    .Select(d => new SelectListItem
                    {
                        Value = d.ToString(),
                        Text = GetEnumDescription(d)
                    }).ToList();

                return Page();
            }

            foreach (var cost in Ammunitions.ProductionCost)
            {
                cost.CraftableItem = Ammunitions;
            }

            _context.Ammunitions.Add(Ammunitions);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }
    }
}
