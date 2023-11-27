using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using MassTransit.Futures.Contracts;
using StackExchange.Redis;
using Trecom.Api.Services.BasketService.Models;
using Trecom.Api.Services.BasketService.Persistance.Interfaces;
using Trecom.Api.Services.BasketService.Services;
using Trecom.Shared.CCS.GlobalException;
using Trecom.Shared.Extensions;
using Trecom.Shared.Services.Interfaces;

namespace Trecom.Api.Services.BasketService.Persistance;

public class BasketRepository : IBasketRepository
{
    private readonly RedisService redis;
    private readonly IDatabase redisDb;
    private readonly IConfiguration configuration;
    private const string basketKey = "basket";
    private readonly ISharedUserService sharedUserService;
    private string userId = String.Empty;
    private readonly int maxBasketItemCount;

    public BasketRepository(RedisService redis, IConfiguration configuration, ISharedUserService sharedUserService)
    {
        this.redis = redis.ValidateNull();
        this.configuration = configuration;
        this.sharedUserService = sharedUserService;
        int.TryParse(this.configuration["RedisSettings:RepositoryDB"], out int db);
        this.redisDb = this.redis.GetDb(db.ValidateNullBool() ? db : 1);
        userId = sharedUserService.GetUserIdAsync().Result;
        int.TryParse(configuration["MaxBasketCount"], out int itemCount);
        maxBasketItemCount = itemCount;
    }

    public async Task<Basket> GetBasketAsync()
    {
        if (await redisDb.KeyExistsAsync(basketKey))
        {
            var data = await redisDb.HashGetAsync(basketKey, userId);

            return data.HasValue ? JsonSerializer.Deserialize<Basket>(data) : new Basket();
        }

        return new Basket();
    }

    public async Task<bool> UpdateBasketAsync(UpdateBasketDto basketDto)
    {
        List<BasketItem> basketItems = basketDto.Items.Select(x => BasketItem.Create(x.productId, x.Price, x.Quantity)).ToList();
            
        Basket basket = Basket.Create(Guid.Parse(userId), basketItems);

        return await redisDb.HashSetAsync(basketKey, basket.BuyerId.ToString(), JsonSerializer.Serialize(basket));
    }

    public async Task<bool> AddBasketItemAsync(BasketItem basketItem)
    {
        Basket basket = await GetBasketAsync();
        if (basket.BasketItems.Any(x => x.ProductId == basketItem.ProductId))
        {
            basket.BasketItems.FirstOrDefault(x => x.ProductId == basketItem.ProductId).Quantity += basketItem.Quantity;
        }
        else
        {
            basket.BasketItems.Add(basketItem);
        }

        return await redisDb.HashSetAsync(basketKey, basket.BuyerId.ToString(), JsonSerializer.Serialize(basket));
    }

    public async Task<bool> ChangeBasketItemCount(BasketItem basketItem, int count)
    {
        Basket basket = await GetBasketAsync();
        BasketItem existBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == basketItem.ProductId);
        if (existBasketItem is not null)
        {
            if (count > 0)
            {
                existBasketItem.Quantity += count;
            }
            else if(count < 0)
                
            {
                if(!CheckAndRemoveBasketItemIfCountIsZero(basket, existBasketItem,count))
                   existBasketItem.Quantity -= Math.Abs(count);
            }
            else
            {
                //Do Nothing
            }
        }
        else
        {
            throw new BusinessException("Basket item not found");
        }

        return await redisDb.HashSetAsync(basketKey,
            basket.BuyerId.ToString(),
            JsonSerializer.Serialize(basket));
    }

    public async Task<bool> DeleteBasketAsync()
    {
        return await redisDb.HashDeleteAsync(basketKey, userId);
    }

    private bool CheckAndRemoveBasketItemIfCountIsZero(Basket basket,BasketItem basketItem,int count)
    {
        if (basketItem.Quantity - Math.Abs(count) <= 0)
        {
            basket.BasketItems.Remove(basketItem);
            return true;
        }
        return false;
    }
}