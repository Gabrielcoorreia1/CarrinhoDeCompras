using PurchasingSystem.Domain.Shared.Exceptions;
using PurchasingSystem.Domain.Shared.SeedWorks;
using PurchasingSystem.Domain.User.Errors;

namespace PurchasingSystem.Domain.User.ValueObjects
{
    public record FullName : ValueObject
    {
        public const int MaxLength = 100;
        public const int MinLength = 3;
        private FullName() { }
        private FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static FullName Create(string firstName, string lastName)
        {
            Validate(firstName, lastName);
            return new FullName(firstName, lastName);
        }

        private static void Validate(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < MinLength || firstName.Length > MaxLength)
                throw new DomainException(DomainErrors.Username.Empty);
            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < MinLength || lastName.Length > MaxLength)
                throw new DomainException(DomainErrors.Username.Empty);
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
