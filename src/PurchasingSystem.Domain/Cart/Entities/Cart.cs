using PurchasingSystem.Domain.Cart.ValueObjects;
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
        private readonly List<CartItem> _items = [];
        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
        
        public void AddItem(Guid productId, int quantity = 1)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId não pode ser vazio", nameof(productId));
            if (quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));
            
            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.AddQuantity(quantity);
            }
            else
            {
                _items.Add(CartItem.Create(productId, quantity));
            }
        }
        
        public void RemoveItem(Guid productId)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _items.Remove(item);
            }
        }
        
        public void UpdateItemQuantity(Guid productId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));
            
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                throw new InvalidOperationException("Item não encontrado no carrinho");
            
            item.UpdateQuantity(quantity);
        }
        
        public void Clear()
        {
            _items.Clear();
        }
        
        public int TotalItems => _items.Sum(i => i.Quantity);
    }
}
