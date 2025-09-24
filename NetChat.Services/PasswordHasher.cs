using System;
using System.Security.Cryptography;
using System.Text;

namespace NetChat.Services.Security
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;           
        private const int KeySize = 32;            
        private const int Iterations = 100_000;    
        private const string Algorithm = "PBKDF2-SHA256";

        public static string HashPassword(string password)
        {
            if (password is null) throw new ArgumentNullException(nameof(password));

            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            var key = PBKDF2(password, salt, Iterations, KeySize);

            return $"{Algorithm}${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(key)}";
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            if (password is null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(storedHash)) return false;

            var parts = storedHash.Split('$', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4) return false;

            var algorithm = parts[0];
            if (!string.Equals(algorithm, Algorithm, StringComparison.Ordinal))
                return false;

            if (!int.TryParse(parts[1], out var iterations) || iterations <= 0)
                return false;

            byte[] salt, expectedKey;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expectedKey = Convert.FromBase64String(parts[3]);
            }
            catch
            {
                return false;
            }

            var computedKey = PBKDF2(password, salt, iterations, expectedKey.Length);

            return FixedTimeEquals(expectedKey, computedKey);
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int keySize)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256
            );
            return pbkdf2.GetBytes(keySize);
        }

        private static bool FixedTimeEquals(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}