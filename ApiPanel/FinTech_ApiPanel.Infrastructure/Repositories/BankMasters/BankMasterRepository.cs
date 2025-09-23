using Dapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Domain.Entities.BankMasters;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.BankMasters
{
    public class BankMasterRepository : IBankMasterRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ITokenService _tokenService;

        public BankMasterRepository(IDbConnection dbConnection, ITokenService tokenService)
        {
            _dbConnection = dbConnection;
            _tokenService = tokenService;
        }

        // CREATE
        public async Task<long> AddAsync(BankMaster model)
        {
            model.CreatedAt = DateTime.Now;
            model.CreatedBy = _tokenService.GetLoggedInUserinfo().UserId;

            const string sql = @"
            INSERT INTO BankMasters 
            (BankName, Type, BranchName, IFSCCode, AccountHolderName, AccountNumber, UPIHandle, IsActive, CreatedBy, CreatedAt)
            VALUES 
            (@BankName, @Type, @BranchName, @IFSCCode, @AccountHolderName, @AccountNumber, @UPIHandle, @IsActive, @CreatedBy, @CreatedAt);
            SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

            return await _dbConnection.ExecuteScalarAsync<long>(sql, model);
        }

        // READ: Get by ID
        public async Task<BankMaster?> GetByIdAsync(long id)
        {
            const string sql = "SELECT * FROM BankMasters WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<BankMaster>(sql, new { Id = id });
        }
        
        public async Task<BankMaster?> GetByAccountNumberAsync(string accountNumber)
        {
            const string sql = "SELECT * FROM BankMasters WHERE AccountNumber = @accountNumber";
            return await _dbConnection.QueryFirstOrDefaultAsync<BankMaster>(sql, new { AccountNumber = accountNumber });
        }

        // READ: Get All
        public async Task<IEnumerable<BankMaster>> GetAllAsync()
        {
            const string sql = "SELECT * FROM BankMasters ORDER BY BankName";
            return await _dbConnection.QueryAsync<BankMaster>(sql);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(BankMaster model)
        {
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = _tokenService.GetLoggedInUserinfo()?.UserId;

            const string sql = @"
            UPDATE BankMasters SET
                BankName = @BankName,
                Type = @Type,
                BranchName = @BranchName,
                IFSCCode = @IFSCCode,
                AccountHolderName = @AccountHolderName,
                AccountNumber = @AccountNumber,
                UPIHandle = @UPIHandle,
                IsActive = @IsActive,
                UpdatedBy = @UpdatedBy,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id;";

            int rows = await _dbConnection.ExecuteAsync(sql, model);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            const string sql = "DELETE FROM BankMasters WHERE Id = @Id";
            int rows = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}