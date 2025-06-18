using IntelboardCore.DAL;
using IntelboardCore.DTO;
//using IntelboardAPI.Data;
using IntelboardCore.Models;
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
        private readonly IManagerDto _manager;
        public IndexModel(IManagerDto manager)
        {
           _manager = manager;
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
            Ammunitions = await _manager.AmmunitionManager.GetAmmunitionsAsync();
            Resources = await _manager.ResourceManager.GetResourcesAsync();
            Materials = await _manager.MaterialManager.GetMaterialsAsync();
            Weapons = await _manager.WeaponManager.GetWeaponsAsync();
            Categories = await _manager.CategoryManager.GetCategoriesAsync();
            Inventories = await _manager.InventoryManager.GetInventoriesAsync();
            CraftableItems = await _manager.CraftableItemManager.GetCraftableItemsAsync();
            
        }

    }
}
