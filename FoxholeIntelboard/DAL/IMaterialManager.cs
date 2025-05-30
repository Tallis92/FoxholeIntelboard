using IntelboardAPI.Models;
namespace FoxholeIntelboard.DAL
{
    public interface IMaterialManager
    {
        Task<List<Material>> GetMaterialsAsync();
        Task<Material> GetMaterialByIdAsync(int? id);
        Task EditMaterialAsync(Material material);
        Task CreateMaterialsAsync(Material material);
        Task DeleteMaterialAsync(int? id);
        Task SeedMaterialsAsync();
    }
}
