using MediatR;

namespace PurchasingSystem.Application.UseCases.User.Commands.Update
{
    public record UpdateUserCommand(Guid Id, string FirstName, string LastName, string Email, string password) : IRequest;
}
