CREATE TABLE FundRequests (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    UserId BIGINT NOT NULL,
    BankId BIGINT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    AdminRemark NVARCHAR(MAX) NULL,
    UserRemark NVARCHAR(MAX) NULL,
    ReferenceId NVARCHAR(255) NOT NULL,
    Attachment NVARCHAR(255) NOT NULL,
    TransactionDate DATETIME NOT NULL,
    ModeOfPayment TINYINT NOT NULL,
    Status TINYINT NOT NULL,
    CreatedBy BIGINT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedBy BIGINT NULL,
    UpdatedAt DATETIME NULL,
    FOREIGN KEY (UserId) REFERENCES UserMasters(Id),
    FOREIGN KEY (BankId) REFERENCES BankMasters(Id)
);