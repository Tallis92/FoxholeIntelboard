using IntelboardAPI.Models;
namespace IntelboardAPI.Services;

public interface IResourceService
    {
       Task<List<Resource>> GetResourcesAsync();
       Task SeedResourcesAsync();
    }

