using PurchasingSystem.Domain.Shared.SeedWorks;
using System.Collections.ObjectModel;

namespace PurchasingSystem.Domain.Items.Entities
{
    public class Order : Entity
    {
        private Order() : base(Guid.NewGuid()) { }
        private Order(
            Guid id) : base(id);
        public Order Create()
        {
            var order = new Order();

            return order;
        }
        private List<OrderItem> _items = [];
        public Guid UserId { get; }
        public ReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    }
}
