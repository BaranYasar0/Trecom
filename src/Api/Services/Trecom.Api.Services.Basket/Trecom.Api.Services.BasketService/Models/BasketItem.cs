using Trecom.Shared.CCS.GlobalException;
using Trecom.Shared.Events;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.BasketService.Models;

public class BasketItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public static BasketItem Create(Guid productId, decimal price, int quantity)
    {
        var tempBasketItem = new BasketItem
        {
            ProductId = productId,
            Price = price,
            Quantity = quantity
        };
        ValidateBasketItem(new List<BasketItem>() { tempBasketItem });
        return tempBasketItem;
    }

    public static void ValidateBasketItem(List<BasketItem> basketItems)
    {
        foreach (var basketItem in basketItems)
        {
            if (basketItem.Quantity <= 0)
            {
                throw new BusinessException($"{basketItem.Quantity.GetType().Name} must be greater than 0");
            }

            else if (basketItem.Price <= 0)
            {
                throw new BusinessException($"{basketItem.Price.GetType().Name} must be greater than 0");
            }

            else if (basketItem.ProductId == Guid.Empty)
            {
                throw new BusinessException($"ProductId Cannot be Null!");
            }
        }

    }

    public static List<OrderItemMessage> ToBasketItems(List<BasketItem> items) => items.Select(x => OrderItemMessage.Create(x.ProductId, x.Quantity, x.Price)).ToList();

}