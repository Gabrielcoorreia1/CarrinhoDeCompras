using MediatR;
using PurchasingSystem.Application.Services.Abstractions;
using PurchasingSystem.Domain.Shared.Exceptions;
using PurchasingSystem.Domain.User.Errors;
using PurchasingSystem.Domain.User.Interfaces;

namespace PurchasingSystem.Application.UseCases.User.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;  
        private readonly IJwtTokenProvider _jwtTokenProvider;
        public LoginUserCommandHandler(IUserRepository userRepository, IJwtTokenProvider jwtTokenProvider)
        {
            _userRepository = userRepository;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByEmailAsync(command.Email);

            bool isPasswordValid = user is not null && user.Password.Verify(command.Password);

            if (!isPasswordValid)
            {
                throw new DomainException(DomainErrors.Account.InvalidLogin);
            }

            var token = _jwtTokenProvider.GenerateToken(user!);

            return new LoginUserCommandResponse(user.Id, token);
        }
    }
}
