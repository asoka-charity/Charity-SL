ALTER TABLE [AspNetUsers] ADD [UserTypeId] int NULL;

CREATE TABLE [UserTypes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(200) NULL,
    CONSTRAINT [PK_UserTypes] PRIMARY KEY ([Id])
);

CREATE INDEX [IX_AspNetUsers_UserTypeId] ON [AspNetUsers] ([UserTypeId]);

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_UserTypes_UserTypeId] FOREIGN KEY ([UserTypeId]) REFERENCES [UserTypes] ([Id]) ON DELETE SET NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250220160431_AddUserTypesTable', N'9.0.1');
