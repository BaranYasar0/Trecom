using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Domain.Entities;

public class OrderItem:BaseEntity
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public Guid OrderDetailId { get; set; }
    public OrderDetail OrderDetail { get; set; }

    public OrderItem()
    {
        
    }

    public OrderItem(Guid productId, string productName, int quantity, decimal price, Guid orderDetailId)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
        OrderDetailId = orderDetailId;
    }
}