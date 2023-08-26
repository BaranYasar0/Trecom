namespace Trecom.Shared.Events;

public class OrderItemMessage
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public OrderItemMessage(Guid productId, int quantity, decimal price)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public static OrderItemMessage Create(Guid productId, int quantity, decimal price)
    {
        return new OrderItemMessage(productId, quantity, price);
    }
}