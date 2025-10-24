using MediatR;
using PurchasingSystem.Application.Services.Abstractions;
using PurchasingSystem.Domain.Shared.Exceptions;
using PurchasingSystem.Domain.User.Errors;
using PurchasingSystem.Domain.User.Interfaces;

namespace PurchasingSystem.Application.UseCases.User.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserID);
            if (user is null)
                throw new DomainException(DomainErrors.Account.NotFound);

            await _userRepository.Delete(user);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
