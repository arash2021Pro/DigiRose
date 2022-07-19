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

