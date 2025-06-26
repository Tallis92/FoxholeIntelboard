using IntelboardCore.Models;
namespace IntelboardCore.DAL.Interfaces
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
