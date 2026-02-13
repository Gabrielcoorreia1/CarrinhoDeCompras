using MediatR;
using PurchasingSystem.Application.Services.Abstractions;
using PurchasingSystem.Domain.Cart.Errors;
using PurchasingSystem.Domain.Cart.Interfaces;
using PurchasingSystem.Domain.Shared.Exceptions;

namespace PurchasingSystem.Application.UseCases.Cart.Commands.Create
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand>
    {
        private readonly ICartRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateCartCommandHandler(ICartRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            // Verificar se usuário já tem carrinho
            var existingCart = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);
            
            if (existingCart is not null)
                throw new DomainException(CartDomainErrors.Cart.AlreadyExists);
            
            // Criar novo carrinho
            var cart = Domain.Cart.Entities.Cart.Create(request.UserId);
            
            await _repository.AddAsync(cart, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
