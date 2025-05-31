using FoxholeIntelboard.DAL;
using FoxholeIntelboard.DTO;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FoxholeIntelboard.Pages.Ammunitions
{

    public class CreateModel : PageModel
    {

        private readonly IManagerDto _manager;

        public CreateModel(IManagerDto manager)
        {
            _manager = manager;
        }

        [BindProperty]
        public Ammunition Ammunitions { get; set; } = default!;

        [BindProperty]
        public List<Material> Materials { get; set; } = new();
        public List<SelectListItem> DamageTypeOptions { get; set; } = new();
        public List<SelectListItem> AmmoPropertiesOptions { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Materials = await _manager.MaterialManager.GetMaterialsAsync();

            DamageTypeOptions = Enum.GetValues(typeof(DamageType))
                .Cast<DamageType>()
                .Select(d => new SelectListItem
                {
                    Value = d.ToString(),
                    Text = GetEnumDescription(d)
                }).ToList();

            AmmoPropertiesOptions = Enum.GetValues(typeof(AmmoProperties))
               .Cast<AmmoProperties>()
               .Select(p => new SelectListItem
               {
                   Value = p.ToString(),
                   Text = GetName(p)
               }).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Ammunitions.ProductionCost == null || !Ammunitions.ProductionCost.Any())
            {
                ModelState.AddModelError("", "At least one production cost is required.");
                return Page();
            }

            DamageTypeOptions = Enum.GetValues(typeof(DamageType))
                .Cast<DamageType>()
                .Select(d => new SelectListItem
                {
                    Value = d.ToString(),
                    Text = GetEnumDescription(d)
                }).ToList();

            foreach (var cost in Ammunitions.ProductionCost)
            {
                cost.CraftableItem = Ammunitions;
            }

            await _manager.AmmunitionManager.CreateAmmunitionAsync(Ammunitions);

            return RedirectToPage("./Index");
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }
        public static string GetName(Enum value)
        {
            return value.GetType()
                        .GetMember(value.ToString())
                        .FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>()?
                        .Name ?? value.ToString();
        }
    }
}
