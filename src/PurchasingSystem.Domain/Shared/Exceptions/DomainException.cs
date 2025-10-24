using PurchasingSystem.Domain.Shared.Errors;

namespace PurchasingSystem.Domain.Shared.Exceptions
{
    public class DomainException : Exception
    {
        public Error Error { get; }
        public DomainException(Error error) : base(error.Message)
        {
            Error = error ?? throw new ArgumentNullException(nameof(error), "O erro não pode ser nulo.");
        }
    }
}
