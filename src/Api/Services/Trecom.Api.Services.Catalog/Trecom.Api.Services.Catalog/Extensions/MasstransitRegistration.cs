using MassTransit;
using Trecom.Api.Services.Catalog.Application.Consumers;
using Trecom.Api.Services.Catalog.Application.Events;
using Trecom.Shared;

namespace Trecom.Api.Services.Catalog.Extensions
{
    public static class MasstransitRegistration
    {
        internal static IServiceCollection AddMassTransitServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UpdateBrandAndSupplierForCreateProductEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMqSettings"]);
                    
                    cfg.ReceiveEndpoint(RabbitMqSettings.UpdateBrandAndSupplierForCreateProductEvent, e =>
                    {
                        e.ConfigureConsumer<UpdateBrandAndSupplierForCreateProductEventConsumer>(context);
                    });

                });
            });

            return services;
        }
    }
}
