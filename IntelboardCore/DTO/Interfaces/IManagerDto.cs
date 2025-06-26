using IntelboardCore.DAL.Interfaces;

namespace IntelboardCore.DTO.Interfaces
{
    public interface IManagerDto
    {
        public IResourceManager ResourceManager { get; }
        public IMaterialManager MaterialManager { get; }
        public IAmmunitionManager AmmunitionManager { get; }
        public IWeaponManager WeaponManager { get; }
        public ICraftableItemManager CraftableItemManager { get; }
        public ICategoryManager CategoryManager { get; }
        public IInventoryManager InventoryManager { get; }
    }
}
