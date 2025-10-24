using MediatR;

namespace PurchasingSystem.Application.UseCases.User.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid UserId) : IRequest<GetUserByIdQueryResponse>;
}
