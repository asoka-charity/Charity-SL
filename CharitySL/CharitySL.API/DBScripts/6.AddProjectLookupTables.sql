DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projects]') AND [c].[name] = N'Category');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Projects] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Projects] DROP COLUMN [Category];

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projects]') AND [c].[name] = N'Coordinator');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Projects] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Projects] DROP COLUMN [Coordinator];

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projects]') AND [c].[name] = N'Status');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Projects] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Projects] DROP COLUMN [Status];

ALTER TABLE [UserTypes] ADD [CreatedAt] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00';

ALTER TABLE [UserTypes] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [UserTypes] ADD [UpdatedAt] datetimeoffset NULL;

ALTER TABLE [Projects] ADD [ProjectCategoryId] int NULL;

ALTER TABLE [Projects] ADD [ProjectStatusId] int NULL;

CREATE TABLE [ProjectCategories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(200) NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_ProjectCategories] PRIMARY KEY ([Id])
);

CREATE TABLE [ProjectStatuses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(200) NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_ProjectStatuses] PRIMARY KEY ([Id])
);

CREATE INDEX [IX_Projects_ProjectCategoryId] ON [Projects] ([ProjectCategoryId]);

CREATE INDEX [IX_Projects_ProjectStatusId] ON [Projects] ([ProjectStatusId]);

ALTER TABLE [Projects] ADD CONSTRAINT [FK_Projects_ProjectCategories_ProjectCategoryId] FOREIGN KEY ([ProjectCategoryId]) REFERENCES [ProjectCategories] ([Id]) ON DELETE SET NULL;

ALTER TABLE [Projects] ADD CONSTRAINT [FK_Projects_ProjectStatuses_ProjectStatusId] FOREIGN KEY ([ProjectStatusId]) REFERENCES [ProjectStatuses] ([Id]) ON DELETE SET NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250220203938_AddProjectLookupTables', N'9.0.1');