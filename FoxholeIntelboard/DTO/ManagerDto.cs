using FoxholeIntelboard.DAL;

namespace FoxholeIntelboard.DTO
{
    public class ManagerDto : IManagerDto
    {

        public IAmmunitionManager AmmunitionManager { get; }
        public IWeaponManager WeaponManager { get; }
        public IMaterialManager MaterialManager { get; }
        public IResourceManager ResourceManager { get; }
        public ICategoryManager CategoryManager { get; }
        public ICraftableItemManager CraftableItemManager { get; }
        public IInventoryManager InventoryManager { get; }

        public ManagerDto(IAmmunitionManager ammo, IWeaponManager weapon, IMaterialManager material, IResourceManager resource, 
        ICategoryManager category, ICraftableItemManager craftable, IInventoryManager inventory)
        {
            AmmunitionManager = ammo;
            WeaponManager = weapon;
            MaterialManager = material;
            ResourceManager = resource;
            CategoryManager = category;
            CraftableItemManager = craftable;
            InventoryManager = inventory;
        }

    }
}
