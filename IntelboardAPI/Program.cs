using IntelboardAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Services + Dependency Injection
builder.AddServices();

var app = builder.Build();

// Middleware
app.ConfigureApiPipeline();

app.Run();
