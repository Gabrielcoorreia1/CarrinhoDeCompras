using MediatR;
using PurchasingSystem.Application.Services.Abstractions;
using PurchasingSystem.Domain.Cart.Errors;
using PurchasingSystem.Domain.Cart.Interfaces;
using PurchasingSystem.Domain.Shared.Exceptions;

namespace PurchasingSystem.Application.UseCases.Cart.Queries.GetShoppingCart
{
    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCartResponse>
    {
        private readonly ICartRepository _cartRepository;
        
        public GetShoppingCartQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        
        public async Task<ShoppingCartResponse> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            
            if (cart is null)
                throw new DomainException(CartDomainErrors.Cart.NotFound);
            
            // Para este exemplo simplificado, vamos retornar os itens sem buscar detalhes dos produtos
            // Em um cenário real, você buscaria os detalhes do produto de um IItemRepository
            var items = cart.Items.Select(item => new CartItemResponse(
                item.ProductId,
                $"Product {item.ProductId}", // Placeholder - deveria buscar nome real do produto
                0.0, // Placeholder - deveria buscar preço real do produto
                item.Quantity,
                0.0 // Placeholder - deveria calcular preço total
            )).ToList();
            
            return new ShoppingCartResponse(cart.Id, cart.UserId, items, 0.0);
        }
    }
}
