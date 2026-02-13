using MediatR;

namespace PurchasingSystem.Application.UseCases.Cart.Queries.GetShoppingCart
{
    public record GetShoppingCartQuery(Guid UserId) : IRequest<ShoppingCartResponse>;
}
