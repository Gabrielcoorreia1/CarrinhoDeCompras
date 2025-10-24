namespace PurchasingSystem.Application.Services.Abstractions
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
