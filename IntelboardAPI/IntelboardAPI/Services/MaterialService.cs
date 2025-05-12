using IntelboardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IWebHostEnvironment _env;
        private readonly Data.IntelboardDbContext _context;
        public MaterialService(Data.IntelboardDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public Task<List<Material>> GetMaterialsAsync()
        {
            return _context.Materials.ToListAsync();
        }

        // Loads and loops through csv file, checks for identical objects in the database and then saves a new material into the database.
        // Uses dictionary to look for matches in Materials table.
        public async Task SeedMaterialsAsync()
        {
            var resourceLookup = await _context.Resources.ToDictionaryAsync(r => r.Name);
            var materialNames = await _context.Materials.Select(m => m.Name).ToListAsync();

            var materialFilePath = Path.Combine(_env.ContentRootPath, "Data\\CSV", "Materials.csv");
            var costFilePath = Path.Combine(_env.ContentRootPath, "Data\\CSV", "MaterialCosts.csv");
            var materialLookup = new Dictionary<string, Material>();

            if (File.Exists(materialFilePath))
            {
                foreach (var line in File.ReadLines(materialFilePath).Skip(1))
                {
                    var parts = line.Split(',');
                    string name = parts[0];
                    int crateAmount = int.Parse(parts[1]);
                    bool tech = bool.Parse(parts[2]);
                    bool large = bool.Parse(parts[3]);
                    bool facility = bool.Parse(parts[4]);

                    if (!materialNames.Contains(name))
                    {
                        var material = new Material
                        {
                            Name = name,
                            CrateAmount = crateAmount,
                            TechMaterial = tech,
                            LargeMaterial = large,
                            FacilityMade = facility,
                            ProductionCost = new List<Cost>()
                        };

                        materialLookup[name] = material;
                        _context.Materials.Add(material);
                        Console.WriteLine($"{name} added succesfully to materials list");
                    }
                    else
                    {
                        Console.WriteLine($"{name} already exists in database!");
                    }
                }

                await _context.SaveChangesAsync(); 
            }

            // Reloads Materials table to give access to newly assigned Id's, then uses a dictionary with a Name key to check
            // for a match. If a match is found, load the whole material object to gain access to the Id to be stored in craftableItem Table.
            // Then creates corresponding Cost data.
            var allMaterials = await _context.Materials.ToListAsync();
            var materialByName = allMaterials.ToDictionary(m => m.Name);

            if (File.Exists(costFilePath))
            {
                foreach (var line in File.ReadLines(costFilePath).Skip(1))
                {
                    var parts = line.Split(',');
                    int amount = int.Parse(parts[0]);
                    string materialName = parts[1];
                    string type = parts[2];
                    string itemName = parts[3];

                    if (!materialByName.TryGetValue(materialName, out var parentMaterial))
                    {
                        Console.WriteLine($"No material found for cost target: {materialName}");
                        continue;
                    }

                    var cost = new Cost
                    {
                        Amount = amount,
                        CraftableItemId = parentMaterial.Id
                    };

                    // Checks csv Title 'Type' to assign either a resourceId or materialIdand keep the other null.

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
                        c.CraftableItemId == parentMaterial.Id &&
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
