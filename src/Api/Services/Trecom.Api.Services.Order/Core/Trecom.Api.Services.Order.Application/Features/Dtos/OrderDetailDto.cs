using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Api.Services.Order.Domain.Enums;

namespace Trecom.Api.Services.Order.Application.Features.Dtos
{
    public record OrderDetailDto
    {
        public List<OrderItemDto> OrderItems { get; set; }=new List<OrderItemDto>();
        public AddressDto Address { get; set; }
        public DeliveryType? DeliveryType { get; set; } = 0;

        public OrderDetailDto(AddressDto address, DeliveryType? deliveryType,List<OrderItemDto> orderItems)
        {
            Address = address;
            DeliveryType = deliveryType;
            OrderItems = orderItems;
        }

        [JsonIgnore]
        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.TotalPriceForOneItem); private set{} }

    }
}
