using IntelboardAPI.Models;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Data;

namespace IntelboardAPI.Services
{
    public class AmmunitionService : IAmmunitionService
    {
        private readonly IntelboardDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AmmunitionService(IntelboardDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Loads and loops through csv file, checks for identical objects in the database and then saves a new material into the database.
        // Uses dictionary to look for matches in Materials table.
        public async Task SeedAmmunitionsAsync()
        {
            var materialLookup = await _context.Materials.ToDictionaryAsync(r => r.Name);
            var ammunitionNames = await _context.Ammunitions.Select(m => m.Name).ToListAsync();

            var ammunitionFilePath = Path.Combine(_env.ContentRootPath, "Data\\CSV", "Ammunitions.csv");
            var costFilePath = Path.Combine(_env.ContentRootPath, "Data\\CSV", "AmmunitionCosts.csv");
            var ammunitionLookup = new Dictionary<string, Ammunition>();

            if (File.Exists(ammunitionFilePath))
            {
                foreach (var line in File.ReadLines(ammunitionFilePath).Skip(1))
                {
                    var parts = line.Split(',');
                    string name = parts[0];
                    int crateAmount = int.Parse(parts[1]);
                    string description = parts[2];
                    int damage = int.Parse(parts[3]);


                    if (!ammunitionNames.Contains(name))
                    {
                        var ammunition = new Ammunition
                        {
                            Name = name,
                            CrateAmount = crateAmount,
                            Description = description,
                            Damage = (DamageType)damage,
                            ProductionCost = new List<Cost>()
                        };

                        ammunitionLookup[name] = ammunition;
                        _context.Ammunitions.Add(ammunition);
                        Console.WriteLine($"{name} added succesfully to ammunitions list");
                    }
                    else
                    {
                        Console.WriteLine($"{name} already exists in database!");
                    }
                }

                await _context.SaveChangesAsync();
            }

            // Reloads Ammunitions table to give access to newly assigned Id's, then uses a dictionary with a Name key to check
            // for a match. If a match is found, load the whole ammunition object to gain access to the Id to be stored in craftableItem Table.
            // Then creates corresponding Cost data.
            var allAmmunitions = await _context.Ammunitions.ToListAsync();
            var ammunitionByName = allAmmunitions.ToDictionary(m => m.Name);
            var resourceLookup = await _context.Resources.ToDictionaryAsync(r => r.Name);
            var allMaterials = await _context.Materials.ToListAsync();
            var materialByName = allMaterials.ToDictionary(m => m.Name);
            if (File.Exists(costFilePath))
            {
                foreach (var line in File.ReadLines(costFilePath).Skip(1))
                {
                    var parts = line.Split(',');
                    int amount = int.Parse(parts[0]);
                    string ammunitionName = parts[1];
                    string type = parts[2];
                    string itemName = parts[3];

                    if (!ammunitionByName.TryGetValue(ammunitionName, out var parentAmmunition))
                    {
                        Console.WriteLine($"No material found for cost target: {ammunitionName}");
                        continue;
                    }

                    var cost = new Cost
                    {
                        Amount = amount,
                        CraftableItemId = parentAmmunition.Id
                    };

                    // Checks csv Title 'Type' to assign either a resourceId or materialId and keep the other null.

                    if (type == "Resource" && resourceLookup.TryGetValue(itemName, out var resource))
                    {
                        cost.ResourceId = resource.Id;
                        cost.Resource = resource;
                    }
                    else if (type == "Material" && materialByName.TryGetValue(itemName, out var materialCostItem))
                    {
                        cost.MaterialId = materialCostItem.Id;
                        cost.Material = materialCostItem;
                    }

                    // Checks for identical costs in database to avoid duplicate entries. 
                    bool costExists = await _context.Costs.AnyAsync(c =>
                        c.CraftableItemId == parentAmmunition.Id &&
                        c.Amount == amount &&
                        ((c.Resource != null && c.Resource.Name == itemName) ||
                         (c.Material != null && c.Material.Name == itemName)));

                    if (!costExists)
                    {
                        _context.Costs.Add(cost);
                        Console.WriteLine($"Cost successfully added to {itemName}!");
                    }
                    else
                    {
                        Console.WriteLine($"An identical cost already exists in database!");
                    }
                }

                await _context.SaveChangesAsync();
                Console.WriteLine("Costs added and saved.");
            }
        }
    }
}
