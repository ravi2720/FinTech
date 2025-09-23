CREATE TABLE FinancialComponentMasters (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    OperatorId BIGINT NOT NULL,
    Type TINYINT NOT NULL,
    Start_Value DECIMAL(18, 2) NULL,
    End_Value DECIMAL(18, 2) NULL,
    Value DECIMAL(18, 2) NOT NULL,
    ServiceType TINYINT NOT NULL,
    ServiceSubType TINYINT NULL,
    CalculationType TINYINT NULL,
    CreatedBy BIGINT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedBy BIGINT NULL,
    UpdatedAt DATETIME NULL,
    FOREIGN KEY (OperatorId) REFERENCES OperatorMasters(Id)
);