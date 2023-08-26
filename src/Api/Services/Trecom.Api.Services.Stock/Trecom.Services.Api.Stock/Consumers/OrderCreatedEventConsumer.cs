using MassTransit;
using Microsoft.EntityFrameworkCore;
using Trecom.Services.Api.Stock.Models;
using Trecom.Shared;
using Trecom.Shared.Events;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Services.Api.Stock.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<IOrderCreatedRequestEvent>
    {
        private readonly ILogger<OrderCreatedEventConsumer> logger;
        private readonly StockDbContext dbContext;
        private readonly ISendEndpointProvider sendEndpointProvider;
        private readonly IPublishEndpoint publishEndpoint;
        public OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger, StockDbContext context, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
        {
            this.logger = logger;
            this.dbContext = context;
            this.sendEndpointProvider = sendEndpointProvider;
            this.publishEndpoint = publishEndpoint;
        }


        public async Task Consume(ConsumeContext<IOrderCreatedRequestEvent> context)
        {
            if (context?.Message?.OrderItemMessages == null ||
                context.Message.OrderItemMessages?.Count() < 1 ||
                !context.Message.OrderItemMessages.All(
                    y =>
                {
                    return dbContext.Stocks.Any(x => x.ProductId == y.ProductId);
                }
                    ))
            {
                var sendEndpoint =
                   await sendEndpointProvider.GetSendEndpoint(
                        new Uri($"queue:{RabbitMqSettings.StockNotReservedQueueName}"));

                await sendEndpoint.Send<IStockNotReservedEvent>(StockNotReservedEvent.Create(Guid.Parse(context.CorrelationId.ToString()), context.Message.OrderId,
                      "OrderItem doesnt exist!"));

                logger.LogInformation($"StockNotReserved Event sended to RabbitMQ for context Id:{context.CorrelationId}");
            }

            bool stockValidation = true;
            context.Message.OrderItemMessages.ForEach(async x =>
            {
                var stock = await dbContext.Stocks.FindAsync(x.ProductId);
                stockValidation = stock.Amount >= x.Quantity;
            });

            if (!stockValidation)
            {
                var sendEndpoint =
                    await sendEndpointProvider.GetSendEndpoint(
                        new Uri($"queue:{RabbitMqSettings.StockNotReservedQueueName}"));

                await sendEndpoint.Send<IStockNotReservedEvent>(StockNotReservedEvent.Create(context.CorrelationId.Value, context.Message.OrderId,
                    "OrderItem stock is not enough!"));
            }

            await publishEndpoint.Publish<IStockReservedEvent>(StockReservedEvent.Create(context.CorrelationId.Value,
                context.Message.OrderId, context.Message.PaymentMessage, context.Message.OrderItemMessages));
            logger.LogInformation($"StockReservedEvent sended to RabbitMq for context Id {context.CorrelationId}");


            logger.LogInformation($"Order with OrderId:{context.Message.OrderId} handled.");

        }



    }
}
