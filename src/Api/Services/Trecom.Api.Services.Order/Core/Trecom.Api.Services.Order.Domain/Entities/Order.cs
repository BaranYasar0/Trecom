using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Trecom.Api.Services.Order.Domain.Enums;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int OrderId { get; set; }
        public string? Description { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public Guid BuyerId { get; set; }
        public Buyer Buyer { get; set; }
        public Guid OrderDetailId { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public Guid DeliveryCompanyId { get; set; }
        public DeliveryCompany? DeliveryCompany { get; set; }

        public Order()
        {

        }

        public Order(Guid orderDetailId)
        {
            OrderDetailId = orderDetailId;
        }

        public Order(OrderDetail orderDetail)
        {
            OrderDetail = orderDetail;
            OrderDetailId = orderDetail.Id;
        }
        public Order(int orderId, Guid buyerId, Guid deliveryCompanyId)
        {
            OrderId = orderId;
            BuyerId = buyerId;
            DeliveryCompanyId = deliveryCompanyId;
        }

        public Task SetOrderStatusAsFailed()=>
            Task.FromResult(OrderStatus = OrderStatus.StockFailed);

        public Task SetOrderStatusAsCompleted() =>
            Task.FromResult(this.OrderStatus = OrderStatus.Completed);
    }
}
