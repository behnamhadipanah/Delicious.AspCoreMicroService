using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region constructor

        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region get orders
        [HttpGet("{username}",Name = "GetOrders")]
        [ProducesResponseType(typeof(IEnumerable<OrderViewModel>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrderByUsername(string username)
        {
            var query = new GetOrdersListQuery(username);
            var orders = await _mediator.Send(query);
            return orders;
        }

        #endregion

        #region checkout order

        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        #endregion

        #region update order


        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion


        #region Delete Order
        [HttpDelete("{id}",Name = "DeleteOrder")]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder( int id)
        {
            await _mediator.Send(new DeleteOrderCommand(id));
            return NoContent();
        }


        #endregion
    }
}
