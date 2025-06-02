using FoxholeIntelboard.DAL;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using IntelboardAPI.Data;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.DTO;
using FoxholeIntelboard.DTO;


namespace FoxholeIntelboard.Pages;

public class IndexModel : PageModel
{
    //private readonly IManagerDto _manager;


    //public IndexModel(IManagerDto manager)
    //{
    //    _manager = manager;
    //}
    //public IList<Ammunition> Ammunitions { get; set; }
    //public IList<Material> Materials { get; set; }
    //public IList<Resource> Resources { get; set; }
    //public IList<Weapon> Weapons { get; set; }
    //public IList<Category> Categories { get; set; }
    public async Task OnGetAsync()
    {
        //Ammunitions = await _manager.AmmunitionManager.GetAmmunitionsAsync();
        //Resources = await _manager.ResourceManager.GetResourcesAsync();
        //Materials = await _manager.MaterialManager.GetMaterialsAsync();
        //Weapons = await _manager.WeaponManager.GetWeaponsAsync();
        //Categories = await _manager.CategoryManager.GetCategoriesAsync();
    }


   

}
