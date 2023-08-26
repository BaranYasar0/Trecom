using Newtonsoft.Json;

namespace Trecom.Api.Services.Order.Application.Features.Dtos;

public record OrderItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    [JsonIgnore]
    public decimal TotalPriceForOneItem
    {
        get => this.Quantity * this.Price; private set { }
    }

    public OrderItemDto(Guid productId, int quantity, decimal price)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
}