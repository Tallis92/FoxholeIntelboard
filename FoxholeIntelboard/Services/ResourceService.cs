using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.Models;
using System.ComponentModel.Design;
using FoxholeIntelboard.Data;

namespace FoxholeIntelboard.Services
{
    public class ResourceService : IResourceService
    {
        private readonly Data.IntelboardDBContext _context;
        public ResourceService(Data.IntelboardDBContext context)
        {
            _context = context;
        }
        public async Task<List<Resource>> GetResourcesAsync()
        {
            List<Resource> resources = await _context.Resources.ToListAsync();
            return resources;
        }

        //Checking DB for resource and if they're there nothing happens otherwise it adds a new resource. Only for admin.
        public async Task SeedResourcesAsync()
        {
            List<string> resources = await _context.Resources.Select(x => x.Name).ToListAsync();
            List<string> resourceNames = new List<string> { "Salvage", "Coal", "Oil", "Sulfur", "Components", "Rare Metal", "Damaged Components", "Aluminum", "Copper", "Iron" };

            foreach (string name in resourceNames)
            {
                if (resources.Contains(name))
                {
                    Console.WriteLine($"{name} exists");
                }
                else
                {
                    Console.WriteLine($"{name} doesn't exist, added now");
                    Resource newResource = new Resource();
                    newResource.Name = name;
                    _context.Resources.Add(newResource);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
