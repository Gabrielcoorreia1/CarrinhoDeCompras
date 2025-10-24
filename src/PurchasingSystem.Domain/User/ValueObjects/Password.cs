using PurchasingSystem.Domain.Shared.SeedWorks;
using System.Security.Cryptography;

namespace PurchasingSystem.Domain.User.ValueObjects
{
    public record Password : ValueObject
    {
        public string Hash { get; }
        public string Salt { get; }

        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;

        private Password(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public static Password Create(string plainTextPassword)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
            var saltString = Convert.ToBase64String(saltBytes);

            var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
                plainTextPassword,
                saltBytes,
                Iterations,
                _hashAlgorithm,
                KeySize);
            var hashString = Convert.ToBase64String(hashBytes);

            return new Password(hashString, saltString);
        }

        public bool Verify(string plainTextPassword)
        {
            if (string.IsNullOrEmpty(plainTextPassword)) return false;

            var saltBytes = Convert.FromBase64String(Salt);

            // Calcula o hash da senha fornecida usando o MESMO salt.
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                plainTextPassword,
                saltBytes,
                Iterations,
                _hashAlgorithm,
                KeySize);

            var hashToCompareString = Convert.ToBase64String(hashToCompare);

            return CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(Hash),
                hashToCompare
            );
        }
    }
}
