using System.Reflection;
using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Trecom.Api.Identity.Application.Features.Profile;
using Trecom.Api.Identity.Application.Features.Rules;
using Trecom.Api.Identity.Application.Helpers.Encryption;
using Trecom.Api.Identity.Application.Helpers.JWT;
using Trecom.Api.Identity.Application.Observers;
using Trecom.Api.Identity.Application.Observers.User;
using Trecom.Api.Identity.EntityFramework;
using Trecom.Api.Identity.Services;
using Trecom.Api.Identity.Services.Interfaces;
using Trecom.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMqSettings:Host"]);
        
    });
});

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenHelper, JwtHelper>();
builder.Services.AddScoped<AuthBusinessRules>();

builder.Services.AddScoped<ObserverBuilder<IUserObserver>>(sp =>
{
    UserObserverBuilder observerBuilder = new UserObserverBuilder();

    observerBuilder.RegisterObserver(new UserLogToElasticAndConsole(sp));
    observerBuilder.RegisterObserver(new UserSendEmail(sp));

    return observerBuilder;
});

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureCustomExceptionMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
