using MediatR;

namespace PurchasingSystem.Application.UseCases.User.Commands.Register
{
    public record RegisterUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Cpf,
        string Password) : IRequest<Guid>;
}
