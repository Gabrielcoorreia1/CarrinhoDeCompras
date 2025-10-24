using MediatR;

namespace PurchasingSystem.Application.UseCases.User.Commands.Login
{
    public record LoginUserCommand(
        string Email,
        string Password) : IRequest<LoginUserCommandResponse>;
}
