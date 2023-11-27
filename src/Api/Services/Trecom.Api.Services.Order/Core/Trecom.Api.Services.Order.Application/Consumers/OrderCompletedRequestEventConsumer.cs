using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Shared.Events.Interfaces;
using Trecom.Shared.Extensions;

namespace Trecom.Api.Services.Order.Application.Consumers;

public class OrderCompletedRequestEventConsumer:IConsumer<IOrderCompletedRequestEvent>
{
    private readonly IOrderRepository orderRepository;
    private readonly ILogger<OrderCompletedRequestEventConsumer> logger;

    public OrderCompletedRequestEventConsumer(IOrderRepository orderRepository, ILogger<OrderCompletedRequestEventConsumer> logger)
    {
        this.orderRepository = orderRepository.ValidateNull();
        this.logger = logger;
    }

    public async Task Consume(ConsumeContext<IOrderCompletedRequestEvent> context)
    {
        logger.LogInformation($"{this.GetType().Name} started to consume {context.Message.GetType().Name}");
        Domain.Entities.Order toBeUpdatedOrder =
            await orderRepository.GetAsync(g => g.Id == context.Message.OrderId);

        if(toBeUpdatedOrder.ValidateNullBool())
            await toBeUpdatedOrder.SetOrderStatusAsCompleted();

        await orderRepository.UpdateAsync(toBeUpdatedOrder);

        logger.LogInformation($"Order with id: {toBeUpdatedOrder.Id} has been updated.");
    }
}