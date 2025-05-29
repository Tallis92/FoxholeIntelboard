using FoxholeIntelboard.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Services + Dependency Injection
builder.BuildServices();

var app = builder.Build();

Console.WriteLine("Build is starting...");

// Middleware + routing etc.
app.ConfigurePipeline();

app.Run();
