using MediatR;

namespace PurchasingSystem.Application.UseCases.Cart.Commands.RemoveItem
{
    public record RemoveItemFromCartCommand(Guid UserId, Guid ProductId) : IRequest;
}
