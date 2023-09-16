using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using MassTransit.Futures.Contracts;
using StackExchange.Redis;
using Trecom.Api.Services.BasketService.Models;
using Trecom.Api.Services.BasketService.Persistance.Interfaces;
using Trecom.Api.Services.BasketService.Services;
using Trecom.Api.Services.Catalog.Extensions;
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

    public BasketRepository(RedisService redis, IConfiguration configuration, ISharedUserService sharedUserService)
    {
        this.redis = redis.ValidateNull();
        this.configuration = configuration;
        this.sharedUserService = sharedUserService;
        int.TryParse(this.configuration["RedisSettings:RepositoryDB"], out int db);
        this.redisDb = this.redis.GetDb(db.ValidateNullBool() ? db : 1);
        userId = sharedUserService.GetUserIdAsync().Result;
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

    public async Task<bool> DeleteBasketAsync()
    {
        return await redisDb.HashDeleteAsync(basketKey, userId);
    }
}