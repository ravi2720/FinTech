using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using System.Security.Cryptography;
using System.Text;

namespace FinTech_ApiPanel.Infrastructure.Utilities.Cryptography
{
    public class CryptoService : ICryptoService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100_000;
        private const string AlphanumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";


        public PasswordHashDto GenerateSaltedHash(string password)
        {
            byte[] saltBytes = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(HashSize);

                return new PasswordHashDto
                {
                    Hash = Convert.ToBase64String(hashBytes),
                    Salt = Convert.ToBase64String(saltBytes)
                };
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] enteredHashBytes = pbkdf2.GetBytes(HashSize);
                byte[] storedHashBytes = Convert.FromBase64String(storedHash);

                // Compare byte arrays in constant time to prevent timing attacks
                return AreHashesEqual(enteredHashBytes, storedHashBytes);
            }
        }

        bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            if (firstHash.Length != secondHash.Length)
                return false;

            var result = 0;
            for (int i = 0; i < firstHash.Length; i++)
            {
                result |= firstHash[i] ^ secondHash[i];
            }

            return result == 0;
        }

        public string GenerateClientId()
        {
            return GenerateRandomString(16);
        }

        public string GenerateClientSecret()
        {
            return GenerateRandomString(32);
        }

        string GenerateRandomString(int length)
        {
            var result = new StringBuilder(length);
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] buffer = new byte[sizeof(uint)];

                while (result.Length < length)
                {
                    rng.GetBytes(buffer);
                    uint num = BitConverter.ToUInt32(buffer, 0);
                    result.Append(AlphanumericChars[(int)(num % (uint)AlphanumericChars.Length)]);
                }
            }
            return result.ToString();
        }

        public string EncryptAadhaar(string aadhaarNumber, string encryptionKey)
        {
            if (string.IsNullOrEmpty(aadhaarNumber))
                throw new ArgumentNullException(nameof(aadhaarNumber), "Aadhaar number cannot be null or empty.");

            if (string.IsNullOrEmpty(encryptionKey) || encryptionKey.Length != 32)
                throw new ArgumentException("Encryption key must be 32 characters (256 bits) long.");

            // Convert key and plaintext to bytes
            byte[] keyBytes = Encoding.ASCII.GetBytes(encryptionKey);
            byte[] clearBytes = Encoding.UTF8.GetBytes(aadhaarNumber);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Key = keyBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // Generate a new random IV for each encryption
                aes.GenerateIV();
                byte[] ivBytes = aes.IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, ivBytes))
                {
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(clearBytes, 0, clearBytes.Length);

                    // Prepend IV to encrypted data for use in decryption
                    byte[] encryptedData = new byte[ivBytes.Length + encryptedBytes.Length];
                    Buffer.BlockCopy(ivBytes, 0, encryptedData, 0, ivBytes.Length);
                    Buffer.BlockCopy(encryptedBytes, 0, encryptedData, ivBytes.Length, encryptedBytes.Length);

                    // Return Base64 encoded string with IV + ciphertext
                    return Convert.ToBase64String(encryptedData);
                }
            }
        }

        public string DecryptAadhaar(string encryptedBase64, string encryptionKey)
        {
            if (string.IsNullOrEmpty(encryptedBase64))
                throw new ArgumentNullException(nameof(encryptedBase64));

            if (string.IsNullOrEmpty(encryptionKey) || encryptionKey.Length != 32)
                throw new ArgumentException("Encryption key must be 32 characters (256 bits) long.");

            byte[] encryptedData = Convert.FromBase64String(encryptedBase64);
            byte[] keyBytes = Encoding.ASCII.GetBytes(encryptionKey);

            byte[] iv = new byte[16];
            Buffer.BlockCopy(encryptedData, 0, iv, 0, iv.Length);

            int encryptedBytesLength = encryptedData.Length - iv.Length;
            byte[] encryptedBytes = new byte[encryptedBytesLength];
            Buffer.BlockCopy(encryptedData, iv.Length, encryptedBytes, 0, encryptedBytesLength);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Key = keyBytes;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var decryptor = aes.CreateDecryptor())
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                    // Decode with UTF8 to match encryption encoding
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }

        public string Generate32CharKey()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Span<byte> randomBytes = stackalloc byte[32];
            RandomNumberGenerator.Fill(randomBytes);

            char[] result = new char[32];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = chars[randomBytes[i] % chars.Length];
            }

            return new string(result);
        }
    }
}
