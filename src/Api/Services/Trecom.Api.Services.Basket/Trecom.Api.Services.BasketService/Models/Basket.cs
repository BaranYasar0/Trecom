using Trecom.Shared.Models;

namespace Trecom.Api.Services.BasketService.Models
{
    public class Basket:BaseEntity
    {
        public Guid BuyerId { get; set; }
        public List<BasketItem> BasketItems { get; set; }

        public static Basket Create(Guid buyerId, List<BasketItem> items)
        {
            BasketItem.ValidateBasketItem(items);
            return new Basket()
            {
                BuyerId = buyerId,
                BasketItems = items
            };
        }
    }
}
