using Microsoft.OpenApi.Models;
using Trecom.Api.Services.Catalog.Extensions;
using Trecom.Api.Services.Catalog.Persistance.DataSeeding;
using Trecom.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MediatRReponseCaching", Version = "v1" });
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.RegisterRequiredServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(c => { c.DisplayRequestDuration(); c.SwaggerEndpoint("/swagger/v1/swagger.json", "MediatRReponseCaching v1"); });
app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
