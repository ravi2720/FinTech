using Dapper;
using FinTech_ApiPanel.Domain.Entities.ServiceMasters;
using FinTech_ApiPanel.Domain.Interfaces.IServiceMasters;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.ServiceMasters
{
    public class ServiceMasterRepository : IServiceMasterRepository
    {
        private readonly IDbConnection _dbConnection;

        public ServiceMasterRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<ServiceMaster>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM ServiceMasters";
                return await _dbConnection.QueryAsync<ServiceMaster>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving data.", ex);
            }
        }

        public async Task<ServiceMaster?> GetByIdAsync(long id)
        {
            try
            {
                var sql = "SELECT * FROM ServiceMasters WHERE Id = @id";
                return await _dbConnection.QueryFirstOrDefaultAsync<ServiceMaster>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the service by ID.", ex);
            }
        }

        public async Task<ServiceMaster?> GetByTypeAsync(string type)
        {
            try
            {
                var sql = "SELECT * FROM ServiceMasters WHERE Type = @type";
                return await _dbConnection.QueryFirstOrDefaultAsync<ServiceMaster>(sql, new { Type = type });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the service by type.", ex);
            }
        }

        public async Task<ServiceMaster?> GetByTitleAsync(string title)
        {
            try
            {
                var sql = "SELECT * FROM ServiceMasters WHERE Title = @title";
                return await _dbConnection.QueryFirstOrDefaultAsync<ServiceMaster>(sql, new { Title = title });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the service by Title.", ex);
            }
        }

        public async Task<long> AddAsync(ServiceMaster service)
        {
            try
            {
                var sql = "INSERT INTO ServiceMasters (Title,Type,IsActive) VALUES (@Title,@Type,@IsActive); SELECT CAST(SCOPE_IDENTITY() as BIGINT);";
                return await _dbConnection.ExecuteScalarAsync<long>(sql, service);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the service.", ex);
            }
        }

        public async Task<bool> UpdateAsync(ServiceMaster service)
        {
            try
            {
                var sql = "UPDATE ServiceMasters SET Title = @Title, Type = @Type, IsActive = @IsActive WHERE Id = @Id";
                var rowsAffected = await _dbConnection.ExecuteAsync(sql, service);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the service.", ex);
            }
        }
    }
}
