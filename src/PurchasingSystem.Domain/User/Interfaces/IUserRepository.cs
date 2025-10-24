namespace PurchasingSystem.Domain.User.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(Entities.User user, CancellationToken cancellationToken = default);
        Task UpdateAsync(Entities.User user);
        Task<Entities.User?> GetByIdAsync(Guid userID, CancellationToken cancellationToken = default);
        Task<bool> IsEmailInUseAsync(string email, CancellationToken cancellationToken = default);
        Task<Entities.User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task Delete(Entities.User user);
    }
}
