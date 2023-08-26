using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"configuration.development.json").AddEnvironmentVariables();
});

builder.Services.AddOcelot();


var app = builder.Build();

// Configure the HTTP request pipeline.

await app.UseOcelot();




app.Run();
