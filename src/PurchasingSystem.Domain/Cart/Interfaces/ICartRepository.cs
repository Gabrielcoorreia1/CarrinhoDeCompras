namespace PurchasingSystem.Domain.Cart.Interfaces
{
    public interface ICartRepository
    {
        Task AddAsync(Entities.Cart cart, CancellationToken cancellationToken = default);
        Task<Entities.Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken = default);
        Task RemoveAsync(Cart cart);
    }
}
