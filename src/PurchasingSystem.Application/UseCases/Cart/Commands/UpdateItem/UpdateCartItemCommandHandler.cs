using MediatR;
using PurchasingSystem.Application.Services.Abstractions;
using PurchasingSystem.Domain.Cart.Errors;
using PurchasingSystem.Domain.Cart.Interfaces;
using PurchasingSystem.Domain.Shared.Exceptions;

namespace PurchasingSystem.Application.UseCases.Cart.Commands.UpdateItem
{
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateCartItemCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            
            if (cart is null)
                throw new DomainException(CartDomainErrors.Cart.NotFound);
            
            cart.UpdateItemQuantity(request.ProductId, request.Quantity);
            
            await _cartRepository.UpdateAsync(cart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
