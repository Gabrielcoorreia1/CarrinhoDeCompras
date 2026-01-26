using PurchasingSystem.Domain.Shared.SeedWorks;

namespace PurchasingSystem.Domain.Cart.ValueObjects
{
    public record CartItem : ValueObject
    {
        private CartItem() { }
        
        private CartItem(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
        
        public static CartItem Create(Guid productId, int quantity)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId não pode ser vazio", nameof(productId));
            if (quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));
                
            return new CartItem(productId, quantity);
        }
        
        public Guid ProductId { get; private init; }
        public int Quantity { get; private set; }
        
        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));
            Quantity = quantity;
        }
        
        public void AddQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));
            Quantity += quantity;
        }
    }
}
