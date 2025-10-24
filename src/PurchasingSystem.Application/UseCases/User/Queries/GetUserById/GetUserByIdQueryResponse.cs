namespace PurchasingSystem.Application.UseCases.User.Queries.GetUserById
{
    public record GetUserByIdQueryResponse(Guid Id, string Username, string Email);
}
