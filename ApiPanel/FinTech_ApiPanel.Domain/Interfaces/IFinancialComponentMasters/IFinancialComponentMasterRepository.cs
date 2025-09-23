using FinTech_ApiPanel.Domain.Entities.FinancialComponentMasters;

namespace FinTech_ApiPanel.Domain.Interfaces.IFinancialComponentMasters
{
    public interface IFinancialComponentMasterRepository
    {
        Task<long> AddAsync(FinancialComponentMaster entity);
        Task<FinancialComponentMaster> GetByIdAsync(long id);
        Task<IEnumerable<FinancialComponentMaster>> GetAllAsync();
        Task<bool> UpdateAsync(FinancialComponentMaster entity);
        Task<bool> DeleteAsync(long id);
    }
}
