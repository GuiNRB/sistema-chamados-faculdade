-- Script para marcar a migration como já aplicada no banco existente
-- Execute este script no seu SQL Server Management Studio ou similar

-- Criar a tabela __EFMigrationsHistory se não existir
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='__EFMigrationsHistory' AND xtype='U')
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END

-- Marcar a migration como aplicada
IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20251103200809_InitialSQLServerMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES ('20251103200809_InitialSQLServerMigration', '8.0.10');
END

PRINT 'Migration marcada como aplicada com sucesso!';