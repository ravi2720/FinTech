using FinTech_ApiPanel.Domain.Entities.FundRequests;

namespace FinTech_ApiPanel.Domain.Interfaces.IFundRequests
{
    public interface IFundRequestRepository
    {
        Task<long> AddAsync(FundRequest model);
        Task<FundRequest?> GetByIdAsync(long id);
        Task<IEnumerable<FundRequest>> GetAllAsync();
        Task<bool> UpdateAsync(FundRequest model);
    }
}
