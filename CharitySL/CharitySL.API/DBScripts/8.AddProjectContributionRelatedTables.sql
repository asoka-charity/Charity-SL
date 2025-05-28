CREATE TABLE [PaymentMethods] (
    [Id] int NOT NULL IDENTITY,
    [MethodName] nvarchar(100) NOT NULL,
    [Description] nvarchar(200) NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_PaymentMethods] PRIMARY KEY ([Id])
);

CREATE TABLE [PaymentStatuses] (
    [Id] int NOT NULL IDENTITY,
    [Status] nvarchar(100) NOT NULL,
    [Description] nvarchar(200) NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_PaymentStatuses] PRIMARY KEY ([Id])
);

CREATE TABLE [ProjectOwners] (
    [Id] int NOT NULL IDENTITY,
    [ProjectId] int NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_ProjectOwners] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectOwners_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_ProjectOwners_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
);

CREATE TABLE [ProjectContributions] (
    [Id] int NOT NULL IDENTITY,
    [ProjectOwnerId] int NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [PaidDate] datetimeoffset NULL,
    [PaymentMethodId] int NOT NULL,
    [PaymentReference] nvarchar(200) NULL,
    [PaymentStatusId] int NOT NULL,
    [AdditionalNote] nvarchar(200) NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_ProjectContributions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectContributions_PaymentMethods_PaymentMethodId] FOREIGN KEY ([PaymentMethodId]) REFERENCES [PaymentMethods] ([Id]),
    CONSTRAINT [FK_ProjectContributions_PaymentStatuses_PaymentStatusId] FOREIGN KEY ([PaymentStatusId]) REFERENCES [PaymentStatuses] ([Id]),
    CONSTRAINT [FK_ProjectContributions_ProjectOwners_ProjectOwnerId] FOREIGN KEY ([ProjectOwnerId]) REFERENCES [ProjectOwners] ([Id])
);

CREATE INDEX [IX_ProjectContributions_PaymentMethodId] ON [ProjectContributions] ([PaymentMethodId]);

CREATE INDEX [IX_ProjectContributions_PaymentStatusId] ON [ProjectContributions] ([PaymentStatusId]);

CREATE INDEX [IX_ProjectContributions_ProjectOwnerId] ON [ProjectContributions] ([ProjectOwnerId]);

CREATE INDEX [IX_ProjectOwners_ProjectId] ON [ProjectOwners] ([ProjectId]);

CREATE INDEX [IX_ProjectOwners_UserId] ON [ProjectOwners] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250226134817_AddProjectContributionRelatedTables', N'9.0.1');
