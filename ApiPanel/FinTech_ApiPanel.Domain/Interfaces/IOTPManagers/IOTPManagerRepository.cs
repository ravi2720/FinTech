using FinTech_ApiPanel.Domain.Entities.UserMasters;

namespace FinTech_ApiPanel.Domain.Interfaces.IOTPManagers
{
    public interface IOTPManagerRepository
    {
        Task<long> AddAsync(OTPManager otp);
        Task<OTPManager?> GetByIdAsync(long id);
        Task<IEnumerable<OTPManager>> GetAllAsync();
        Task<bool> UpdateAsync(OTPManager otp);
        Task<bool> DeleteAsync(long id);
    }
}
