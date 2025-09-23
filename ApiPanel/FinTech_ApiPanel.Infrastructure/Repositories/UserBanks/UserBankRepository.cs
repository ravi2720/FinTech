using Dapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserBanks;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.UserBanks
{
    public class UserBankRepository : IUserBankRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ITokenService _tokenService;

        public UserBankRepository(IDbConnection dbConnection, ITokenService tokenService)
        {
            _dbConnection = dbConnection;
            _tokenService = tokenService;
        }

        // CREATE
        public async Task<long> AddAsync(UserBank model)
        {
            model.CreatedAt = DateTime.Now;
            model.CreatedBy = _tokenService.GetLoggedInUserinfo().UserId;

            const string sql = @"
                INSERT INTO UserBanks
                (UserId, BankName, BranchName, IFSCCode, AccountHolderName, AccountNumber, UPIHandle,
                 IsActive, CreatedAt, CreatedBy)
                VALUES
                (@UserId, @BankName, @BranchName, @IFSCCode, @AccountHolderName, @AccountNumber, @UPIHandle,
                 @IsActive, @CreatedAt, @CreatedBy);
                SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

            return await _dbConnection.ExecuteScalarAsync<long>(sql, model);
        }

        // READ by Id
        public async Task<UserBank?> GetByIdAsync(long id)
        {
            const string sql = "SELECT * FROM UserBanks WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<UserBank>(sql, new { Id = id });
        }

        // READ by UserId
        public async Task<IEnumerable<UserBank>> GetByUserIdAsync(long userId)
        {
            const string sql = "SELECT * FROM UserBanks WHERE UserId = @UserId";
            return await _dbConnection.QueryAsync<UserBank>(sql, new { UserId = userId });
        }

        public async Task<UserBank?> GetByAccountNumberAsync(string accountNumber)
        {
            const string sql = "SELECT * FROM UserBanks WHERE AccountNumber = @accountNumber";
            return await _dbConnection.QueryFirstOrDefaultAsync<UserBank>(sql, new { AccountNumber = accountNumber });
        }

        // READ All
        public async Task<IEnumerable<UserBank>> GetAllAsync()
        {
            const string sql = "SELECT * FROM UserBanks ORDER BY Id DESC";
            return await _dbConnection.QueryAsync<UserBank>(sql);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(UserBank model)
        {
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = _tokenService.GetLoggedInUserinfo()?.UserId;

            const string sql = @"
                UPDATE UserBanks SET
                    BankName = @BankName,
                    BranchName = @BranchName,
                    IFSCCode = @IFSCCode,
                    AccountHolderName = @AccountHolderName,
                    AccountNumber = @AccountNumber,
                    UPIHandle = @UPIHandle,
                    IsActive = @IsActive,
                    UpdatedAt = @UpdatedAt,
                    UpdatedBy = @UpdatedBy
                WHERE Id = @Id;";

            int rows = await _dbConnection.ExecuteAsync(sql, model);
            return rows > 0;
        }

        // DELETE
        public async Task<bool> DeleteAsync(long id)
        {
            const string sql = "DELETE FROM UserBanks WHERE Id = @Id";
            int rows = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
