using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            
        }

        public ShoppingCart(string username)
        {
            Username=username;
        }
        public string Username { get; set; }

        

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                if (ShoppingCartItems is not null && ShoppingCartItems.Any())
                {
                    foreach (var item in ShoppingCartItems)
                    {
                        totalPrice += item.Price * item.Quantity;
                    }
                }
                
                return totalPrice;
            }
        }
    }
}
