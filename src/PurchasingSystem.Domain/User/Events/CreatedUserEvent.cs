using PurchasingSystem.Domain.Shared.Events;

namespace PurchasingSystem.Domain.User.Events
{
    public sealed record CreatedUserEvent(Guid UserId) : IDomainEvent;
}
