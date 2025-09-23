using Dapper;
using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Entities.UserWallets;
using FinTech_ApiPanel.Domain.Shared.Utils;
using System.Data;

namespace FinTech_ApiPanel.API.Middleware
{
    public class AdminSeeder
    {
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var scopedProvider = scope.ServiceProvider;

            var config = scopedProvider.GetRequiredService<IConfiguration>();
            var dbConnection = scopedProvider.GetRequiredService<IDbConnection>();
            var cryptoService = scopedProvider.GetRequiredService<ICryptoService>();

            var adminUser = new UserMaster
            {
                FullName = "Administrator",
                UserName = "admin",
                Email = "admin@mailinator.com",
                PhoneNumber = "9999999999",
                Address = "Admin Address",

                IsAdmin = true,
                IsActive = true,
                EmailVerified = true,
                PhoneVerified = true,
                KYCVerified = true,
                ProfilePicture = string.Empty,
                IPin = EncryptionUtils.GenerateOtp(),

                Gender = null, 
                DOB = new DateTime(1990, 1, 1),
                PAN = "ABCDE1234F",
                GSTIN = "22AAAAA0000A1Z5",
                CompanyPAN = "ABCDE1234G",
                CompanyTradeName = "Admin Corp",
                CompanyLegalName = "Admin Corporation Pvt Ltd",
                BusinessCategory = "IT Services",
                CompanyAddress = "Corporate Tower, Business Park, City",
                CompanyLogo = null,
                Prefix = "USER",
            };

            var password = "Pass@1234";
            var passwordResult = cryptoService.GenerateSaltedHash(password);
            adminUser.Salt = passwordResult.Salt;
            adminUser.Password = passwordResult.Hash;

            var existingAdmin = await dbConnection.QueryFirstOrDefaultAsync<UserMaster>(
                "SELECT * FROM UserMasters WHERE IsAdmin = 1");

            if (existingAdmin == null)
            {
                adminUser.CreatedAt = DateTime.Now;
                adminUser.CreatedBy = 0;

                const string insertQuery = @"
                INSERT INTO UserMasters
                (
                    FullName, UserName, Email, EmailVerified, Gender, DOB, PAN, Salt, Password,
                    PhoneNumber, IPin, PhoneVerified, KYCVerified, Address,
                    IsAdmin, IsActive, ProfilePicture, LoginAttempts,
                    GSTIN, CompanyPAN, CompanyTradeName, CompanyLegalName, BusinessCategory, CompanyAddress, CompanyLogo,
                    CreatedBy, CreatedAt, UpdatedBy, UpdatedAt
                )
                VALUES
                (
                    @FullName, @UserName, @Email, @EmailVerified, @Gender, @DOB, @PAN, @Salt, @Password,
                    @PhoneNumber, @IPin, @PhoneVerified, @KYCVerified, @Address,
                    @IsAdmin, @IsActive, @ProfilePicture, @LoginAttempts,
                    @GSTIN, @CompanyPAN, @CompanyTradeName, @CompanyLegalName, @BusinessCategory, @CompanyAddress, @CompanyLogo,
                    @CreatedBy, @CreatedAt, @UpdatedBy, @UpdatedAt
                );
                SELECT CAST(SCOPE_IDENTITY() as BIGINT);";

                var adminUserId = await dbConnection.ExecuteScalarAsync<long>(insertQuery, adminUser);

                var existingWallet = await dbConnection.QueryFirstOrDefaultAsync<long>(
                "SELECT Id FROM Wallets WHERE UserId = @UserId",
                new { UserId = adminUserId });

                if (existingWallet == 0)
                {
                    var wallet = new Wallet
                    {
                        UserId = adminUserId,
                        TotalBalance = 0,
                        HeldAmount = 0,
                        CreatedAt = DateTime.Now,
                        CreatedBy = adminUserId
                    };

                    const string insertWalletQuery = @"
                    INSERT INTO Wallets (UserId, TotalBalance, HeldAmount, CreatedBy, CreatedAt)
                    VALUES (@UserId, @TotalBalance, @HeldAmount, @CreatedBy, @CreatedAt);";

                    await dbConnection.ExecuteAsync(insertWalletQuery, wallet);
                }
            }            
        }
    }
}