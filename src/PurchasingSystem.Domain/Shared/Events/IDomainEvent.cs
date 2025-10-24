using MediatR;

namespace PurchasingSystem.Domain.Shared.Events
{
    public abstract record IDomainEvent : INotification;
}
