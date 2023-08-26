using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Trecom.Api.Services.Order.Application.Features.Dtos;
using Trecom.Api.Services.Order.Application.Features.Rules;
using Trecom.Api.Services.Order.Application.Helpers;
using Trecom.Api.Services.Order.Application.Services.Interfaces;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Api.Services.Order.Domain.Enums;
using Trecom.Shared.Events;
using Trecom.Shared.Events.Interfaces;
using Trecom.Shared.Services.Interfaces;

namespace Trecom.Api.Services.Order.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ISharedUserService sharedUserService;
        private readonly IMapper mapper;
        private readonly ILogger<OrderService> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly OrderBusinessRules businessRules;
        private readonly IOrderRepository orderRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public OrderService(ISharedUserService sharedUserService, IMapper mapper, ILogger<OrderService> logger, IHttpContextAccessor httpContextAccessor, OrderBusinessRules businessRules, IOrderRepository orderRepository)
        {
            this.sharedUserService = sharedUserService;
            this.mapper = mapper;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
            this.businessRules = businessRules;
            this.orderRepository = orderRepository;
        }

        public async Task InitializeParametersForCreateOrder(Domain.Entities.Order order)
        {
            order.OrderId = OrderHelpers.GenerateOrderId();
            order.OrderStatus = OrderStatus.Started;
            order.BuyerId = Guid.Parse("b3503728-bc25-4df0-8373-f1d1d6827bc5");
            order.DeliveryCompanyId = Guid.Parse("97d50bfb-6509-4558-85e8-b4e1924c7300");
            order.OrderDetail.DeliveryNumber = OrderHelpers.GenerateDeliveryNumber();
            order.OrderDetail.DeliveryDate = DateTime.Now.AddDays(1);
            order.OrderDetail.OrderId = order.Id;

            foreach (var item in order.OrderDetail.OrderItems)
                item.OrderDetailId = order.OrderDetail.Id;
        }

        public Task<OrderCreatedRequestEvent> CreateOrderCreatedRequestEvent(Domain.Entities.Order createdOrder)
        {

            OrderCreatedRequestEvent orderEvent = new(createdOrder.Id, createdOrder.BuyerId, new PaymentMessage("123456789"));

            logger.LogInformation(orderEvent.ToString());

            return Task.FromResult(orderEvent);
        }

        public async Task<Domain.Entities.Order> CreateOrderRequest(CreateOrderDto orderDto)
        {
            Domain.Entities.Order toBeCreatedOrder = mapper.Map<Domain.Entities.Order>(orderDto);

            await InitializeParametersForCreateOrder(toBeCreatedOrder);

            await businessRules.CheckOrderParametersAreNullOrNot(toBeCreatedOrder);
            await businessRules.OrderStatusMustBeStarted(toBeCreatedOrder);
            await orderRepository.AddAsync(toBeCreatedOrder);

            await publishEndpoint.Publish(OrderCreatedRequestEvent.Create(toBeCreatedOrder.Id, toBeCreatedOrder.BuyerId, new PaymentMessage("123456789")));

            return toBeCreatedOrder;
        }
    }

    public class OrderCreatedRequestEvent : IOrderCreatedRequestEvent
    {
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
        public PaymentMessage PaymentMessage { get; set; }
        public List<OrderItemMessage> OrderItemMessages { get; set; } = new List<OrderItemMessage>();

        public OrderCreatedRequestEvent(Guid orderId, Guid buyerId, PaymentMessage paymentMessage)
        {
            OrderId = orderId;
            BuyerId = buyerId;
            PaymentMessage = paymentMessage;
        }

        public override string ToString()
        {
            return $"OrderCreatedRequestEvent has been created.\nOrderId:{OrderId},BuyerId:{BuyerId},PaymentMessage:{PaymentMessage}";
        }

        public static OrderCreatedRequestEvent Create(Guid orderId, Guid buyerId, PaymentMessage paymentMessage)
        {
            return new(orderId, buyerId, paymentMessage);
        }
    }
}
