using MediatR;
using PurchasingSystem.Application.Services.Abstractions;
using PurchasingSystem.Domain.Shared.Exceptions;
using PurchasingSystem.Domain.User.Errors;
using PurchasingSystem.Domain.User.Interfaces;

namespace PurchasingSystem.Application.UseCases.User.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {

            if (await _userRepository.IsEmailInUseAsync(command.Email))
            {
                throw new DomainException(DomainErrors.Account.EmailInUse);
            }

            var user = Domain.User.Entities.User.Create(
                command.FirstName,
                command.LastName,
                command.Email,
                command.Password,
                command.Cpf
            );

            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync(cancellationToken);

            return user.Id;
        }
    }
}
