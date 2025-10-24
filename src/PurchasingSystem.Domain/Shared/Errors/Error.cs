namespace PurchasingSystem.Domain.Shared.Errors
{
    public record Error(
        string Code,
        string Message,
        ErrorType Type);
}
