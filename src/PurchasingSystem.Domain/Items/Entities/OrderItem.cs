using PurchasingSystem.Domain.Shared.SeedWorks;

namespace PurchasingSystem.Domain.Items.Entities
{
    public class OrderItem : Entity
    {
        private OrderItem() : base(Guid.NewGuid()) { }
        private OrderItem() : base() { }
        public Name Name { get; }
        public double UnitValue { get; }
        public int Quantity { get; }
        public int Stock { get; }
        public double TotalValue => UnitValue * Quantity;
    }
}
