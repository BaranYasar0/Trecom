using Trecom.Client.MvcClient.Handlers;
using Trecom.Client.MvcClient.Services;
using Trecom.Client.MvcClient.Services.Interfaces;

namespace Trecom.Client.MvcClient.Extensions;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection RegisterRequiredServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddTransient<ExceptionHandlerDelegate>();
        return services;
    }
}