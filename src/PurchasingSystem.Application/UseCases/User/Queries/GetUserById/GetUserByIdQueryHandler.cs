using MediatR;
using PurchasingSystem.Domain.Shared.Exceptions;
using PurchasingSystem.Domain.User.Errors;
using PurchasingSystem.Domain.User.Interfaces;

namespace PurchasingSystem.Application.UseCases.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
    {
        private readonly IUserRepository _repository;
        public GetUserByIdQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.UserId, cancellationToken);

            if(user is null) 
                throw new DomainException(DomainErrors.Account.NotFound);

            return new GetUserByIdQueryResponse(user.Id, user.FullName.ToString(), user.Email.Value);
        }
    }
}
