using IntelboardAPI.Models;
namespace FoxholeIntelboard.DAL
{
    public interface IAmmunitionManager
    {
        Task<List<Ammunition>> GetAmmunitionsAsync();
        Task<Ammunition> GetAmmunitionByIdAsync(int? id);
        Task CreateAmmunitionAsync(Ammunition ammunition);
        Task EditAmmunitionAsync(Ammunition ammunition);
        Task DeleteAmmunitionAsync(int? id);
        Task SeedAmmunitionsAsync();
    }
}
