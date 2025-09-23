using Dapper;
using FinTech_ApiPanel.Domain.Entities.Auth;
using FinTech_ApiPanel.Domain.Interfaces.IAuth;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.Auth
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbConnection _connection;

        public ClientRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Client> GetByUserIdAsync(long userId)
        {
            var sql = "SELECT UserId, ClientId, ClientSecret, EncryptionKey FROM Clients WHERE UserId = @UserId";
            return await _connection.QueryFirstOrDefaultAsync<Client>(sql, new { UserId = userId });
        }

        public async Task<Client> ValidateClient(string clientId, string clientSecret)
        {
            var sql = "SELECT UserId, ClientId, ClientSecret, EncryptionKey FROM Clients WHERE ClientId = @ClientId AND ClientSecret = @ClientSecret";
            return await _connection.QueryFirstOrDefaultAsync<Client>(sql, new { ClientId = clientId, ClientSecret = clientSecret });
        }

        public async Task<int> AddAsync(Client client)
        {
            var sql = @"
                INSERT INTO Clients (UserId, ClientId, ClientSecret, EncryptionKey) 
                VALUES (@UserId, @ClientId, @ClientSecret, @EncryptionKey)";
            return await _connection.ExecuteAsync(sql, client);
        }
        public async Task<int> UpdateAsync(Client client)
        {
            var sql = @"
            UPDATE Clients
            SET ClientId = @ClientId,
                ClientSecret = @ClientSecret,
                EncryptionKey = @EncryptionKey
            WHERE UserId = @UserId";
            return await _connection.ExecuteAsync(sql, client);
        }

        public async Task<Client?> GetAdminClientAsync()
        {
            const string sql = @"
            SELECT c.*
            FROM Clients c
            INNER JOIN UserMasters u ON u.Id = c.UserId
            WHERE u.IsAdmin = 1";

            return await _connection.QuerySingleOrDefaultAsync<Client>(sql);
        }
    }
}
