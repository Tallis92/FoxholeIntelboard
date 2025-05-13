using IntelboardAPI.Data;
using IntelboardAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
          .WithOrigins("https://localhost:7096")  // your Razor-Pages app origin
          .AllowAnyHeader()                        // allow all headers (e.g. Authorization, Content-Type)
          .AllowAnyMethod()                        // allow GET, POST, PUT, DELETE, etc.
          .AllowCredentials();                     // if you need cookies/auth
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<IntelboardDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IntelboardDB")));

builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();

var app = builder.Build();

app.UseCors("AllowFrontend");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();