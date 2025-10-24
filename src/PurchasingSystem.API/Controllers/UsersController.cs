using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchasingSystem.Application.UseCases.User.Commands.Delete;
using PurchasingSystem.Application.UseCases.User.Commands.Login;
using PurchasingSystem.Application.UseCases.User.Commands.Register;
using PurchasingSystem.Application.UseCases.User.Commands.Update;
using PurchasingSystem.Application.UseCases.User.Queries.GetUserById;

namespace PurchasingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var userId = await _sender.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, new { id = userId });
        }

        [HttpGet("{id:guid}", Name = "GetUserById")]
        [ProducesResponseType(typeof(GetUserByIdQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery(id);
            var userResponse = await _sender.Send(query);
            return Ok(userResponse);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginUserCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var loginResponse = await _sender.Send(command);

            return Ok(loginResponse);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand request)
        {
            var command = new UpdateUserCommand(id, request.FirstName, request.LastName, request.Email, request.password);

            await _sender.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand(id);

            await _sender.Send(command);

            return NoContent();
        }
    }
}
