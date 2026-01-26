using MediatR;
using PurchasingSystem.Application.Services.Abstractions;
using PurchasingSystem.Domain.Cart.Errors;
using PurchasingSystem.Domain.Cart.Interfaces;
using PurchasingSystem.Domain.Shared.Exceptions;

namespace PurchasingSystem.Application.UseCases.Cart.Commands.RemoveItem
{
    public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public RemoveItemFromCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            
            if (cart is null)
                throw new DomainException(CartDomainErrors.Cart.NotFound);
            
            cart.RemoveItem(request.ProductId);
            
            await _cartRepository.UpdateAsync(cart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
