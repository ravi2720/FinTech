using FinTech_ApiPanel.Domain.Entities.Whitelists;

namespace FinTech_ApiPanel.Domain.Interfaces.IWhitelists
{
    public interface IWhitelistRepository
    {
        Task<IEnumerable<WhitelistEntry>> GetAllAsync();
        Task<IEnumerable<WhitelistEntry>> GetByUserIdAsync(long userId);
        Task<WhitelistEntry?> GetByIdAsync(long id);
        Task<long> AddAsync(WhitelistEntry entry);
        Task<bool> UpdateAsync(WhitelistEntry entry);
        Task<bool> DeleteAsync(long id);
    }
}