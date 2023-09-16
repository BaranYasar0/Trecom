using MassTransit;
using Trecom.Api.Services.BasketService.Models;
using Trecom.Api.Services.BasketService.Persistance.Interfaces;
using Trecom.Api.Services.Catalog.Extensions;
using Trecom.Shared.Events;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Api.Services.BasketService.Services;

public class BasketService
{
    private readonly IBasketRepository basketRepository;
    private readonly IPublishEndpoint publishEndpoint;

    public BasketService(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint)
    {
        this.basketRepository = basketRepository;
        this.publishEndpoint = publishEndpoint;
    }

    public async Task CheckOutBasketAsync(PaymentMessage payment,AddressMessage address)
    {
        Basket basket = await basketRepository.GetBasketAsync();

        basket.ValidateNullBusiness();
        BasketItem.ValidateBasketItem(basket.BasketItems);

        await publishEndpoint.Publish<IBasketCheckOutEvent>(new BasketCheckOutEvent(BasketItem.ToBasketItems(basket.BasketItems), payment,
            basket.BuyerId,address));
    }
}