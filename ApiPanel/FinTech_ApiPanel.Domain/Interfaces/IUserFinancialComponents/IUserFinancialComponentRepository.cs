using FinTech_ApiPanel.Domain.Entities.UserFinancialComponents;

namespace FinTech_ApiPanel.Domain.Interfaces.IUserFinancialComponents
{
    public interface IUserFinancialComponentRepository
    {
        Task<long> AddAsync(UserFinancialComponent entity);
        Task<UserFinancialComponent> GetByIdAsync(long id);
        Task<IEnumerable<UserFinancialComponent>> GetByUserIdAsync(long userId);
        Task<IEnumerable<UserFinancialComponent>> GetAllAsync();
        Task<bool> UpdateAsync(UserFinancialComponent entity);
        Task<bool> DeleteAsync(long id);
    }
}
