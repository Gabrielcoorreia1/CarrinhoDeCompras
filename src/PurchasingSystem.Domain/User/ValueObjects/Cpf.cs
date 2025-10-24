using PurchasingSystem.Domain.Shared.Exceptions;
using PurchasingSystem.Domain.Shared.SeedWorks;
using PurchasingSystem.Domain.User.Errors;

namespace PurchasingSystem.Domain.User.ValueObjects
{
    public record Cpf : ValueObject
    {
        public string Value { get; }

        private Cpf(string value)
        {
            Value = value;
        }

        public static Cpf Create(string cpfValue)
        {
            var cleanedCpf = new string(cpfValue?.Where(char.IsDigit).ToArray());

            if (!IsValid(cleanedCpf))
            {
                throw new DomainException(DomainErrors.Cpf.InvalidCheckDigit);
            }

            return new Cpf(cleanedCpf);
        }

        private static bool IsValid(string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
                return false;

            if (cpf.All(c => c == cpf[0]))
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
