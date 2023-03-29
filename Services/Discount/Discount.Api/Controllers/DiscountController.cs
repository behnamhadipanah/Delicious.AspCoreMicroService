using System.Net;
using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        #region Constructor
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        #endregion

        #region Get Coupon
        [HttpGet("{productName}",Name ="GetDiscount" )]
        [ProducesResponseType(typeof(Coupon),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDiscount(string productName)
        {
            var coupon = await _discountRepository.GetDiscount(productName);
            return Ok(coupon);
        }
        #endregion

        #region Create Discount
        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreteDiscount([FromBody]Coupon coupon)
        {
            var result = await _discountRepository.CreateDiscount(coupon);

            return CreatedAtRoute("GetDiscount",new {productName=coupon.ProductName},coupon);
        }
        #endregion

        #region Update Discount
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await _discountRepository.UpdateDiscount(coupon));
        }
        #endregion

        #region Delete Discount
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            
            return Ok(await _discountRepository.DeleteDiscount(productName));
        }
        #endregion
    }
}
