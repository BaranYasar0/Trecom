using Trecom.Api.Services.BasketService.Models;

namespace Trecom.Api.Services.BasketService.Persistance.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasketAsync();

        Task<bool> UpdateBasketAsync(UpdateBasketDto basket);

        Task<bool> DeleteBasketAsync();
    }
}
