using IntelboardAPI.Models;

namespace FoxholeIntelboard.DAL
{
    public interface ICategoryManager
    {
        Task<List<Category>> GetCategoriesAsync();

        Task SeedCategoriesAsync();
    }
}
