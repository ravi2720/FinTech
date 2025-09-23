using FinTech_ApiPanel.Domain.Entities.TransactionLogs;

namespace FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs
{
    public interface ITransactionLogRepository
    {
        Task<long> AddAsync(TransactionLog transaction);
        Task<TransactionLog?> GetByIdAsync(long id);
        Task<IEnumerable<TransactionLog>?> GetByReferenceIdAsync(string id);
        Task<IEnumerable<TransactionLog>> GetByUserIdAsync(long userId);
        Task<IEnumerable<TransactionLog>> GetAllAsync();
        Task<IEnumerable<TransactionLog>> GetByLogTypeAsync(byte logType);

        Task<int> UpdateStatusAsync(TransactionLog transaction);
    }
}
