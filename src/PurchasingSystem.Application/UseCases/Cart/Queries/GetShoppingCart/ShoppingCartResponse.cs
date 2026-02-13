namespace PurchasingSystem.Application.UseCases.Cart.Queries.GetShoppingCart
{
    public record ShoppingCartResponse(
        Guid CartId,
        Guid UserId,
        IEnumerable<CartItemResponse> Items,
        double TotalValue);
}
