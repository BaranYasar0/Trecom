using Microsoft.Extensions.Caching.Distributed;
using Trecom.Api.Services.Order.Domain.Enums;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Domain.Entities;

public class OrderDetail:BaseEntity
{
    public int DeliveryNumber { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DeliveryType? DeliveryType { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }=new List<OrderItem>();
    public Address Address { get; set; }
}