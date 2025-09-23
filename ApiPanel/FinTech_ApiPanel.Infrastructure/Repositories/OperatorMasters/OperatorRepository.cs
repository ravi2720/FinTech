using Dapper;
using FinTech_ApiPanel.Domain.Entities.OperatorMasters;
using FinTech_ApiPanel.Domain.Interfaces.IOperatorMasters;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.OperatorMasters
{
    public class OperatorRepository : IOperatorRepository
    {
        private readonly IDbConnection _dbConnection;

        public OperatorRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<OperatorMaster> GetAll()
        {
            string sql = "SELECT * FROM OperatorMasters";
            return _dbConnection.Query<OperatorMaster>(sql);
        }

        public async Task<OperatorMaster?> GetByIdAsync(long id)
        {
            string sql = "SELECT * FROM OperatorMasters WHERE Id = @id";
            return await _dbConnection.QueryFirstOrDefaultAsync<OperatorMaster>(sql, new { id });
        }

        public async Task<OperatorMaster?> GetByCodeAsync(string code)
        {
            string sql = "SELECT * FROM OperatorMasters WHERE Code = @code";
            return await _dbConnection.QueryFirstOrDefaultAsync<OperatorMaster>(sql, new { code });
        }
    }
}
