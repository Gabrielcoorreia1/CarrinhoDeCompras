using PurchasingSystem.Domain.Shared.SeedWorks;

namespace PurchasingSystem.Domain.Cart.Entities
{
    public class Cart : Entity
    {
        private Cart(Guid Id) : base(Id)
        {
        }
        private Cart(Guid id, Guid userId) : this(id)
        {
            UserId = userId;
        }
        public static Cart Create(Guid userId)
        {
            return new Cart(Guid.NewGuid(), userId);
        }
        public Guid UserId { get; private set; }
        public List<int> Items { get; private set; } = [];
        public void AddItem(int id) => Items.Add(id);
        public void RemoveItem(int id) => Items.Remove(id);
    }
}
