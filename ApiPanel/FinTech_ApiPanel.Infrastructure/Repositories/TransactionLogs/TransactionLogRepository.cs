using Dapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.TransactionLogs
{
    public class TransactionLogRepository : ITransactionLogRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ITokenService _tokenService;

        public TransactionLogRepository(IDbConnection dbConnection, ITokenService tokenService)
        {
            _dbConnection = dbConnection;
            _tokenService = tokenService;
        }

        public async Task<long> AddAsync(TransactionLog transaction)
        {
            transaction.CreatedAt = DateTime.Now;
            if (_tokenService.GetLoggedInUserinfo() != null)
                transaction.CreatedBy = _tokenService.GetLoggedInUserinfo().UserId;

            string sql = @"
            INSERT INTO TransactionLogs 
            (
                UserId, RefUserId, ReferenceId, Amount, RemainingAmount, AuditType, LogType, Status, MobileNumber, Email, Remark, CaptureType, EndPointIP, WalletUpdated,
                Ipay_OrderId, Ipay_StatusCode, Ipay_ActCode, Ipay_Uuid, Ipay_Timestamp, 
                Ipay_Environment, Ipay_OutletId, Ipay_Latitude, Ipay_Longitude, Ipay_Response,
                CreatedBy, CreatedAt
            )
            VALUES 
            (
                @UserId, @RefUserId, @ReferenceId, @Amount, @RemainingAmount, @AuditType, @LogType, @Status, @MobileNumber, @Email, @Remark, @CaptureType, @EndPointIP, @WalletUpdated,
                @Ipay_OrderId, @Ipay_StatusCode, @Ipay_ActCode, @Ipay_Uuid, @Ipay_Timestamp,
                @Ipay_Environment, @Ipay_OutletId, @Ipay_Latitude, @Ipay_Longitude, @Ipay_Response,
                @CreatedBy, @CreatedAt
            );
            SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

            return await _dbConnection.ExecuteScalarAsync<long>(sql, transaction);
        }

        public async Task<TransactionLog?> GetByIdAsync(long id)
        {
            string sql = "SELECT * FROM TransactionLogs WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<TransactionLog>(sql, new { Id = id });
        }

        public async Task<IEnumerable<TransactionLog>?> GetByReferenceIdAsync(string id)
        {
            string sql = "SELECT * FROM TransactionLogs WHERE ReferenceId = @Id";
            return await _dbConnection.QueryAsync<TransactionLog>(sql, new { Id = id });
        }

        public async Task<IEnumerable<TransactionLog>> GetByUserIdAsync(long userId)
        {
            string sql = "SELECT * FROM TransactionLogs WHERE UserId = @UserId";
            return await _dbConnection.QueryAsync<TransactionLog>(sql, new { UserId = userId });
        }

        public async Task<IEnumerable<TransactionLog>> GetAllAsync()
        {
            string sql = "SELECT * FROM TransactionLogs";
            return await _dbConnection.QueryAsync<TransactionLog>(sql);
        }

        public async Task<IEnumerable<TransactionLog>> GetByLogTypeAsync(byte logType)
        {
            string sql = "SELECT * FROM TransactionLogs WHERE LogType = @LogType";
            return await _dbConnection.QueryAsync<TransactionLog>(sql, new { LogType = logType });
        }
        public async Task<int> UpdateStatusAsync(TransactionLog transaction)
        {
            transaction.UpdatedAt = DateTime.Now;
            if (_tokenService.GetLoggedInUserinfo() != null)
                transaction.UpdatedBy = _tokenService.GetLoggedInUserinfo().UserId;

            string sql = @"
            UPDATE TransactionLogs
            SET 
                Status = @Status,
                Remark = ISNULL(@Remark, Remark),
                UpdatedBy = @UpdatedBy,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

            return await _dbConnection.ExecuteAsync(sql, new
            {
                Id = transaction.Id,
                Status = transaction.Status,
                Remark = transaction.Remark,
                UpdatedBy = transaction.UpdatedBy,
                UpdatedAt = transaction.UpdatedAt ?? DateTime.UtcNow
            });
        }

    }
}
