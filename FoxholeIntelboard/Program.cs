
using Microsoft.EntityFrameworkCore;
using FoxholeIntelboard.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<AmmunitionManager>();
builder.Services.AddScoped<MaterialManager>();
builder.Services.AddScoped<ResourceManager>();


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

app.Run();


