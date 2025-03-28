using FoxholeIntelboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace FoxholeIntelboard.Pages;

public class IndexModel : PageModel
{
    public Ammunition Ammo { get; set; }
    
    public void OnGet()
    {
        var ammo = new Ammunition
        {
            Name = "7.62mm",
            Damage = DamageType.Kinetic, // Enum-v�rdet s�tts h�r
            Description = "Standard rifle ammo", // Unik beskrivning f�r denna ammo
            CrateAmount = 100,
            ProductionCost = new List<Cost> { /* ... */ },
        };
        Ammo = ammo;
        Console.WriteLine($"Name: {ammo.Name}");
        Console.WriteLine($"Object Description: {ammo.Description}");        // "Standard rifle ammo"
        Console.WriteLine($"Damage Description: {ammo.GetDamageDescription()}");

    }
}
