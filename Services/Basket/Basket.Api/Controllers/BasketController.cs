using System.Net;
using Basket.Api.Entities;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        #region Constructor

        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        #endregion

        #region get basket
        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string username)
        {
            var basket = await _basketRepository.GetUserBasket(username);
            return Ok(basket ?? new ShoppingCart(username));
        }
        #endregion


        #region update basket
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            return Ok(await _basketRepository.UpdateBasket(shoppingCart));
        }
        #endregion

        #region Remove Basket
        [HttpDelete("{username}",Name = "DeleteBasket")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            return Ok(await _basketRepository.DeleteBasket(username));
        }
        

        #endregion
    }
}
