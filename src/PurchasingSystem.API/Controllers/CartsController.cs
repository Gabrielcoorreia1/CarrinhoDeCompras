using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PurchasingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ISender _sender;

        public CartsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCart()
        {
            var query = new GetShoppingCartQuery();
            var cart = await _sender.Send(query);
            return Ok(cart);
        }

        [HttpPost("items")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddItem([FromBody] AddItemToCartCommand command)
        {
            await _sender.Send(command);
            return NoContent();
        }

        [HttpPut("items/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateItemQuantity(Guid productId, [FromBody] UpdateCartItemRequest request)
        {
            var command = new UpdateCartItemCommand(productId, request.Quantity);
            await _sender.Send(command);
            return NoContent();
        }

        [HttpDelete("items/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveItem(Guid productId)
        {
            var command = new RemoveItemFromCartCommand(productId);
            await _sender.Send(command);
            return NoContent();
        }
    }
}
