CREATE TABLE Clients (
    UserId BIGINT NOT NULL,
    ClientId NVARCHAR(100) NOT NULL,
    ClientSecret NVARCHAR(200) NOT NULL,
    EncryptionKey NVARCHAR(200) NOT NULL,
    PRIMARY KEY (UserId, ClientId, ClientSecret),
    FOREIGN KEY (UserId) REFERENCES UserMasters(Id)
);
