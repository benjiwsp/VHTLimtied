CREATE TABLE [dbo].[MoneyIO]
(
	[M_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [info] NCHAR(60) NOT NULL, 
    [price] DECIMAL(18, 2) NOT NULL, 
    [whom] NCHAR(30) NOT NULL, 
    [category] NCHAR(30) NOT NULL, 
    [tmieOfIssue] DATETIME NOT NULL, 
    [inOrOut] INT NOT NULL, 
    [note] NCHAR(60) NULL, 
    [moneyBackYet] INT NULL
)
