using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Api.Services.Order.Domain.Enums;

namespace Trecom.Api.Services.Order.Application.Features.Dtos;

public class OrderResponseDto
{
    public Guid Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
}