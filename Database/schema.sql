IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [BusinessCards] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Gender] nvarchar(20) NOT NULL,
    [DateOfBirth] datetime2 NULL,
    [Email] nvarchar(255) NOT NULL,
    [Phone] nvarchar(50) NOT NULL,
    [Address] nvarchar(500) NULL,
    [PhotoBase64] varchar(max) NULL,
    CONSTRAINT [PK_BusinessCards] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251109101137_InitialCreate', N'8.0.21');
GO

COMMIT;
GO

