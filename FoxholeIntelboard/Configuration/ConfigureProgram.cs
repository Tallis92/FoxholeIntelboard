using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.DAL;
using IntelboardAPI.Data;
namespace FoxholeIntelboard.Configuration

{
    public static class ConfigureProgram
    {
        public static void BuildServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<AmmunitionManager>();
            builder.Services.AddScoped<MaterialManager>();
            builder.Services.AddScoped<ResourceManager>();
            builder.Services.AddScoped<WeaponManager>();
            builder.Services.AddScoped<CategoryManager>();
            builder.Services.AddScoped<InventoryManager>();
            builder.Services.AddScoped<CraftableItemManager>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<IntelboardDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IntelboardDB")));
        }
    }
}
