using IntelboardCore.Models;
using IntelboardCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace IntelboardCore.Services
{
    public class ResourceService : IResourceService
    {
        private readonly string _contentRootPath;
        private readonly HttpClient _httpClient;

        public ResourceService(string contentRootPath, HttpClient httpClient)
        {
            _contentRootPath = contentRootPath;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");
        }

        public List<string[]> CsvData { get; set; } = new();

        // Reads data from Resources.csv to check if the Resources table already contains each resource.
        // If it exist, ignore. If it does not, save resource to database
        public async Task SeedResourcesAsync()
        {
            var resources = await _httpClient.GetFromJsonAsync<List<Resource>>("/api/Resource/");
            var filePath = Path.Combine(_contentRootPath, "Data\\CSV", "Resources.csv");
            Console.WriteLine("Filepath: " + filePath);

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');
                    if (!resources.Select(r => r.Name).Contains(values[0]))
                    {
                        Resource newResource = new Resource
                        {
                            Name = values[0],
                            Image = values[1]
                        };

                        var response = await _httpClient.PostAsJsonAsync("/api/Resource/", newResource);
                        Console.WriteLine(response.IsSuccessStatusCode ? $"{values[0]} created successfully." : $"Error creating resource: {response.StatusCode} {response.Content}");
                    }
                    else
                    {
                        Console.WriteLine($"Resource {values[0]} already exists in database");
                    }
                }
            }
        }

    }
}
