using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Api.Services.Order.Application.Features.Dtos;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Shared.Events;

namespace Trecom.Api.Services.Order.Application.Services.Interfaces;

public interface IOrderService
{
    public Task InitializeParametersForCreateOrder(Domain.Entities.Order order);
    public Task<OrderCreatedRequestEvent> CreateOrderCreatedRequestEvent(Domain.Entities.Order createdOrder);
    public Task<Domain.Entities.Order> CreateOrderRequest(CreateOrderDto orderDto);
}