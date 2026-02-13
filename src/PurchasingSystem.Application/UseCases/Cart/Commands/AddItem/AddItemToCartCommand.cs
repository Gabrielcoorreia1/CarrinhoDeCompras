using MediatR;

namespace PurchasingSystem.Application.UseCases.Cart.Commands.AddItem
{
    public record AddItemToCartCommand(Guid UserId, Guid ProductId, int Quantity) : IRequest;
}
