using IntelboardAPI.Data;
using IntelboardCore.Services;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Extensions
{
    public static class ConfigureService
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllers()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.WriteIndented = true; // optional
             });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
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
            builder.Services.AddScoped<IResourceService>(provider =>
            {
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                var httpClient = provider.GetRequiredService<HttpClient>();
                return new ResourceService(env.ContentRootPath, httpClient);
            });
            builder.Services.AddScoped<IMaterialService>(provider =>
            {
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                var httpClient = provider.GetRequiredService<HttpClient>();
                return new MaterialService(env.ContentRootPath, httpClient);
            });
            builder.Services.AddScoped<IAmmunitionService>(provider =>
            {
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                var httpClient = provider.GetRequiredService<HttpClient>();
                return new AmmunitionService(env.ContentRootPath, httpClient);
            });
            builder.Services.AddScoped<IWeaponService>(provider =>
            {
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                var httpClient = provider.GetRequiredService<HttpClient>();
                return new WeaponService(env.ContentRootPath, httpClient);
            });
            builder.Services.AddScoped<ICategoryService>(provider =>
            {
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                var httpClient = provider.GetRequiredService<HttpClient>();
                return new CategoryService(env.ContentRootPath, httpClient);
            });
        }
    }
}
