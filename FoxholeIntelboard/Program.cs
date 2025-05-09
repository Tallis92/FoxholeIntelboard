using FoxholeIntelboard.Data;
using FoxholeIntelboard.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
Console.WriteLine("Connection string: " + builder.Configuration.GetConnectionString("IntelboardDB"));
builder.Services.AddDbContext<IntelboardDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IntelboardDB")));
builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();



var app = builder.Build();
Console.WriteLine("Build is starting...");
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
Console.WriteLine("App is starting...");
app.Run();


// Configure the HTTP request pipeline.

