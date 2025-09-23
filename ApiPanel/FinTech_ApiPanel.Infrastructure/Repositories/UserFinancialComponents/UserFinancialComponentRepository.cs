using Dapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Domain.Entities.UserFinancialComponents;
using FinTech_ApiPanel.Domain.Interfaces.IUserFinancialComponents;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.UserFinancialComponents
{
    public class UserFinancialComponentRepository : IUserFinancialComponentRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ITokenService _tokenService;

        public UserFinancialComponentRepository(IDbConnection dbConnection, ITokenService tokenService)
        {
            _dbConnection = dbConnection;
            _tokenService = tokenService;
        }

        public async Task<long> AddAsync(UserFinancialComponent entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = _tokenService.GetLoggedInUserinfo().UserId;

            string sql = @"
            INSERT INTO UserFinancialComponents 
            (UserId, OperatorId, Type, ServiceSubType, Start_Value, End_Value, Value, ServiceType, CalculationType, CreatedBy, CreatedAt)
            VALUES (@UserId, @OperatorId, @Type, @ServiceSubType, @Start_Value, @End_Value, @Value, @ServiceType, @CalculationType, @CreatedBy, @CreatedAt);
            SELECT CAST(SCOPE_IDENTITY() as BIGINT);";

            return await _dbConnection.ExecuteScalarAsync<long>(sql, entity);
        }

        public async Task<UserFinancialComponent> GetByIdAsync(long id)
        {
            string sql = "SELECT * FROM UserFinancialComponents WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserFinancialComponent>(sql, new { Id = id });
        }

        public async Task<IEnumerable<UserFinancialComponent>> GetByUserIdAsync(long userId)
        {
            string sql = "SELECT * FROM UserFinancialComponents WHERE UserId = @UserId";
            return await _dbConnection.QueryAsync<UserFinancialComponent>(sql, new { UserId = userId });
        }

        public async Task<IEnumerable<UserFinancialComponent>> GetAllAsync()
        {
            string sql = "SELECT * FROM UserFinancialComponents";
            return await _dbConnection.QueryAsync<UserFinancialComponent>(sql);
        }

        public async Task<bool> UpdateAsync(UserFinancialComponent entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = _tokenService.GetLoggedInUserinfo()?.UserId;

            string sql = @"
            UPDATE UserFinancialComponents SET
                UserId = @UserId,
                OperatorId = @OperatorId,
                Type = @Type,
                ServiceSubType = @ServiceSubType,
                Start_Value = @Start_Value,
                End_Value = @End_Value,
                Value = @Value,
                ServiceType = @ServiceType,
                CalculationType = @CalculationType,
                UpdatedBy = @UpdatedBy,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

            return await _dbConnection.ExecuteAsync(sql, entity) > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            string sql = "DELETE FROM UserFinancialComponents WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}