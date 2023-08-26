using System.Text;

using Trecom.Api.Services.BasketService.Persistance;
using Trecom.Api.Services.BasketService.Services;
using Trecom.Shared.Extensions;
using Trecom.Shared.Models;
using Trecom.Shared.Services;
using Trecom.Shared.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<RedisService>();
builder.Services.AddScoped<BasketRepository>();
builder.Services.AddScoped<ISharedUserService, SharedUserService>();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthenticationParameters(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}  

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
