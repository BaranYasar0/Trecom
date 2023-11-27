using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Trecom.Api.Identity.Application.Features.Rules;
using Trecom.Api.Identity.Application.Helpers.JWT;
using Trecom.Api.Identity.EntityFramework;
using Trecom.Api.Identity.Services;
using Trecom.Api.Identity.Services.Interfaces;

namespace Trecom.Api.Identity.Extensions;

public static class IdentityServicesRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("SqlCon"));
        });

        services.AddScoped<ITokenHelper, JwtHelper>();
        services.AddScoped<AuthBusinessRules>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}