namespace PurchasingSystem.Application.UseCases.Cart.Queries.GetShoppingCart
{
    public record CartItemResponse(
        Guid ProductId,
        string ProductName,
        double UnitPrice,
        int Quantity,
        double TotalPrice);
}
