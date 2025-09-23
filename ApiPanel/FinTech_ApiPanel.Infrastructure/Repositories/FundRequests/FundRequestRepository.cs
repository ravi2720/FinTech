using Dapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Domain.Entities.FundRequests;
using FinTech_ApiPanel.Domain.Interfaces.IFundRequests;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.FundRequests
{
    public class FundRequestRepository : IFundRequestRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ITokenService _tokenService;

        public FundRequestRepository(IDbConnection dbConnection, ITokenService tokenService)
        {
            _dbConnection = dbConnection;
            _tokenService = tokenService;
        }

        // CREATE
        public async Task<long> AddAsync(FundRequest model)
        {
            try
            {
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = _tokenService.GetLoggedInUserinfo().UserId;

                const string sql = @"
                INSERT INTO FundRequests 
                (UserId, BankId, Amount, AdminRemark, UserRemark, ReferenceId, Attachment, TransactionDate, ModeOfPayment, Status, CreatedBy, CreatedAt)
                VALUES 
                (@UserId, @BankId, @Amount, @AdminRemark, @UserRemark, @ReferenceId, @Attachment, @TransactionDate, @ModeOfPayment, @Status, @CreatedBy, @CreatedAt);
                SELECT CAST(SCOPE_IDENTITY() as BIGINT);
                ";

                return await _dbConnection.ExecuteScalarAsync<long>(sql, model);

            }
            catch (Exception ex)
            {
                throw new Exception("Error adding fund request: " + ex.Message);
            }
        }

        // GET BY ID
        public async Task<FundRequest?> GetByIdAsync(long id)
        {
            try
            {
                const string sql = "SELECT * FROM FundRequests WHERE Id = @Id";
                return await _dbConnection.QueryFirstOrDefaultAsync<FundRequest>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving fund request by ID: " + ex.Message);
            }
        }

        // GET ALL
        public async Task<IEnumerable<FundRequest>> GetAllAsync()
        {
            try
            {
                const string sql = "SELECT * FROM FundRequests ORDER BY CreatedAt DESC";
                return await _dbConnection.QueryAsync<FundRequest>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all fund requests: " + ex.Message);
            }
        }

        // UPDATE
        public async Task<bool> UpdateAsync(FundRequest model)
        {
            try
            {
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = _tokenService.GetLoggedInUserinfo()?.UserId;

                const string sql = @"
                UPDATE FundRequests SET 
                    UserId = @UserId,
                    BankId = @BankId,
                    Amount = @Amount,
                    AdminRemark = @AdminRemark,
                    UserRemark = @UserRemark,
                    ReferenceId = @ReferenceId,
                    Attachment = @Attachment,
                    TransactionDate = @TransactionDate,
                    ModeOfPayment = @ModeOfPayment,
                    Status = @Status,
                    UpdatedBy = @UpdatedBy,
                    UpdatedAt = @UpdatedAt
                WHERE Id = @Id;
                ";

                var rows = await _dbConnection.ExecuteAsync(sql, model);
                return rows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating fund request: " + ex.Message);
            }
        }
    }
}
