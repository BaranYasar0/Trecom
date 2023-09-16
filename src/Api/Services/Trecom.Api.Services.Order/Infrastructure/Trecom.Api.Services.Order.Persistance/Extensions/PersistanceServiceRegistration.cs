using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Persistance.Contexts;
using Trecom.Api.Services.Order.Persistance.Repositories;

namespace Trecom.Api.Services.Order.Persistance.Extensions;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddRequiredPersistanceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(x =>
        {
            x.UseSqlServer(configuration.GetConnectionString("SqlCon"))
                ;
        });


        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IDeliveryCompanyRepository, DeliveryCompanyRepository>();
        services.AddScoped<IBuyerRepository, BuyerRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        return services;
    }
}