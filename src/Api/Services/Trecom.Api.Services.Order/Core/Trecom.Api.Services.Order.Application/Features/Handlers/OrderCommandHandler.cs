using System.Security.Claims;
using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Trecom.Api.Services.Order.Application.Features.Commands;
using Trecom.Api.Services.Order.Application.Features.Dtos;
using Trecom.Api.Services.Order.Application.Features.Rules;
using Trecom.Api.Services.Order.Application.Helpers;
using Trecom.Api.Services.Order.Application.Services;
using Trecom.Api.Services.Order.Application.Services.Interfaces;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Shared.Events;

namespace Trecom.Api.Services.Order.Application.Features.Handlers;

public class OrderCommandHandler:
    IRequestHandler<CreateOrderCommand,OrderResponseDto>
{
    private readonly IOrderRepository orderRepository;
    private readonly IOrderDetailRepository orderDetailRepository;
    private readonly IMapper mapper;
    private readonly IOrderService orderService;
    private readonly IDeliveryCompanyRepository deliveryRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly OrderBusinessRules businessRules;
    private readonly IPublishEndpoint publishEndpoint;

    public OrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IOrderService orderService, IOrderDetailRepository orderDetailRepository, IDeliveryCompanyRepository deliveryRepository, IHttpContextAccessor httpContextAccessor, OrderBusinessRules businessRules, IPublishEndpoint publishEndpoint)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
        this.orderService = orderService;
        this.orderDetailRepository = orderDetailRepository;
        this.deliveryRepository = deliveryRepository;
        this.httpContextAccessor = httpContextAccessor;
        this.businessRules = businessRules;
        this.publishEndpoint = publishEndpoint;
    }

    public async Task<OrderResponseDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Order order=await orderService.CreateOrderRequest(request.CreateOrderDto);

        return mapper.Map<OrderResponseDto>(order);
    }
}