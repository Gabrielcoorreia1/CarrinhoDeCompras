using PurchasingSystem.Domain.Shared.Events;

namespace PurchasingSystem.Domain.User.Events
{
    public sealed record DeletedUserEvent(Guid UserId) : IDomainEvent;
}
