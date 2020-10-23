CREATE TABLE dbo.Books (
	ISBN char(13) PRIMARY KEY NOT NULL,
	Title varchar(50) NOT NULL,
	Author varchar(50) NOT NULL,
	TotalCopies int NOT NULL,
	AvailableCopies int NOT NULL
)
GO