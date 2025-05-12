using IntelboardAPI.Models;

namespace IntelboardAPI.Services
{
    public interface IMaterialService
    {
        Task<List<Material>> GetMaterialsAsync();
        Task SeedMaterialsAsync();
    }

}
