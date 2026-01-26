using PurchasingSystem.Domain.Shared.SeedWorks;

namespace PurchasingSystem.Domain.Items.Entities
{
    public class OrderItem : Entity
    {
        private OrderItem() : base(Guid.NewGuid()) { }
        
        private OrderItem(Guid id, string name, double unitValue, int quantity, int stock) : base(id)
        {
            Name = name;
            UnitValue = unitValue;
            Quantity = quantity;
            Stock = stock;
        }
        
        public static OrderItem Create(string name, double unitValue, int quantity, int stock)
        {
            // Adicionar validações
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome não pode ser vazio", nameof(name));
            if (unitValue <= 0)
                throw new ArgumentException("Valor unitário deve ser maior que zero", nameof(unitValue));
            if (quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));
            if (stock < 0)
                throw new ArgumentException("Estoque não pode ser negativo", nameof(stock));
                
            return new OrderItem(Guid.NewGuid(), name, unitValue, quantity, stock);
        }
        
        public string Name { get; private set; }
        public double UnitValue { get; private set; }
        public int Quantity { get; private set; }
        public int Stock { get; private set; }
        public double TotalValue => UnitValue * Quantity;
    }
}
