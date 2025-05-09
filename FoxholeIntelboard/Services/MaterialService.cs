using FoxholeIntelboard.Models;
using Microsoft.EntityFrameworkCore;

namespace FoxholeIntelboard.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IWebHostEnvironment _env;
        private readonly Data.IntelboardDBContext _context;
        public MaterialService(Data.IntelboardDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public Task<List<Material>> GetMaterialsAsync()
        {
            return _context.Materials.ToListAsync();
        }

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

                    if (!materialNames.Contains(name))
                    {
                        var material = new Material
                        {
                            Name = name,
                            CrateAmount = crateAmount,
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

            // Refresh the lookup with updated IDs
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

                    _context.Costs.Add(cost);
                }

                await _context.SaveChangesAsync();
                Console.WriteLine("Costs added and saved.");
            }
        }

    }
}
