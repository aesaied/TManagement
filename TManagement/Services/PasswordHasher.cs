using System.Security.Cryptography;
using System.Text;

namespace TManagement.Services
{
    public class PasswordHasher
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        public string HashPassword(string password, out string salt)
        {
            var saltVal = RandomNumberGenerator.GetBytes(keySize);
            salt = Convert.ToHexString(saltVal);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltVal,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }


        public bool VerifyPassword(string password, string hash, string salt)
        {
            var saltVal = Convert.FromHexString(salt);
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, saltVal, iterations, hashAlgorithm, keySize);
            // var x = Convert.ToHexString(hashToCompare);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
