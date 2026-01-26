using MediatR;

namespace PurchasingSystem.Application.UseCases.Cart.Commands.UpdateItem
{
    public record UpdateCartItemCommand(Guid UserId, Guid ProductId, int Quantity) : IRequest;
}
