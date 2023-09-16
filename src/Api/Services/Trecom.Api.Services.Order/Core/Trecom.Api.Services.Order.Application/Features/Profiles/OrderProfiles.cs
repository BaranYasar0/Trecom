using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Trecom.Api.Services.Order.Application.Features.Dtos;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Shared.Events;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Api.Services.Order.Application.Features.Profiles;

public class OrderProfiles:Profile
{
    public OrderProfiles()
    {
        //CreateMap<CreateOrderDto, Domain.Entities.Order>();
                
        CreateMap<CreateOrderDto, Domain.Entities.Order>()
            .ForPath(x => x.OrderDetail.Address, y => y.MapFrom(x => x.OrderDetail.Address))
            .ForPath(x => x.OrderDetail.OrderItems, y => y.MapFrom(x => x.OrderDetail.OrderItems)); 

        CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
        CreateMap<AddressDto, Address>().ReverseMap();
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();

        CreateMap<Domain.Entities.Order, OrderResponseDto>();

        CreateMap<OrderItem, OrderItemMessage>().ReverseMap();

        CreateMap<IBasketCheckOutEvent, OrderDetailDto>()
            .ForMember(x => x.OrderItems, y => y.MapFrom(x => x.OrderItemMessages))
            .ForMember(x => x.Address, y => y.MapFrom(x => x.AddressMessage));
    }
}