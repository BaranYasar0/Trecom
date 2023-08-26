using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Trecom.Api.Services.Catalog.Extensions;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Shared.Events;

namespace Trecom.Api.Services.Order.Application.Consumers
{
    public class OrderFailedRequestEventConsumer:IConsumer<OrderFailedRequestEvent>
    {
        private readonly IOrderRepository orderRepository;
        private readonly ILogger<OrderFailedRequestEventConsumer> logger;

        public OrderFailedRequestEventConsumer(IOrderRepository orderRepository, ILogger<OrderFailedRequestEventConsumer> logger)
        {
            this.orderRepository = orderRepository;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderFailedRequestEvent> context)
        {
            logger.LogInformation($"{this.GetType().Name} started to consume {context.Message.GetType().Name}");
            
            Domain.Entities.Order toBeUpdatedOrder =
                await orderRepository.GetAsync(g => g.Id == context.Message.OrderId);

            if (toBeUpdatedOrder.ValidateNullBool())
                await toBeUpdatedOrder.SetOrderStatusAsFailed();

            await orderRepository.UpdateAsync(toBeUpdatedOrder);

            logger.LogInformation($"Order with id: {toBeUpdatedOrder.Id} has been updated.");
        }
    }
}
