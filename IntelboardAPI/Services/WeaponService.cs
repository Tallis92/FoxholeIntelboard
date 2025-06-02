using IntelboardAPI.Data;
using IntelboardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Services
{
    public class WeaponService : IWeaponService
    {
        private readonly IntelboardDbContext _context;
        private readonly IWebHostEnvironment _env;
        public WeaponService(IntelboardDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Loads csv files into an array, loops through the array to create factionspecific weapons and correspodning costs.
        // Then checks for identical objects in the database and then saves a new weapon into the database.
        // Uses dictionary to look for matches in Weapons table.
        public async Task SeedWeaponsAsync()
        {
            var materialLookup = await _context.Materials.ToDictionaryAsync(r => r.Name);
            var weaponNames = await _context.Weapons.Select(m => m.Name).ToListAsync();
            List<string> weaponFiles = new List<string>(){"ColonialWeapons.csv", "WardenWeapons.csv"};
            List<string> costFiles = new List<string>() { "ColonialWeaponCosts.csv", "WardenWeaponCosts.csv" };

            for(int i = 0; i < 2; i++) {
                var weaponFilePath = Path.Combine(_env.ContentRootPath, "Data\\CSV", weaponFiles[i]);
                var costFilePath = Path.Combine(_env.ContentRootPath, "Data\\CSV", costFiles[i]);
                var weaponLookup = new Dictionary<string, Weapon>();

                if (File.Exists(weaponFilePath))
                {
                    foreach (var line in File.ReadLines(weaponFilePath).Skip(1))
                    {
                        var parts = line.Split(',');

                        if (parts.Length < 8)
                        {
                            Console.WriteLine("Invalid line format, skipping: " + line);
                            continue;
                        }

                        string name = parts[0];
                        int categoryId = int.Parse(parts[1]);
                        int factionId = int.Parse(parts[2]);
                        int crateAmount = int.Parse(parts[3]);
                        string description = parts[4];
                        var weaponType = (WeaponType)int.Parse(parts[5]);
                        int ammunitionId = int.Parse(parts[6]);
                        List<WeaponProperties> weaponProperties = parts[7].Split(';', StringSplitOptions.RemoveEmptyEntries)
                                                                    .Select(p => (WeaponProperties)int.Parse(p))
                                                                    .ToList();

                        if (!weaponNames.Contains(name))
                        {
                            var weapon = new Weapon
                            {
                                Name = name,
                                CategoryId = categoryId,
                                FactionId = factionId,
                                CrateAmount = crateAmount,
                                Description = description,
                                WeaponType = weaponType,
                                AmmunitionId = ammunitionId,
                                WeaponProperties = weaponProperties,                                          
                                ProductionCost = new List<Cost>()
                            };

                            weaponLookup[name] = weapon;
                            _context.Weapons.Add(weapon);
                            Console.WriteLine($"{name} added succesfully to ammunitions list");
                        }
                        else
                        {
                            Console.WriteLine($"{name} already exists in database!");
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                // Reloads Weapons table to give access to newly assigned Id's, then uses a dictionary with a Name key to check
                // for a match. If a match is found, load the whole weapon object to gain access to the Id to be stored in craftableItem Table.
                // Then creates corresponding Cost data.
                var allWeapons = await _context.Weapons.ToListAsync();
                var weaponByName = allWeapons.ToDictionary(m => m.Name);
                var resourceLookup = await _context.Resources.ToDictionaryAsync(r => r.Name);
                var allMaterials = await _context.Materials.ToListAsync();
                var materialByName = allMaterials.ToDictionary(m => m.Name);
                if (File.Exists(costFilePath))
                {
                    foreach (var line in File.ReadLines(costFilePath).Skip(1))
                    {
                        var parts = line.Split(',');
                        int amount = int.Parse(parts[0]);
                        string weaponName = parts[1];
                        string type = parts[2];
                        string itemName = parts[3];

                        if (!weaponByName.TryGetValue(weaponName, out var parentWeapon))
                        {
                            Console.WriteLine($"No material found for cost target: {weaponName}");
                            continue;
                        }

                        var cost = new Cost
                        {
                            Amount = amount,
                            CraftableItemId = parentWeapon.Id
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
                            c.CraftableItemId == parentWeapon.Id &&
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
                }
                await _context.SaveChangesAsync();
                Console.WriteLine("Costs added and saved.");
            }
        }
    }
}

