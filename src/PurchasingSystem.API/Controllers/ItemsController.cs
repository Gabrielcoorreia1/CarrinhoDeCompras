using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PurchasingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ISender _sender;
        public ItemsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command)
        {
            var itemId = await _sender.Send(command);
            return CreatedAtAction(nameof(GetItemById), new { id = itemId }, new { id = itemId });
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] UpdateItemRequest request)
        {
            var command = new UpdateItemCommand(id, request.Name, request.Description, request.Price);
            await _sender.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var command = new DeleteItemCommand(id);
            await _sender.Send(command);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllItems()
        {
            var query = new GetAllItemsQuery();
            var items = await _sender.Send(query);
            return Ok(items);
        }

        [HttpGet("{id:guid}", Name = "GetItemById")]
        [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            var query = new GetItemByIdQuery(id);
            var item = await _sender.Send(query);
            return Ok(item);
        }
    }
}