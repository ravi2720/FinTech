using FinTech_ApiPanel.Domain.Entities.BankMasters;

namespace FinTech_ApiPanel.Domain.Interfaces.IBankMasters
{
    public interface IBankMasterRepository
    {
        Task<long> AddAsync(BankMaster model);
        Task<BankMaster?> GetByIdAsync(long id);
        Task<IEnumerable<BankMaster>> GetAllAsync();
        Task<bool> UpdateAsync(BankMaster model);
        Task<BankMaster?> GetByAccountNumberAsync(string accountNumber);
        Task<bool> DeleteAsync(long id);
    }
}
