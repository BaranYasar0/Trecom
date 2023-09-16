using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Domain.Enums;
using Trecom.Shared.CCS.GlobalException;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Application.Features.Rules;

public class OrderBusinessRules
{
    private readonly IOrderRepository orderRepository;

    public OrderBusinessRules(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public Task CheckOrderParametersAreNullOrNot(Domain.Entities.Order order)
    {
        if (order == null || order.OrderDetail is null || order.OrderDetail?.OrderItems == null ||
            order.OrderDetail.OrderItems.Count < 1)
            throw new BusinessException($"{order.Id} has null parameter");

        return Task.CompletedTask;
    }

    public Task OrderStatusMustBeStarted(Domain.Entities.Order order)
    {
        if (order.OrderStatus != OrderStatus.Started)
            throw new BusinessException($"{order.Id} hasnt {OrderStatus.Started}");
        return Task.CompletedTask;
    }
}