using Dapper;
using FinTech_ApiPanel.Domain.Entities.UserServices;
using FinTech_ApiPanel.Domain.Interfaces.IUserServices;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.UserServices
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserServiceRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<UserService>> GetByUserIdAsync(long userId)
        {
            try
            {
                var sql = "SELECT * FROM UserServices WHERE UserId = @UserId";
                return await _dbConnection.QueryAsync<UserService>(sql, new { UserId = userId });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user services for UserId {userId}: {ex.Message}", ex);
            }
        }

        public async Task<UserService> GetByUserIdAndServiceIdAsync(long userId, long serviceId)
        {
            try
            {
                var sql = "SELECT * FROM UserServices WHERE UserId = @UserId AND ServiceId = @ServiceId";
                return await _dbConnection.QuerySingleOrDefaultAsync<UserService>(sql, new { UserId = userId, ServiceId = serviceId });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user service for UserId {userId} and ServiceId {serviceId}: {ex.Message}", ex);
            }
        }

        public async Task AddAsync(UserService userService)
        {
            try
            {
                var sql = @"INSERT INTO UserServices (UserId, ServiceId, IsActive) VALUES (@UserId, @ServiceId, @IsActive);";

                await _dbConnection.ExecuteAsync(sql, userService);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding user service for UserId {userService.UserId} and ServiceId {userService.ServiceId}: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(UserService userService)
        {
            try
            {
                var sql = @"UPDATE UserServices SET IsActive = @IsActive WHERE UserId = @UserId AND ServiceId = @ServiceId;";

                await _dbConnection.ExecuteAsync(sql, userService);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user service for UserId {userService.UserId} and ServiceId {userService.ServiceId}: {ex.Message}", ex);
            }
        }
    }
}
