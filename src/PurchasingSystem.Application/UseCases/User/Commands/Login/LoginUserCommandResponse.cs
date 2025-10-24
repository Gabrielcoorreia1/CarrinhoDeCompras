namespace PurchasingSystem.Application.UseCases.User.Commands.Login
{
    public record LoginUserCommandResponse(
        Guid Id,
        string Jwt);
}
