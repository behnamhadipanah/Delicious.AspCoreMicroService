using Basket.Api.Entities;

namespace Basket.Api.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart> GetUserBasket(string username);
    Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart);
    Task<bool> DeleteBasket(string username);
}