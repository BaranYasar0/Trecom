using Trecom.Client.MvcClient.Services;
using Trecom.Client.MvcClient.Services.Interfaces;

namespace Trecom.Client.MvcClient.Extensions;

public static class HttpServiceRegistrationExtension
{
    public static IServiceCollection RegisterHttpServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddHttpClient<IAuthService, AuthService>(x =>
        {
            x.BaseAddress = new Uri($"{configuration["GatewaySettings:GatewayUrl"]}/identity/auth/");
        });

        services.AddHttpClient("category", cfg =>
        {
            cfg.BaseAddress = new Uri($"{configuration["GatewaySettings:GatewayUrl"]}/services/catalog/");
        });

        return services;
    }
}