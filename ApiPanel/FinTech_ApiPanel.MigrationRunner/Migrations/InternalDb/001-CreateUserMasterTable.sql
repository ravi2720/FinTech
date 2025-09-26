CREATE TABLE UserMasters (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(255) NOT NULL,
    UserName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    EmailVerified BIT NOT NULL,
    Gender TINYINT NULL,
    DOB DATETIME NULL,
    PAN NVARCHAR(20) NULL,
    Salt NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    PhoneNumber VARCHAR(10) NULL,
    IPin BIGINT NOT NULL,
    PhoneVerified BIT NOT NULL,
    KYCVerified BIT NOT NULL,
    Address NVARCHAR(MAX) NULL,
    IsAdmin BIT NOT NULL,
    IsActive BIT NOT NULL,
    ProfilePicture NVARCHAR(MAX) NULL,
    LoginAttempts INT NOT NULL,
    GSTIN NVARCHAR(50) NULL,
    CompanyPAN NVARCHAR(20) NULL,
    CompanyTradeName NVARCHAR(255) NULL,
    CompanyLegalName NVARCHAR(255) NULL,
    BusinessCategory NVARCHAR(255) NULL,
    CompanyAddress NVARCHAR(MAX) NULL,
    CompanyLogo NVARCHAR(MAX) NULL,
    OutletId NVARCHAR(50) NULL,
    Prefix NVARCHAR(5) NULL,
    CreatedBy BIGINT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedBy BIGINT NULL,
    UpdatedAt DATETIME NULL
);

DECLARE @AdminUserId BIGINT;

-- Insert Admin User
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
    N'Administrator',
    N'admin',
    N'admin@mailinator.com',
    1,                         -- EmailVerified
    NULL,                      -- Gender
    '1990-01-01 00:00:00.000', -- DOB
    N'ABCDE1234F',             -- PAN
    N'<<SALT_VALUE>>',         -- Salt from cryptoService
    N'<<HASHED_PASSWORD>>',    -- Hash from cryptoService
    N'9999999999',             -- PhoneNumber
    N'123456',                 -- IPin (example OTP)
    1,                         -- PhoneVerified
    1,                         -- KYCVerified
    N'Admin Address',
    1,                         -- IsAdmin
    1,                         -- IsActive
    N'',                       -- ProfilePicture
    0,                         -- LoginAttempts
    N'22AAAAA0000A1Z5',        -- GSTIN
    N'ABCDE1234G',             -- CompanyPAN
    N'Admin Corp',             -- CompanyTradeName
    N'Admin Corporation Pvt Ltd', -- CompanyLegalName
    N'IT Services',            -- BusinessCategory
    N'Corporate Tower, Business Park, City', -- CompanyAddress
    NULL,                      -- CompanyLogo
    0,                         -- CreatedBy
    GETDATE(),                 -- CreatedAt
    NULL,                      -- UpdatedBy
    NULL                       -- UpdatedAt
);

SET @AdminUserId = SCOPE_IDENTITY();

-- Insert Wallet for Admin
INSERT INTO Wallets (UserId, TotalBalance, HeldAmount, CreatedBy, CreatedAt)
VALUES (@AdminUserId, 0, 0, @AdminUserId, GETDATE());
