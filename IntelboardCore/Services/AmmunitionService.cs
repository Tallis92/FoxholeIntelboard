using IntelboardCore.Models;
using IntelboardCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace IntelboardCore.Services
{
    public class AmmunitionService : IAmmunitionService
    {
        private readonly string _contentRootPath;
        private readonly HttpClient _httpClient;
        public AmmunitionService(string contentRootPath, HttpClient httpClient)
        {
            _contentRootPath = contentRootPath;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");
        }

        // Loads and loops through csv file, checks for identical objects in the database and then saves a new ammunition into the database.
        // Uses dictionary to look for matches in Ammunitions table.
        public async Task SeedAmmunitionsAsync()
        {
            var materials = await _httpClient.GetFromJsonAsync<List<Material>>("/api/Material/");
            var materialLookup = materials?.ToDictionary(r => r.Name) ?? new Dictionary<string, Material>();

            var ammunitions = await _httpClient.GetFromJsonAsync<List<Ammunition>>("/api/Ammunition/");

            var ammunitionFilePath = Path.Combine(_contentRootPath, "Data\\CSV", "Ammunitions.csv");
            var costFilePath = Path.Combine(_contentRootPath, "Data\\CSV", "AmmunitionCosts.csv");
            var ammunitionLookup = new Dictionary<string, Ammunition>();

            if (File.Exists(ammunitionFilePath))
            {
                try
                {
                    foreach (var line in File.ReadLines(ammunitionFilePath).Skip(1))
                    {
                        var parts = line.Split(',');
                        string name = parts[0];
                        int categoryId = int.Parse(parts[1]);
                        int crateAmount = int.Parse(parts[2]);
                        string description = parts[3];
                        var damagetype = (DamageType)int.Parse(parts[4]);
                        List<AmmoProperties> ammoProperties = parts[5].Split(';', StringSplitOptions.RemoveEmptyEntries)
                                                                   .Select(p => (AmmoProperties)int.Parse(p))
                                                                   .ToList();
                        string image = parts[6];


                        if (!ammunitions.Select(a => a.Name).Contains(name))
                        {
                            var ammunition = new Ammunition
                            {
                                Name = name,
                                CrateAmount = crateAmount,
                                CategoryId = categoryId,
                                Description = description,
                                DamageType = damagetype,
                                AmmoProperties = ammoProperties,
                                ProductionCost = new List<Cost>(),
                                Image = image
                            };

                            ammunitionLookup[name] = ammunition;
                            var response = await _httpClient.PostAsJsonAsync("/api/Ammunition/", ammunition);
                            Console.WriteLine(response.IsSuccessStatusCode ? $"{name} created successfully." : $"Error creating ammunition: {response.StatusCode} {response.Content}");
                        }
                        else
                        {
                            Console.WriteLine($"{name} already exists in database!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during ammunition seeding: {ex.Message}");
                }

            }

            // Reloads Ammunitions table to give access to newly assigned Id's, then uses a dictionary with a Name key to check
            // for a match. If a match is found, load the whole ammunition object to gain access to the Id to be stored in craftableItem Table.
            // Then creates corresponding Cost data.
            var allAmmunitions = await _httpClient.GetFromJsonAsync<List<Ammunition>>("/api/Ammunition/");
            var ammunitionByName = allAmmunitions.ToDictionary(m => m.Name);

            var resources = await _httpClient.GetFromJsonAsync<List<Resource>>("/api/Resource/");
            var resourceLookup = resources.ToDictionary(r => r.Name);

            var allMaterials = await _httpClient.GetFromJsonAsync<List<Material>>("/api/Material/");

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
                    string uri = $"/api/Cost/Exists?craftableItemId={parentAmmunition.Id}&amount={amount}&itemName={itemName}";
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
