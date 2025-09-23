using Dapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Domain.Entities.FinancialComponentMasters;
using FinTech_ApiPanel.Domain.Interfaces.IFinancialComponentMasters;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.FinancialComponentMasters
{
    public class FinancialComponentMasterRepository : IFinancialComponentMasterRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ITokenService _tokenService;

        public FinancialComponentMasterRepository(IDbConnection dbConnection, ITokenService tokenService)
        {
            _dbConnection = dbConnection;
            _tokenService = tokenService;
        }

        public async Task<long> AddAsync(FinancialComponentMaster entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = _tokenService.GetLoggedInUserinfo().UserId;

            string sql = @"
            INSERT INTO FinancialComponentMasters 
            (OperatorId, Type, ServiceSubType, Start_Value, End_Value, Value, ServiceType, CalculationType, CreatedBy, CreatedAt)
            VALUES (@OperatorId, @Type, @ServiceSubType, @Start_Value, @End_Value, @Value, @ServiceType, @CalculationType, @CreatedBy, @CreatedAt);
            SELECT CAST(SCOPE_IDENTITY() as BIGINT);";

            return await _dbConnection.ExecuteScalarAsync<long>(sql, entity);
        }

        public async Task<FinancialComponentMaster> GetByIdAsync(long id)
        {
            string sql = "SELECT * FROM FinancialComponentMasters WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<FinancialComponentMaster>(sql, new { Id = id });
        }

        public async Task<IEnumerable<FinancialComponentMaster>> GetAllAsync()
        {
            string sql = "SELECT * FROM FinancialComponentMasters";
            return await _dbConnection.QueryAsync<FinancialComponentMaster>(sql);
        }

        public async Task<bool> UpdateAsync(FinancialComponentMaster entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = _tokenService.GetLoggedInUserinfo()?.UserId;

            string sql = @"
            UPDATE FinancialComponentMasters SET
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
            string sql = "DELETE FROM FinancialComponentMasters WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }

}
