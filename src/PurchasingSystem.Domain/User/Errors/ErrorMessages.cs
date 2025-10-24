namespace PurchasingSystem.Domain.User.Errors
{
    public static class ErrorMessages
    {
        public static AccountErrorMessages Account { get; set; } = new();
        public static EmailErrorMessages Email { get; set; } = new();
        public static UsernameErrorMessages Username { get; set; } = new();
        public static CpfErrorMessages Cpf { get; set; } = new();
        public static PasswordErrorMessages Password { get; set; } = new();

        public class AccountErrorMessages
        {
            public string EmailInUse { get; set; } = "O e-mail já está em uso.";
            public string NotFound { get; set; } = "Conta não encontrada.";
            public string InvalidLogin { get; set; } = "E-mail ou senha inválidos.";
            public string AccountDisabled { get; set; } = "Conta desativada. Entre em contato com o suporte.";
        }
        public class EmailErrorMessages
        {
            public string Empty { get; set; } = "O e-mail não pode ser vazio.";
            public string InvalidFormat { get; set; } = "O e-mail deve conter um formato válido.";
        }
        public class UsernameErrorMessages
        {
            public string Empty { get; set; } = "O nome de usuário não pode ser vazio.";
            public string InvalidFormat { get; set; } = "O nome de usuário deve conter apenas letras, números e sublinhados.";
        }
        public class CpfErrorMessages
        {
            public string Empty { get; set; } = "O CPF não pode ser vazio.";
            public string InvalidFormat { get; set; } = "O CPF deve conter apenas números e ter 11 dígitos.";
            public string InvalidCheckDigit { get; set; } = "O CPF possui dígitos verificadores inválidos.";
        }
        public class PasswordErrorMessages
        {
            public string Empty { get; set; } = "A senha não pode ser vazia.";
            public string InvalidFormat { get; set; } = "A senha deve conter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e símbolos.";
            public string Weak { get; set; } = "A senha é muito fraca. Por favor, escolha uma senha mais forte.";
        }

    }
}
