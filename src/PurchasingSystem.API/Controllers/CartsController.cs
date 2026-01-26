using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchasingSystem.API.Contracts.Cart;
using PurchasingSystem.Application.UseCases.Cart.Commands.AddItem;
using PurchasingSystem.Application.UseCases.Cart.Commands.RemoveItem;
using PurchasingSystem.Application.UseCases.Cart.Commands.UpdateItem;
using PurchasingSystem.Application.UseCases.Cart.Queries.GetShoppingCart;

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

        [HttpGet("{userId:guid}")]
        [ProducesResponseType(typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCart(Guid userId)
        {
            var query = new GetShoppingCartQuery(userId);
            var cart = await _sender.Send(query);
            return Ok(cart);
        }

        [HttpPost("{userId:guid}/items")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddItem(Guid userId, [FromBody] AddItemToCartCommand command)
        {
            var updatedCommand = command with { UserId = userId };
            await _sender.Send(updatedCommand);
            return NoContent();
        }

        [HttpPut("{userId:guid}/items/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateItemQuantity(Guid userId, Guid productId, [FromBody] UpdateCartItemRequest request)
        {
            var command = new UpdateCartItemCommand(userId, productId, request.Quantity);
            await _sender.Send(command);
            return NoContent();
        }

        [HttpDelete("{userId:guid}/items/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveItem(Guid userId, Guid productId)
        {
            var command = new RemoveItemFromCartCommand(userId, productId);
            await _sender.Send(command);
            return NoContent();
        }
    }
}
