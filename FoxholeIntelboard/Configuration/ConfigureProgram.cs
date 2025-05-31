using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.DAL;
using IntelboardAPI.Data;
using FoxholeIntelboard.DTO;
namespace FoxholeIntelboard.Configuration

{
    public static class ConfigureProgram
    {
        public static void BuildServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IAmmunitionManager, AmmunitionManager>();
            builder.Services.AddScoped<IMaterialManager, MaterialManager>();
            builder.Services.AddScoped<IResourceManager, ResourceManager>();
            builder.Services.AddScoped<IWeaponManager, WeaponManager>();
            builder.Services.AddScoped<ICategoryManager, CategoryManager>();
            builder.Services.AddScoped<IInventoryManager, InventoryManager>();
            builder.Services.AddScoped<ICraftableItemManager, CraftableItemManager>();
            builder.Services.AddScoped<IManagerDto, ManagerDto>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<IntelboardDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IntelboardDB")));
        }
    }
}
