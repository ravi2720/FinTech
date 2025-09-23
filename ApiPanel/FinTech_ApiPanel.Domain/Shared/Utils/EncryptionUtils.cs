using System.Security.Cryptography;
using System.Text;

namespace FinTech_ApiPanel.Domain.Shared.Utils
{
    public class EncryptionUtils
    {
        // Decode the base64-encoded key and IV strings
        private static readonly byte[] key = Convert.FromBase64String("mJ3Lbg+6Qk0fV5gLSv+dNxG3nv+X1Nh6e7TpFpq98Xg=");
        private static readonly byte[] iv = Convert.FromBase64String("yM5v4BNa1MlFv6RPoXjDBQ==");
        private static Random _random = new Random();

        public static long GenerateOtp()
        {
            byte[] randomNumber = new byte[4]; // 4 bytes to generate a sufficiently large number

            RandomNumberGenerator.Fill(randomNumber); // Use the static RandomNumberGenerator class

            // Convert the random number to a 32-bit integer and ensure it's positive
            int otp = Math.Abs(BitConverter.ToInt32(randomNumber, 0));

            // Limit the OTP to be within the range 100000 to 999999
            otp = 100000 + (otp % 900000);

            return otp;
        }

        public static string EncryptString(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string GeneratePassword()
        {
            const string lowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digitChars = "0123456789";
            const string specialChars = "@#-$&";

            // Build a base password that satisfies the basic requirements
            var passwordBuilder = new StringBuilder();

            // Add exactly one digit
            passwordBuilder.Append(digitChars[_random.Next(digitChars.Length)]);

            // Add exactly one lowercase letter
            passwordBuilder.Append(lowerCaseChars[_random.Next(lowerCaseChars.Length)]);

            // Add exactly one uppercase letter
            passwordBuilder.Append(upperCaseChars[_random.Next(upperCaseChars.Length)]);

            // Add exactly one special character
            passwordBuilder.Append(specialChars[_random.Next(specialChars.Length)]);

            // Fill the remaining characters with random letters and digits
            string allChars = lowerCaseChars + upperCaseChars + digitChars;
            int remainingLength = 8 - passwordBuilder.Length; // Ensure total length is 8

            for (int i = 0; i < remainingLength; i++)
            {
                passwordBuilder.Append(allChars[_random.Next(allChars.Length)]);
            }

            // Shuffle the characters to avoid predictable patterns
            return new string(passwordBuilder.ToString().OrderBy(c => _random.Next()).ToArray());
        }
    }
}
