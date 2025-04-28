using FoxholeIntelboard.Models;
namespace FoxholeIntelboard.Services;

public interface IResourceService
    {
       Task<List<Resource>> GetResourcesAsync();
       Task SeedResourcesAsync();
    }

