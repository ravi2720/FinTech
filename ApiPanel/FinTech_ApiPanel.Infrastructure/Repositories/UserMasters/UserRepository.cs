using Dapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using System.Data;

namespace FinTech_ApiPanel.Infrastructure.Repositories.UserMasters
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ITokenService _tokenService;

        public UserRepository(IDbConnection dbConnection, ITokenService tokenService)
        {
            _dbConnection = dbConnection;
            _tokenService = tokenService;
        }

        public async Task<int> AddAsync(UserMaster user)
        {
            try
            {
                user.CreatedAt = DateTime.Now;
                user.CreatedBy = _tokenService.GetLoggedInUserinfo().UserId;

                var sql = @"
                INSERT INTO UserMasters
                (
                    FullName, UserName, Email, EmailVerified, Gender, DOB, PAN, Salt, Password,
                    PhoneNumber, IPin, PhoneVerified, KYCVerified, Address,
                    IsAdmin, IsActive, ProfilePicture, LoginAttempts,
                    GSTIN, CompanyPAN, CompanyTradeName, CompanyLegalName, BusinessCategory, CompanyAddress, CompanyLogo, OutletId, Prefix,
                    CreatedBy, CreatedAt, UpdatedBy, UpdatedAt
                )
                VALUES
                (
                    @FullName, @UserName, @Email, @EmailVerified, @Gender, @DOB, @PAN, @Salt, @Password,
                    @PhoneNumber, @IPin, @PhoneVerified, @KYCVerified, @Address,
                    @IsAdmin, @IsActive, @ProfilePicture, @LoginAttempts,
                    @GSTIN, @CompanyPAN, @CompanyTradeName, @CompanyLegalName, @BusinessCategory, @CompanyAddress, @CompanyLogo, @OutletId, @Prefix,
                    @CreatedBy, @CreatedAt, @UpdatedBy, @UpdatedAt
                );
                SELECT CAST(SCOPE_IDENTITY() as int);";

                return await _dbConnection.QuerySingleAsync<int>(sql, user);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the user.", ex);
            }
        }

        public async Task<UserMaster?> GetByIdAsync(long id)
        {
            try
            {
                var sql = "SELECT * FROM UserMasters WHERE Id = @Id";
                return await _dbConnection.QuerySingleOrDefaultAsync<UserMaster>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the user with ID {id}.", ex);
            }
        }

        public async Task<UserMaster?> GetAdminAsync()
        {
            try
            {
                var sql = "SELECT TOP 1 * FROM UserMasters WHERE IsAdmin = @IsAdmin";
                return await _dbConnection.QuerySingleOrDefaultAsync<UserMaster>(sql, new { IsAdmin = true });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the admin user.", ex);
            }
        }


        public async Task<IEnumerable<UserMaster>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM UserMasters";
                return await _dbConnection.QueryAsync<UserMaster>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all users.", ex);
            }
        }

        public async Task<UserMaster?> GetByEmailOrUserNameAsync(string emailOrUserName)
        {
            try
            {
                var sql = @"
                SELECT * FROM UserMasters 
                WHERE Email = @Value OR UserName = @Value";

                return await _dbConnection.QuerySingleOrDefaultAsync<UserMaster>(sql, new { Value = emailOrUserName });
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the user with email or username '{emailOrUserName}'.", ex);
            }
        }

        public async Task<UserMaster?> GetByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                var sql = @"
                SELECT * FROM UserMasters 
                WHERE PhoneNumber = @Value";

                return await _dbConnection.QuerySingleOrDefaultAsync<UserMaster>(sql, new { Value = phoneNumber });
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the user with phone number '{phoneNumber}'.", ex);
            }
        }

        public async Task<bool> UpdateAsync(UserMaster user)
        {
            try
            {
                user.UpdatedAt = DateTime.Now;
                user.UpdatedBy = _tokenService.GetLoggedInUserinfo()?.UserId;

                var sql = @"
                UPDATE UserMasters SET
                    FullName = @FullName,
                    UserName = @UserName,
                    Email = @Email,
                    EmailVerified = @EmailVerified,
                    Gender = @Gender,
                    DOB = @DOB,
                    PAN = @PAN,
                    Salt = @Salt,
                    Password = @Password,
                    PhoneNumber = @PhoneNumber,
                    IPin = @IPin,
                    PhoneVerified = @PhoneVerified,
                    KYCVerified = @KYCVerified,
                    Address = @Address,
                    IsAdmin = @IsAdmin,
                    IsActive = @IsActive,
                    ProfilePicture = @ProfilePicture,
                    LoginAttempts = @LoginAttempts,
                    GSTIN = @GSTIN,
                    CompanyPAN = @CompanyPAN,
                    CompanyTradeName = @CompanyTradeName,
                    CompanyLegalName = @CompanyLegalName,
                    BusinessCategory = @BusinessCategory,
                    CompanyAddress = @CompanyAddress,
                    CompanyLogo = @CompanyLogo,
                    OutletId = @OutletId,
                    Prefix = @Prefix,
                    UpdatedBy = @UpdatedBy,
                    UpdatedAt = @UpdatedAt
                WHERE Id = @Id";

                var affectedRows = await _dbConnection.ExecuteAsync(sql, user);
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the user with ID {user.Id}.", ex);
            }
        }
    }
}
