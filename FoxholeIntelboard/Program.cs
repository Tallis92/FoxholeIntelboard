using FoxholeIntelboard.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Services + Dependency Injection
builder.BuildServices();

var app = builder.Build();

Console.WriteLine("Build is starting...");

// Middleware + routing etc.
app.ConfigurePipeline();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeding>();
    await seeder.SeedDatabaseOnStartupAsync();
}


app.Run();
