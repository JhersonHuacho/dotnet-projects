CREATE TABLE [dbo].[Sale]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IdCustomer] INT NOT NULL, 
    [IdConcert] INT NOT NULL, 
    [SaleDate] DATETIME NOT NULL, 
    [OperationNumber] VARCHAR(50) NOT NULL, 
    [Total] DECIMAL(18, 2) NOT NULL, 
    [Quantity] SMALLINT NOT NULL,
    [Estado] BIT NOT NULL DEFAULT 1, 
    [FechaCreacion] DATETIME NOT NULL DEFAULT getdate(), 
    [UsuarioCreacion] VARCHAR(50) NOT NULL DEFAULT 'sql', 
    [FechaModificacion] DATETIME NULL, 
    [UsuarioModificacion] VARCHAR(50) NULL, 
    CONSTRAINT [FK_Sale_ToCustomer] FOREIGN KEY ([IdCustomer]) REFERENCES [Customer]([Id]), 
    CONSTRAINT [FK_Sale_ToConcert] FOREIGN KEY ([IdConcert]) REFERENCES [Concert]([Id]), 
)
