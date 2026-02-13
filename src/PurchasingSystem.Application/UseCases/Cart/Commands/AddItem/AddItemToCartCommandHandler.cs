using MediatR;
using PurchasingSystem.Application.Services.Abstractions;
using PurchasingSystem.Domain.Cart.Errors;
using PurchasingSystem.Domain.Cart.Interfaces;
using PurchasingSystem.Domain.Shared.Exceptions;

namespace PurchasingSystem.Application.UseCases.Cart.Commands.AddItem
{
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public AddItemToCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            // Buscar carrinho do usuário
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            
            if (cart is null)
                throw new DomainException(CartDomainErrors.Cart.NotFound);
            
            // Adicionar item ao carrinho
            cart.AddItem(request.ProductId, request.Quantity);
            
            await _cartRepository.UpdateAsync(cart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
