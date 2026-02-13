using PurchasingSystem.Domain.Shared.Errors;

namespace PurchasingSystem.Domain.Cart.Errors
{
    public static class CartDomainErrors
    {
        public static class Cart
        {
            public static readonly Error NotFound = new("404", "Carrinho não encontrado", ErrorType.NotFound);
            public static readonly Error AlreadyExists = new("409", "Usuário já possui um carrinho", ErrorType.Conflict);
            public static readonly Error ItemNotFound = new("404", "Item não encontrado no carrinho", ErrorType.NotFound);
            public static readonly Error InvalidQuantity = new("400", "Quantidade inválida", ErrorType.Validation);
        }
        
        public static class Item
        {
            public static readonly Error NotFound = new("404", "Produto não encontrado", ErrorType.NotFound);
        }
    }
}
