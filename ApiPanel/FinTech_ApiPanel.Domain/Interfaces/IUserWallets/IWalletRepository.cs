using FinTech_ApiPanel.Domain.Entities.UserWallets;

namespace FinTech_ApiPanel.Domain.Interfaces.IUserWallets
{
    public interface IWalletRepository
    {
        Task<long> AddAsync(Wallet wallet);
        Task<Wallet?> GetByIdAsync(long id);
        Task<Wallet> GetByUserIdAsync(long userId);
        Task<IEnumerable<Wallet>> GetAllAsync();
        Task<bool> UpdateAsync(Wallet wallet);
    }
}
