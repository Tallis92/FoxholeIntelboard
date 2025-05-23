using FoxholeIntelboard.DAL;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using IntelboardAPI.Data;
using Microsoft.EntityFrameworkCore;


namespace FoxholeIntelboard.Pages;

public class IndexModel : PageModel
{
    private readonly AmmunitionManager _ammunitionManager;
    private readonly MaterialManager _materialManager;
    private readonly ResourceManager _resourceManager;
    private readonly WeaponManager _weaponManager;
    private readonly IntelboardDbContext _context;

    public IList<Ammunition> Ammunitions { get; set; }
    public IList<Material> Materials { get; set; }
    public IList<Resource> Resources { get; set; }
    public IList<Weapon> Weapons { get; set; }
    public IList<Category> Categories { get; set; }

    public IndexModel(AmmunitionManager ammunitionManager,IntelboardDbContext context, MaterialManager materialManager, ResourceManager resourceManager, WeaponManager weaponManager)
    {
        _ammunitionManager = ammunitionManager;
        _context = context;
        _materialManager = materialManager;
        _resourceManager = resourceManager;
        _weaponManager = weaponManager;
    }

    public async Task OnGetAsync()
    {
        Ammunitions = await _ammunitionManager.GetAmmunitionsAsync();
        Resources = await _resourceManager.GetResourcesAsync();
        Materials = await _materialManager.GetMaterialsAsync();
        Weapons = await _weaponManager.GetWeaponsAsync();
        Categories = await _context.Categories.ToListAsync();
    }


   

}
