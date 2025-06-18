using IntelboardCore.Models;

namespace IntelboardCore.DAL
{
    public interface ICategoryManager
    {
        Task<List<Category>> GetCategoriesAsync();

        Task SeedCategoriesAsync();
    }
}
