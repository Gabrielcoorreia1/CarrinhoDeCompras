using MediatR;
using PurchasingSystem.Application.Services.Abstractions;

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
        public Task Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
