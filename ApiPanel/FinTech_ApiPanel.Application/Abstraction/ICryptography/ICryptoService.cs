using FinTech_ApiPanel.Domain.DTOs.UserMasters;

namespace FinTech_ApiPanel.Application.Abstraction.ICryptography
{
    public interface ICryptoService
    {
        PasswordHashDto GenerateSaltedHash(string password);
        bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt);
        string GenerateClientSecret();
        string GenerateClientId();
        string EncryptAadhaar(string aadhaarNumber, string encryptionKey);
        string DecryptAadhaar(string encryptedBase64, string encryptionKey);
        string Generate32CharKey();
    }
}
