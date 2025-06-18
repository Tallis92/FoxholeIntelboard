using IntelboardCore.Models;
using System.Net.Http.Json;

namespace IntelboardCore.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly string _contentRootPath;
        private readonly HttpClient _httpClient;

        public MaterialService(string contentRootPath, HttpClient httpClient)
        {
            _contentRootPath = contentRootPath;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");
        }

        public async Task SeedMaterialsAsync()
        {
            var resources = await _httpClient.GetFromJsonAsync<List<Resource>>("/api/Resource/");
            var resourceLookup = resources?.ToDictionary(r => r.Name) ?? new Dictionary<string, Resource>();
            // TODO: Check this syntax
            var materialNames = await _httpClient.GetFromJsonAsync<List<Material>>("/api/Material/");

            var materialFilePath = Path.Combine(_contentRootPath, "Data", "CSV", "Materials.csv");
            var costFilePath = Path.Combine(_contentRootPath, "Data", "CSV", "MaterialCosts.csv");

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
                    int categoryId = int.Parse(parts[5]);

                    if (!materialNames.Select(m => m.Name).Contains(name))
                    {
                        var material = new Material
                        {
                            Name = name,
                            CrateAmount = crateAmount,
                            TechMaterial = tech,
                            LargeMaterial = large,
                            FacilityMade = facility,
                            CategoryId = categoryId,
                            ProductionCost = new List<Cost>()
                        };

                        materialLookup[name] = material;
                        var response = await _httpClient.PostAsJsonAsync("/api/Material/", material);
                        Console.WriteLine(response.IsSuccessStatusCode
                            ? $"{material.Name} created successfully."
                            : $"Error creating material: {response.StatusCode} {response.Content}");
                    }
                    else
                    {
                        Console.WriteLine($"{name} already exists in database!");
                    }
                }
            }

            var allMaterials = await _httpClient.GetFromJsonAsync<List<Material>>("/api/Material/");
            var materialByName = allMaterials?.ToDictionary(m => m.Name) ?? new Dictionary<string, Material>();

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

                    string uri = $"/api/Cost/Exists?craftableItemId={parentMaterial.Id}&amount={amount}&itemName={itemName}";
                    bool costExists = await _httpClient.GetFromJsonAsync<bool>(uri);

                    if (!costExists)
                    {
                        var response = await _httpClient.PostAsJsonAsync("/api/Cost/", cost);
                        Console.WriteLine($"Cost successfully added to {itemName}!");
                    }
                    else
                    {
                        Console.WriteLine($"An identical cost already exists in database!");
                    }
                }
            }
        }
    }
}
