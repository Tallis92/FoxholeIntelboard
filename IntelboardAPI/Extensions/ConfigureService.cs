using IntelboardAPI.Data;
using IntelboardAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Extensions
{
    public static class ConfigureService
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS policy to allow requests from the frontend
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                      .WithOrigins("https://localhost:7096")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
                });
            });

            // DbContext configuration

            builder.Services.AddDbContext<IntelboardDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IntelboardDB")));

            // Dependency injection for services
            builder.Services.AddScoped<IResourceService, ResourceService>();
            builder.Services.AddScoped<IMaterialService, MaterialService>();
            builder.Services.AddScoped<IAmmunitionService, AmmunitionService>();
            builder.Services.AddScoped<IWeaponService, WeaponService>();

        }
    }
}
