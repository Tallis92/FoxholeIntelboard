using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Models;
using System.ComponentModel.Design;
using FoxholeIntelboard.Data;

namespace FoxholeIntelboard.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IWebHostEnvironment _env;
        private readonly Data.IntelboardDBContext _context;
        public ResourceService(Data.IntelboardDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public List<string[]> CsvData { get; set; } = new();

        public async Task<List<Resource>> GetResourcesAsync()
        {
            List<Resource> resources = await _context.Resources.ToListAsync();
            return resources;
        }

        // Reads data from Resources.csv to check if the Resources table already contains each resource.
        // If it exist, ignore. If it does not, save resource to database-
        public async Task SeedResourcesAsync()
        {
            List<string> resourceNames = await _context.Resources.Select(r => r.Name).ToListAsync(); ;

            var filePath = Path.Combine(_env.ContentRootPath, "Data\\CSV", "Resources.csv");
            Console.WriteLine("Filepath: " + filePath);

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');
                    if (!resourceNames.Contains(values[0]))
                    {
                        Resource newResource = new Resource
                        {
                            Name = values[0]
                        };
                        _context.Resources.Add(newResource);
                        Console.WriteLine($"{values[0]} added succesfully to resource list!");
                    }
                    else
                    {
                        Console.WriteLine($"Resource {values[0]} already exists in database");
                    }
                }
                await _context.SaveChangesAsync();
            }
           
        }
    }
}
