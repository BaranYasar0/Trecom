using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Trecom.Api.Services.Order.Application.Features.Rules;
using Trecom.Api.Services.Order.Application.Services;
using Trecom.Api.Services.Order.Application.Services.Interfaces;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Shared.Pipelines.Authorization;
using Trecom.Shared.Pipelines.Logging;
using Trecom.Shared.Services;
using Trecom.Shared.Services.Interfaces;

namespace Trecom.Api.Services.Order.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddRequiredApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());


        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<OrderBusinessRules>();
        services.AddTransient<ISharedUserService, SharedUserService>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationPipelineBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            

        return services;
    }
}