using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MassTransit.Transports;
using Trecom.Api.Services.Order.Application.Features.Dtos;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Application.Services;
using Trecom.Api.Services.Order.Application.Services.Interfaces;
using Trecom.Shared.Events;
using Trecom.Shared.Events.Interfaces;
using Trecom.Api.Services.Order.Domain.Entities;

namespace Trecom.Api.Services.Order.Application.Consumers
{
    public class BasketCheckOutEventConsumer : IConsumer<IBasketCheckOutEvent>
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public BasketCheckOutEventConsumer(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IBasketCheckOutEvent> context)
        {
            OrderDetailDto orderDetailDto = mapper.Map<OrderDetailDto>(context.Message);

            await orderService.CreateOrderRequest(new CreateOrderDto(orderDetailDto));
        }
    }
}
