using FoxholeIntelboard.Data;
using FoxholeIntelboard.Models;
using Microsoft.EntityFrameworkCore;

namespace FoxholeIntelboard.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IntelboardDBContext _context;

        public MaterialService(IntelboardDBContext context)
        {
            _context = context;
        }

        public Task<List<Material>> GetMaterialsAsync()
        {
            return _context.Materials.ToListAsync();
        }

        //Checking DB for resource and if they're there nothing happens otherwise it adds a new resource. Only for admin.
        public async Task SeedMaterialsAsync()
        {
            List<string> materials = await _context.Materials.Select(x => x.Name).ToListAsync();
            List<string> materialNames = new List<string> { "Basic Materials", "Diesel", "Petrol", "Refined Materials", "Explosive", "Heavy Explosive", "Concrete", "Aluminum Alloy", "Iron Alloy", "Copper", "Relic Materials", "Gravel" };

            foreach (string name in materialNames)
            {
                if (materials.Contains(name))
                {
                    Console.WriteLine($"{name} exists");
                }
                else
                {
                    Console.WriteLine($"{name} doesn't exist, added now");
                    Material newMaterial = new Material();
                    newMaterial.Name = name;
                    _context.Materials.Add(newMaterial);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }

}
