USE [master]
GO
/****** Object:  Database [ProceduresDb]    Script Date: 23/05/2024 08:06:58 ******/
CREATE DATABASE [ProceduresDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProceduresDb', FILENAME = N'/var/opt/mssql/data/ProceduresDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProceduresDb_log', FILENAME = N'/var/opt/mssql/data/ProceduresDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ProceduresDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProceduresDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProceduresDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProceduresDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProceduresDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProceduresDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProceduresDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProceduresDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProceduresDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProceduresDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProceduresDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProceduresDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProceduresDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProceduresDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProceduresDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProceduresDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProceduresDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProceduresDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProceduresDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProceduresDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProceduresDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProceduresDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProceduresDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ProceduresDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProceduresDb] SET RECOVERY FULL 
GO
ALTER DATABASE [ProceduresDb] SET  MULTI_USER 
GO
ALTER DATABASE [ProceduresDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProceduresDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProceduresDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProceduresDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProceduresDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProceduresDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProceduresDb', N'ON'
GO
ALTER DATABASE [ProceduresDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProceduresDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

USE [ProceduresDb]
GO


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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240617124400_InitialSetup'
)
BEGIN
    CREATE TABLE [procedures] (
        [Id] int NOT NULL IDENTITY,
        [CurrentProcedureStatus] int NOT NULL,
        [CreatedOn] datetimeoffset NOT NULL,
        [LastModifiedOn] datetimeoffset NULL,
        CONSTRAINT [PK_procedures] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240617124400_InitialSetup'
)
BEGIN
    CREATE TABLE [attachments] (
        [Id] int NOT NULL IDENTITY,
        [ProcedureId] int NOT NULL,
        [Content] varbinary(max) NOT NULL,
        [FileName] nvarchar(max) NOT NULL,
        [CreatedOn] datetimeoffset NOT NULL,
        [LastModifiedOn] datetimeoffset NULL,
        CONSTRAINT [PK_attachments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_attachments_procedures_ProcedureId] FOREIGN KEY ([ProcedureId]) REFERENCES [procedures] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240617124400_InitialSetup'
)
BEGIN
    CREATE TABLE [procedureStatusHistory] (
        [Id] int NOT NULL IDENTITY,
        [ProcedureId] int NOT NULL,
        [ProcedureStatus] int NOT NULL,
        [CreatedOn] datetimeoffset NOT NULL,
        [LastModifiedOn] datetimeoffset NULL,
        CONSTRAINT [PK_procedureStatusHistory] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_procedureStatusHistory_procedures_ProcedureId] FOREIGN KEY ([ProcedureId]) REFERENCES [procedures] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240617124400_InitialSetup'
)
BEGIN
    CREATE INDEX [IX_attachments_ProcedureId] ON [attachments] ([ProcedureId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240617124400_InitialSetup'
)
BEGIN
    CREATE INDEX [IX_procedureStatusHistory_ProcedureId] ON [procedureStatusHistory] ([ProcedureId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240617124400_InitialSetup'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240617124400_InitialSetup', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240617124518_LogTable'
)
BEGIN

                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[logs]') AND type in (N'U'))
                    CREATE TABLE [dbo].[logs] (

                       [Id] int IDENTITY(1,1) NOT NULL,
                       [Message] nvarchar(max) NULL,
                       [MessageTemplate] nvarchar(max) NULL,
                       [Level] nvarchar(128) NULL,
                       [TimeStamp] datetime NOT NULL,
                       [Exception] nvarchar(max) NULL,
                       [Properties] nvarchar(max) NULL

                       CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC)
                    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240617124518_LogTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240617124518_LogTable', N'8.0.6');
END;
GO

COMMIT;
GO