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

CREATE TABLE [Permissions] (
    [Id] int NOT NULL IDENTITY,
    [PermissionName] nvarchar(max) NULL,
    [CurrentDate] nvarchar(max) NULL,
    [CurrentTime] nvarchar(max) NULL,
    [CreationTime] datetimeoffset NOT NULL,
    [ModificationTime] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [ProductName] nvarchar(max) NULL,
    [Price] int NOT NULL,
    [Count] int NOT NULL,
    [IsEnough] bit NOT NULL,
    [Category] int NOT NULL,
    [CurrentDate] nvarchar(max) NULL,
    [CurrentTime] nvarchar(max) NULL,
    [CreationTime] datetimeoffset NOT NULL,
    [ModificationTime] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [Rolename] nvarchar(max) NULL,
    [CurrentDate] nvarchar(max) NULL,
    [CurrentTime] nvarchar(max) NULL,
    [CreationTime] datetimeoffset NOT NULL,
    [ModificationTime] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RolePermissions] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] int NOT NULL,
    [PermissionId] int NOT NULL,
    [CurrentDate] nvarchar(max) NULL,
    [CurrentTime] nvarchar(max) NULL,
    [CreationTime] datetimeoffset NOT NULL,
    [ModificationTime] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RolePermissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RolePermissions_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Permissions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RolePermissions_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Phonenumber] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [UserStatus] int NOT NULL,
    [ReferralSerial] nvarchar(max) NOT NULL,
    [ReferralCount] int NOT NULL,
    [IsObedient] bit NOT NULL,
    [RoleId] int NOT NULL,
    [CurrentDate] nvarchar(max) NULL,
    [CurrentTime] nvarchar(max) NULL,
    [CreationTime] datetimeoffset NOT NULL,
    [ModificationTime] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Otps] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [code] nvarchar(max) NOT NULL,
    [IsUsed] bit NOT NULL,
    [ExpireTime] datetimeoffset NOT NULL,
    [CurrentDate] nvarchar(max) NULL,
    [CurrentTime] nvarchar(max) NULL,
    [CreationTime] datetimeoffset NOT NULL,
    [ModificationTime] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Otps] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Otps_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ShopCarts] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [UserId] int NOT NULL,
    [CurrentDate] nvarchar(max) NULL,
    [CurrentTime] nvarchar(max) NULL,
    [CreationTime] datetimeoffset NOT NULL,
    [ModificationTime] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ShopCarts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ShopCarts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ShopCarts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Otps_UserId] ON [Otps] ([UserId]);
GO

CREATE INDEX [IX_RolePermissions_PermissionId] ON [RolePermissions] ([PermissionId]);
GO

CREATE INDEX [IX_RolePermissions_RoleId] ON [RolePermissions] ([RoleId]);
GO

CREATE INDEX [IX_ShopCarts_ProductId] ON [ShopCarts] ([ProductId]);
GO

CREATE INDEX [IX_ShopCarts_UserId] ON [ShopCarts] ([UserId]);
GO

CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220711100708_intial', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'ModificationTime');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] ALTER COLUMN [ModificationTime] nvarchar(max) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShopCarts]') AND [c].[name] = N'ModificationTime');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ShopCarts] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ShopCarts] ALTER COLUMN [ModificationTime] nvarchar(max) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Roles]') AND [c].[name] = N'ModificationTime');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Roles] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Roles] ALTER COLUMN [ModificationTime] nvarchar(max) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RolePermissions]') AND [c].[name] = N'ModificationTime');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [RolePermissions] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [RolePermissions] ALTER COLUMN [ModificationTime] nvarchar(max) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'ModificationTime');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Products] ALTER COLUMN [ModificationTime] nvarchar(max) NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Permissions]') AND [c].[name] = N'ModificationTime');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Permissions] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Permissions] ALTER COLUMN [ModificationTime] nvarchar(max) NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Otps]') AND [c].[name] = N'ModificationTime');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Otps] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Otps] ALTER COLUMN [ModificationTime] nvarchar(max) NULL;
GO

CREATE TABLE [Logs] (
    [Id] int NOT NULL IDENTITY,
    [BrowserName] nvarchar(max) NULL,
    [Username] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    [UrlAction] nvarchar(max) NULL,
    [CurrentDate] nvarchar(max) NULL,
    [CurrentTime] nvarchar(max) NULL,
    [CreationTime] datetimeoffset NOT NULL,
    [ModificationTime] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Logs] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220717114651_Log', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Products] ADD [filename] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220729115844_filename', N'6.0.6');
GO

COMMIT;
GO

