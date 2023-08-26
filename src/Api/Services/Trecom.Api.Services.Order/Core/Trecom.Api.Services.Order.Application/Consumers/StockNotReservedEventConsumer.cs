using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Trecom.Api.Services.Catalog.Extensions;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Domain.Enums;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Api.Services.Order.Application.Consumers
{
    public class StockNotReservedEventConsumer:IConsumer<IStockNotReservedEvent>
    {
        private readonly ILogger<StockNotReservedEventConsumer> logger;
        private readonly IOrderRepository orderRepository;
        public StockNotReservedEventConsumer(ILogger<StockNotReservedEventConsumer> logger, IOrderRepository orderRepository)
        {
            this.logger = logger;
            this.orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<IStockNotReservedEvent> context)
        {
            logger.LogInformation($"StockNotReservedEvent started for consume with orderId:{context.Message.OrderId}");
            Domain.Entities.Order? order = await orderRepository.GetAsync(x => x.Id == context.Message.OrderId);

            order.ValidateNull();

            order.SetOrderStatusAsFailed();

            await orderRepository.UpdateAsync(order);

            logger.LogInformation($"Order status updated as {Enum.GetName(typeof(OrderStatus),OrderStatus.StockFailed)} for order id:{order.Id}");

        }
    }
}
