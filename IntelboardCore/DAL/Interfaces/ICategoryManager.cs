using IntelboardCore.Models;

namespace IntelboardCore.DAL.Interfaces
{
    public interface ICategoryManager
    {
        Task<List<Category>> GetCategoriesAsync();

        Task SeedCategoriesAsync();
    }
}
