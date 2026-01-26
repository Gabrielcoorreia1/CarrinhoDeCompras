namespace PurchasingSystem.Domain.Cart.Interfaces
{
    public interface ICartRepository
    {
        Task AddAsync(Entities.Cart cart, CancellationToken cancellationToken = default);
        Task<Entities.Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken = default);
        Task<Entities.Cart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task RemoveAsync(Entities.Cart cart);
        Task UpdateAsync(Entities.Cart cart);
    }
}
