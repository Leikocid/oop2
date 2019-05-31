

USE MASTER
GO

IF DB_ID('computers') IS NOT NULL
BEGIN
    DROP DATABASE computers
END


CREATE DATABASE computers COLLATE Cyrillic_General_CS_AS;
GO

USE [computers]
GO

CREATE TABLE [dbo].[Processor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Model] [varchar](50) NOT NULL,
	[Developer] [varchar](50) NOT NULL,
	[CoresCount] int NOT NULL,
	CONSTRAINT [PK_Processor] PRIMARY KEY (
		[Id] ASC
	)
)
GO

CREATE TABLE [dbo].[Computer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Logo] [image] NULL,
	[ProcessorId] [int] NOT NULL,
	[MemorySize] [int] NULL,
	[VideoCard] [varchar](50) NULL,
	[DiskSize] [int] NULL,
	CONSTRAINT [PK_Computer] PRIMARY KEY (
		[Id] ASC
	)
)
GO

ALTER TABLE [dbo].[Computer] WITH CHECK ADD CONSTRAINT [FK_ComputerProcessor] FOREIGN KEY([ProcessorId])
REFERENCES [dbo].[Processor] ([Id])
ON DELETE CASCADE
GO

CREATE PROCEDURE CreateData
AS
BEGIN
	declare @IdentityOutput table ( ID int );
	declare @id INT;

	insert Processor (Model, Developer, CoresCount) output inserted.Id into @IdentityOutput values ('Core i7', 'intel', 8);
	select @id = (select ID from @IdentityOutput); delete from @IdentityOutput;
	insert Computer (Type, ProcessorId, MemorySize, VideoCard, DiskSize) values ('Notebook', @id, 16, 'Nvidia', 512);
	insert Computer (Type, ProcessorId, MemorySize, VideoCard, DiskSize) values ('Desktop', @id, 32, 'Nvidia', 512);
	insert Computer (Type, ProcessorId, MemorySize, VideoCard, DiskSize) values ('Ultrabook', @id, 8, 'Nvidia', 256);

	SELECT 'Создана 1 запись в Процессорах и 3 записи в Компьютерах'
END
GO

USE MASTER
GO
