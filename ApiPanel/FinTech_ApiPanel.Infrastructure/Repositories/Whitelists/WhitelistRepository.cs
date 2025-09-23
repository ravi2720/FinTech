using Dapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Domain.Entities.Whitelists;
using FinTech_ApiPanel.Domain.Interfaces.IWhitelists;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.Whitelists
{
    public class WhitelistRepository : IWhitelistRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ITokenService _tokenService;

        public WhitelistRepository(IDbConnection dbConnection,
            ITokenService tokenService)
        {
            _dbConnection = dbConnection;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<WhitelistEntry>> GetAllAsync()
        {
            const string sql = @"SELECT * FROM WhitelistEntries";
            return await _dbConnection.QueryAsync<WhitelistEntry>(sql);
        }

        public async Task<IEnumerable<WhitelistEntry>> GetByUserIdAsync(long userId)
        {
            const string sql = @"SELECT * FROM WhitelistEntries WHERE CreatedBy = @userId";
            return await _dbConnection.QueryAsync<WhitelistEntry>(sql, new { userId });
        }

        public async Task<WhitelistEntry?> GetByIdAsync(long id)
        {
            const string sql = @"SELECT * FROM WhitelistEntries WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<WhitelistEntry>(sql, new { Id = id });
        }

        public async Task<long> AddAsync(WhitelistEntry entry)
        {
            entry.CreatedAt = DateTime.Now;
            entry.CreatedBy = _tokenService.GetLoggedInUserinfo().UserId;

            const string sql = @"
            INSERT INTO WhitelistEntries
            (EntryType, Value, Description, IsActive, CreatedBy, CreatedAt)
            VALUES
            (@EntryType, @Value, @Description, @IsActive, @CreatedBy, @CreatedAt);
            SELECT CAST(SCOPE_IDENTITY() as BIGINT);";

            return await _dbConnection.ExecuteScalarAsync<long>(sql, entry);
        }

        public async Task<bool> UpdateAsync(WhitelistEntry entry)
        {
            entry.UpdatedAt = DateTime.Now;
            entry.UpdatedBy = _tokenService.GetLoggedInUserinfo()?.UserId;

            const string sql = @"
            UPDATE WhitelistEntries
            SET
                EntryType = @EntryType,
                Value = @Value,
                Description = @Description,
                IsActive = @IsActive,
                UpdatedBy = @UpdatedBy,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

            var affected = await _dbConnection.ExecuteAsync(sql, entry);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            const string sql = @"DELETE FROM WhitelistEntries WHERE Id = @Id";
            var affected = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }
    }
}