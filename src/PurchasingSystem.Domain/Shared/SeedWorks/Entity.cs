using PurchasingSystem.Domain.Shared.Events;

namespace PurchasingSystem.Domain.Shared.SeedWorks
{
    public abstract class Entity(Guid Id) : IEquatable<Entity>
    {
        private readonly List<IDomainEvent> _events = [];
        public Guid Id { get; } = Id;
        public DateTime CreatedAt { get; }
        public IReadOnlyList<IDomainEvent> GetDomainEvents => _events;
        protected void RaiseDomainEvents(IDomainEvent @event) => _events.Add(@event);
        public void ClearDomainEvents() => _events.Clear();
        public bool Equals(Entity? other) => other != null && other.Id == Id;
        public override bool Equals(object obj) => Equals(obj as Entity);
        public override int GetHashCode() => Id.GetHashCode();

    }
}
