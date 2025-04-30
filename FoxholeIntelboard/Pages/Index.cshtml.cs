using FoxholeIntelboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace FoxholeIntelboard.Pages;

public class IndexModel : PageModel
{
    public Models.Ammunition Ammo { get; set; }
    
    public void OnGet()
    {
        var ammo = new Models.Ammunition
        {
            Name = "7.62mm",
            Damage = DamageType.Kinetic, // Enum-värdet sätts här
            Description = "Standard rifle ammo", // Unik beskrivning för denna ammo
            CrateAmount = 100,
            ProductionCost = new List<Cost> { /* ... */ },
        };
        Ammo = ammo;
        Console.WriteLine($"Name: {ammo.Name}");
        Console.WriteLine($"Ammo Description: {ammo.Description}");        // "Standard rifle ammo"
        Console.WriteLine($"Damage type: {ammo.GetDamageDescription()}");

    }
}
