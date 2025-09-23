using Dapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Domain.Entities.UserWallets;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.UserWallets
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ITokenService _tokenService;

        public WalletRepository(IDbConnection dbConnection, ITokenService tokenService)
        {
            _dbConnection = dbConnection;
            _tokenService = tokenService;
        }

        public async Task<long> AddAsync(Wallet wallet)
        {
            wallet.CreatedAt = DateTime.Now;
            wallet.CreatedBy = _tokenService.GetLoggedInUserinfo().UserId;

            string sql = @"
            INSERT INTO Wallets (UserId, TotalBalance, HeldAmount, CreatedBy, CreatedAt)
            VALUES (@UserId, @TotalBalance, @HeldAmount, @CreatedBy, @CreatedAt);
            SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

            return await _dbConnection.ExecuteScalarAsync<long>(sql, wallet);
        }

        public async Task<Wallet?> GetByIdAsync(long id)
        {
            string sql = "SELECT * FROM Wallets WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Wallet>(sql, new { Id = id });
        }

        public async Task<Wallet> GetByUserIdAsync(long userId)
        {
            string sql = "SELECT * FROM Wallets WHERE UserId = @UserId ";
            return await _dbConnection.QueryFirstOrDefaultAsync<Wallet>(sql, new { UserId = userId });
        }

        public async Task<IEnumerable<Wallet>> GetAllAsync()
        {
            string sql = "SELECT * FROM Wallets ORDER BY CreatedAt DESC";
            return await _dbConnection.QueryAsync<Wallet>(sql);
        }

        public async Task<bool> UpdateAsync(Wallet wallet)
        {
            wallet.UpdatedAt = DateTime.Now;
            wallet.UpdatedBy = _tokenService.GetLoggedInUserinfo()?.UserId;

            string sql = @"
            UPDATE Wallets SET 
                UserId = @UserId,
                TotalBalance = @TotalBalance,
                HeldAmount = @HeldAmount,
                UpdatedBy = @UpdatedBy,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

            var rows = await _dbConnection.ExecuteAsync(sql, wallet);
            return rows > 0;
        }
    }
}
