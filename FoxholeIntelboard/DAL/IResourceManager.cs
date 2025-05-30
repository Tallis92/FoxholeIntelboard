using IntelboardAPI.Models;
namespace FoxholeIntelboard.DAL
{
    public interface IResourceManager 
    {
        Task<List<Resource>> GetResourcesAsync();
        Task<Resource> GetResourceByIdAsync(int? id);
        Task CreateResourceAsync(Resource resource);
        Task EditResourceAsync(Resource resource);
        Task DeleteResourceAsync(int? id);
        Task SeedResourcesAsync();
    }
}
