using FoxholeIntelboard.DAL;
using IntelboardAPI.Data;
using IntelboardAPI.DTO;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FoxholeIntelboard.Pages.Lists
{
    public class IndexModel : PageModel
    {
        private readonly AmmunitionManager _ammunitionManager;
        private readonly MaterialManager _materialManager;
        private readonly ResourceManager _resourceManager;
        private readonly WeaponManager _weaponManager;
        private readonly CategoryManager _categoryManager;
        private readonly InventoryManager _inventoryManager;
        private readonly CraftableItemManager _craftableItemManager;
        public IndexModel(AmmunitionManager ammunitionManager, IntelboardDbContext context, MaterialManager materialManager,
            ResourceManager resourceManager, WeaponManager weaponManager, CategoryManager categoryManager, InventoryManager inventoryManager,
            CraftableItemManager craftableItemManager)
        {
            _ammunitionManager = ammunitionManager;
            _materialManager = materialManager;
            _resourceManager = resourceManager;
            _weaponManager = weaponManager;
            _categoryManager = categoryManager;
            _inventoryManager = inventoryManager;
            _craftableItemManager = craftableItemManager;
        }
        public IList<Ammunition> Ammunitions { get; set; }
        public IList<Material> Materials { get; set; }
        public IList<Resource> Resources { get; set; }
        public IList<Weapon> Weapons { get; set; }
        public IList<Category> Categories { get; set; }
        public List<InventoryDto> Inventories { get; set; } = new List<InventoryDto>();
        public List<CraftableItemDto> CraftableItems { get; set; } = new List<CraftableItemDto>();

        public async Task OnGetAsync()
        {
            Ammunitions = await _ammunitionManager.GetAmmunitionsAsync();
            Resources = await _resourceManager.GetResourcesAsync();
            Materials = await _materialManager.GetMaterialsAsync();
            Weapons = await _weaponManager.GetWeaponsAsync();
            Categories = await _categoryManager.GetCategoriesAsync();
            Inventories = await _inventoryManager.GetInventoriesAsync();
            CraftableItems = await _craftableItemManager.GetCraftableItemsAsync();
            
        }

    }
}
