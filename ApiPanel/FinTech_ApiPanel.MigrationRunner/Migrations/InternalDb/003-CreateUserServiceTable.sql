CREATE TABLE UserServices (
    UserId BIGINT NOT NULL,
    ServiceId BIGINT NOT NULL,
    IsActive BIT NOT NULL,
    PRIMARY KEY (UserId, ServiceId),
    FOREIGN KEY (UserId) REFERENCES UserMasters(Id),
    FOREIGN KEY (ServiceId) REFERENCES ServiceMasters(Id)
);
