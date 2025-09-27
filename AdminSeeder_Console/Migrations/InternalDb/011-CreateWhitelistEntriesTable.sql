CREATE TABLE WhitelistEntries (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    EntryType TINYINT NOT NULL,
    Value NVARCHAR(255) NOT NULL,
    Description NVARCHAR(255) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedBy BIGINT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedBy BIGINT NULL,
    UpdatedAt DATETIME NULL,
    FOREIGN KEY (CreatedBy) REFERENCES UserMasters(Id)
);
