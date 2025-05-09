using FoxholeIntelboard.Models;

namespace FoxholeIntelboard.Services
{
    public interface IMaterialService
    {
        Task<List<Material>> GetMaterialsAsync();
        Task SeedMaterialsAsync();
    }

}
