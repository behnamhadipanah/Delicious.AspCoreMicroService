using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }
    public async Task<ShoppingCart> GetUserBasket(string username)
    {
        var basket = await _redisCache.GetStringAsync(username);
        if (string.IsNullOrEmpty(basket))
            return null;

        return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
    {
        await _redisCache.SetStringAsync(shoppingCart.Username, JsonConvert.SerializeObject(shoppingCart));
        return await GetUserBasket(shoppingCart.Username);
    }

    public async Task<bool> DeleteBasket(string username)
    {
        await _redisCache.RemoveAsync(username);
        var result = await GetUserBasket(username);
        if (result is null) return true;

        return false;

    }
}