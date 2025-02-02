CREATE TABLE Tickets (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Status NVARCHAR(50) DEFAULT 'Open',
    CreatedDate DATETIME DEFAULT GETDATE(),
    Priority NVARCHAR(50),
    CreatedBy NVARCHAR(255)
);

select * from Tickets;