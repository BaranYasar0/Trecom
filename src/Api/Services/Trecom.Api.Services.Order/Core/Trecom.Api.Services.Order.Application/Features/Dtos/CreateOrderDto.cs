using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Api.Services.Order.Domain.Enums;

namespace Trecom.Api.Services.Order.Application.Features.Dtos;

public record CreateOrderDto
{
    public OrderDetailDto OrderDetail { get; set; }

    public CreateOrderDto(OrderDetailDto orderDetail)
    {
        OrderDetail = orderDetail;
    }
}