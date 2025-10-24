using PurchasingSystem.Domain.Shared.Exceptions;
using PurchasingSystem.Domain.Shared.SeedWorks;
using PurchasingSystem.Domain.User.Errors;

namespace PurchasingSystem.Domain.User.ValueObjects
{
    public record Email : ValueObject
    {
        public string Value { get; }
        private Email() { }
        private Email(string value)
        {
            Value = value;
        }
        public static Email Create(string value)
        {
            Validate(value);
            return new Email(value);
        }
        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException(DomainErrors.Email.Empty);
            }
            if (!value.Contains("@"))
            {
                throw new DomainException(DomainErrors.Email.InvalidFormat);
            }
        }
    }
}
