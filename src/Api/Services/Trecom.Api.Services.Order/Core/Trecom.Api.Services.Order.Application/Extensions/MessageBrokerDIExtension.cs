using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trecom.Api.Services.Order.Application.Consumers;
using Trecom.Shared;

namespace Trecom.Api.Services.Order.Application.Extensions
{
    public static class MessageBrokerDIExtension
    {
        public static void AddMassTransitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<StockNotReservedEventConsumer>();
                x.AddConsumer<OrderCompletedRequestEventConsumer>();
                x.AddConsumer<OrderFailedRequestEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMQSettings"]);

                    cfg.ReceiveEndpoint(RabbitMqSettings.StockNotReservedQueueName, ce =>
                    {
                        ce.ConfigureConsumer<StockNotReservedEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(RabbitMqSettings.OrderCompletedEventQueueName, con =>
                    {
                        con.ConfigureConsumer<OrderCompletedRequestEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(RabbitMqSettings.OrderFailedQueueName, con =>
                    {
                        con.ConfigureConsumer<OrderFailedRequestEventConsumer>(context);
                    });
                });
            });
        }
    }
}
