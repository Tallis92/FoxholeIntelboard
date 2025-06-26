using IntelboardCore.Models;
namespace IntelboardCore.DAL.Interfaces
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
