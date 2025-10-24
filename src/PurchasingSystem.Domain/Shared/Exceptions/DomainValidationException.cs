using PurchasingSystem.Domain.Shared.Errors;

namespace PurchasingSystem.Domain.Shared.Exceptions
{
    public class DomainValidationException : Exception
    {
        public IReadOnlyCollection<Error> Errors { get; }
        public DomainValidationException(IReadOnlyCollection<Error> errors) : base("A validação do domínio falhou.")
        {
            Errors = errors;
        }
    }
}
