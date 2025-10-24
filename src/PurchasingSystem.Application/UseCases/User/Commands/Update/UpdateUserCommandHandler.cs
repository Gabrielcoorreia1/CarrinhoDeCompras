using MediatR;
using PurchasingSystem.Application.Services.Abstractions;
using PurchasingSystem.Domain.Shared.Exceptions;
using PurchasingSystem.Domain.User.Errors;
using PurchasingSystem.Domain.User.Interfaces;

namespace PurchasingSystem.Application.UseCases.User.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(command.Id);
            if (user is null)
            {
                throw new DomainException(DomainErrors.Account.NotFound);
            }

            if (command.Email != user.Email.Value && await _userRepository.IsEmailInUseAsync(command.Email))
            {
                throw new DomainException(DomainErrors.Account.EmailInUse);
            }

            user.Update(command.FirstName, command.LastName, command.Email, command.password);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
