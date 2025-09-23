using Dapper;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IOTPManagers;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.OTPManagers
{
    public class OTPManagerRepository : IOTPManagerRepository
    {
        private readonly IDbConnection _dbConnection;

        public OTPManagerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<long> AddAsync(OTPManager otp)
        {
            try
            {
                var sql = @"
                    INSERT INTO OTPManagers (Type, Email, PhoneNumber, Token, TokenValidTill, Otp, OtpValidTill, IsOtpVerified)
                    VALUES (@Type, @Email, @PhoneNumber, @Token, @TokenValidTill, @Otp, @OtpValidTill, @IsOtpVerified);
                    SELECT CAST(SCOPE_IDENTITY() as BIGINT);";

                var id = await _dbConnection.ExecuteScalarAsync<long>(sql, otp);
                return id;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error adding OTPManager", ex);
            }
        }

        public async Task<OTPManager?> GetByIdAsync(long id)
        {
            try
            {
                var sql = "SELECT * FROM OTPManagers WHERE Id = @Id";
                return await _dbConnection.QueryFirstOrDefaultAsync<OTPManager>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error retrieving OTPManager with Id {id}", ex);
            }
        }

        public async Task<IEnumerable<OTPManager>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM OTPManagers";
                return await _dbConnection.QueryAsync<OTPManager>(sql);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error retrieving all OTPManagers", ex);
            }
        }

        public async Task<bool> UpdateAsync(OTPManager otp)
        {
            try
            {
                var sql = @"
                    UPDATE OTPManagers
                    SET Type = @Type,
                        Email = @Email,
                        PhoneNumber = @PhoneNumber,
                        Token = @Token,
                        TokenValidTill = @TokenValidTill,
                        Otp = @Otp,
                        OtpValidTill = @OtpValidTill,
                        IsOtpVerified = @IsOtpVerified
                    WHERE Id = @Id";

                var affectedRows = await _dbConnection.ExecuteAsync(sql, otp);
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error updating OTPManager with Id {otp.Id}", ex);
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                var sql = "DELETE FROM OTPManagers WHERE Id = @Id";
                var affectedRows = await _dbConnection.ExecuteAsync(sql, new { Id = id });
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error deleting OTPManager with Id {id}", ex);
            }
        }
    }
}
