using MediatR;

namespace PurchasingSystem.Application.UseCases.Cart.Commands.Create
{
    public record CreateCartCommand(Guid UserId) : IRequest;
}
