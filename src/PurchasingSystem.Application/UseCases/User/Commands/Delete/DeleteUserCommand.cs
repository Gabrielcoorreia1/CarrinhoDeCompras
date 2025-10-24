using MediatR;

namespace PurchasingSystem.Application.UseCases.User.Commands.Delete
{
    public record DeleteUserCommand(Guid UserID) : IRequest;
}
