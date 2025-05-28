CREATE TABLE [EmailAudits] (
    [Id] int NOT NULL IDENTITY,
    [EmailReceipient] nvarchar(max) NULL,
    [EmailSubject] nvarchar(max) NULL,
    [EmailBody] nvarchar(max) NULL,
    [EmailStatus] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_EmailAudits] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250224232826_AddEmailAuditTable', N'9.0.1');