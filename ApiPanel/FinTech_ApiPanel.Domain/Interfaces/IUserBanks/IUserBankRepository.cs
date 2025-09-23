using FinTech_ApiPanel.Domain.Entities.UserMasters;

namespace FinTech_ApiPanel.Domain.Interfaces.IUserBanks
{
    public interface IUserBankRepository
    {
        Task<long> AddAsync(UserBank model);
        Task<UserBank?> GetByIdAsync(long id);
        Task<IEnumerable<UserBank>> GetByUserIdAsync(long userId);
        Task<IEnumerable<UserBank>> GetAllAsync();
        Task<bool> UpdateAsync(UserBank model);
        Task<UserBank?> GetByAccountNumberAsync(string accountNumber);
        Task<bool> DeleteAsync(long id);
    }
}
