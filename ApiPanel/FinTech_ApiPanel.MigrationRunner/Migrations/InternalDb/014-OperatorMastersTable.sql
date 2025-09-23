CREATE TABLE OperatorMasters
(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Code NVARCHAR(50) NOT NULL,
    Type TINYINT NOT NULL,
    Commission DECIMAL(5,2) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

INSERT INTO OperatorMasters (Name, Code, Type, Commission, IsActive)
VALUES
('Airtel Mobile', 'AMOB', 1, 2.10, 1),
('BSNL Mobile', 'BSNLM', 1, 5.00, 1),
('BSNL Special Mobile', 'BSNLS', 1, 5.00, 1),
('VI Mobile', 'VIM', 1, 4.00, 1),
('Airtel P2A Mobile (NON GST)', 'AP2A', 1, 2.50, 1),
('Jio Mobile', 'JIO', 1, 0.60, 1),
('Jio P2A Mobile (NON GST)', 'JIOP2A', 1, 0.80, 1),
('Airtel PostPaid', 'APP', 2, 0.30, 1),
('BSNL PostPaid', 'BSP', 2, 0.30, 1),
('Jio PostPaid', 'JIOP', 2, 0.30, 1),
('Vodafone Idea PostPaid', 'VIP', 2, 0.30, 1),
('Wiwanet PostPaid', 'WNP', 2, 0.00, 1),
('Airtel Digital TV', 'ADTV', 3, 4.20, 1),
('Dish TV', 'DTV', 3, 4.20, 1),
('Sun Direct', 'SDTV', 3, 3.50, 1),
('Tata Play', 'TPLAY', 3, 4.20, 1),
('Videocon d2h', 'VD2H', 3, 4.20, 1);