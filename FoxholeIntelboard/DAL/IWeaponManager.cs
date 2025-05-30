using IntelboardAPI.Models;
namespace FoxholeIntelboard.DAL
{
    public interface IWeaponManager
    {
        Task<List<Weapon>> GetWeaponsAsync();
        Task<Weapon> GetWeaponByIdAsync(int? id);
        Task CreateWeaponAsync(Weapon weapon);
        Task EditWeaponAsync(Weapon weapon);
        Task DeleteWeaponAsync(int? id);
        Task SeedWeaponsAsync();
    }
}
