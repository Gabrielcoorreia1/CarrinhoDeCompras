using PurchasingSystem.Domain.Shared.SeedWorks;
using System.Collections.ObjectModel;

namespace PurchasingSystem.Domain.Items.Entities
{
    public class Order : Entity
    {
        private Order() : base(Guid.NewGuid()) { }
        private Order(Guid id) : base(id) { }
        
        public static Order Create(Guid userId)
        {
            var order = new Order(Guid.NewGuid()) { UserId = userId };
            return order;
        }
        
        private List<OrderItem> _items = [];
        public Guid UserId { get; private set; }
        public ReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    }
}
