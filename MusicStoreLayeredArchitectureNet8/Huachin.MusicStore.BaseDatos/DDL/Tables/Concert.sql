CREATE TABLE [dbo].[Concert]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [IdGenre] INT NOT NULL,
    [Title] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(50) NOT NULL, 
    [Place] VARCHAR(50) NOT NULL, 
    [UnitPrice] DECIMAL(18, 2) NOT NULL, 
    [DateEvent] DATETIME NOT NULL, 
    [ImageUrl] VARCHAR(MAX) NULL, 
    [TicketsQuantity] INT NOT NULL, 
    [Finalized] BIT NOT NULL DEFAULT 0,
    [Estado] BIT NOT NULL DEFAULT 1, 
    [FechaCreacion] DATETIME NOT NULL DEFAULT getdate(), 
    [UsuarioCreacion] VARCHAR(50) NOT NULL DEFAULT 'sql', 
    [FechaModificacion] DATETIME NULL, 
    [UsuarioModificacion] VARCHAR(50) NULL, 
    CONSTRAINT [FK_Concert_ToGenre] FOREIGN KEY ([IdGenre]) REFERENCES [Genre]([Id]) 
)
