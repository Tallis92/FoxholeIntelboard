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
    }

}
