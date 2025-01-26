CREATE TABLE [dbo].[Books]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(100) NOT NULL, 
    [Author] NVARCHAR(100) NOT NULL, 
    [Published Year] INT NOT NULL, 
    [Genre] NCHAR(50) NOT NULL
)
