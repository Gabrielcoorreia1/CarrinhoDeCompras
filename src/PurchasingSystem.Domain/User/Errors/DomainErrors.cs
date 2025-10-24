using PurchasingSystem.Domain.Shared.Errors;

namespace PurchasingSystem.Domain.User.Errors
{
    public static class DomainErrors
    {
        public static AccountErrors Account { get; } = new();
        public static EmailErrors Email { get; } = new();
        public static UsernameErrors Username { get; } = new();
        public static CpfErrors Cpf { get; } = new();
        public static PasswordErrors Password { get; } = new();

        public class AccountErrors
        {
            public readonly Error EmailInUse = new("409", ErrorMessages.Account.EmailInUse, ErrorType.Conflict);
            public readonly Error NotFound = new("404", ErrorMessages.Account.NotFound, ErrorType.NotFound);
            public readonly Error InvalidLogin = new("401", ErrorMessages.Account.InvalidLogin, ErrorType.Unauthorized);
            public readonly Error AccountDisabled = new("403", ErrorMessages.Account.AccountDisabled, ErrorType.Forbidden);
        }

        public class EmailErrors
        {
            public readonly Error Empty = new("400", ErrorMessages.Email.Empty, ErrorType.Validation);
            public readonly Error InvalidFormat = new("400", ErrorMessages.Email.InvalidFormat, ErrorType.Validation);
        }

        public class UsernameErrors
        {
            public readonly Error Empty = new("400", ErrorMessages.Username.Empty, ErrorType.Validation);
            public readonly Error InvalidFormat = new("400", ErrorMessages.Username.InvalidFormat, ErrorType.Validation);
        }

        public class CpfErrors
        {
            public readonly Error Empty = new("400", ErrorMessages.Cpf.Empty, ErrorType.Validation);
            public readonly Error InvalidFormat = new("400", ErrorMessages.Cpf.InvalidFormat, ErrorType.Validation);
            public readonly Error InvalidCheckDigit = new("400", ErrorMessages.Cpf.InvalidCheckDigit, ErrorType.Validation);
        }

        public class PasswordErrors
        {
            public readonly Error Empty = new("400", ErrorMessages.Password.Empty, ErrorType.Validation);
            public readonly Error InvalidFormat = new("400", ErrorMessages.Password.InvalidFormat, ErrorType.Validation);
            public readonly Error Weak = new("400", ErrorMessages.Password.Weak, ErrorType.Validation);
        }
    }
}
