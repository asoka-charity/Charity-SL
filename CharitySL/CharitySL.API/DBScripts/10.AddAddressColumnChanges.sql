ALTER TABLE [AspNetUsers] ADD [City] nvarchar(200) NULL;

ALTER TABLE [AspNetUsers] ADD [CountryId] int NULL;

ALTER TABLE [AspNetUsers] ADD [ProvinceId] int NULL;

ALTER TABLE [AspNetUsers] ADD [Street] nvarchar(200) NULL;

CREATE TABLE [Countries] (
    [Id] int NOT NULL IDENTITY,
    [CountryName] nvarchar(200) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
);

CREATE TABLE [Provinces] (
    [Id] int NOT NULL IDENTITY,
    [CountryId] int NOT NULL,
    [ProvinceName] nvarchar(200) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_Provinces] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Provinces_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id])
);

CREATE INDEX [IX_AspNetUsers_CountryId] ON [AspNetUsers] ([CountryId]);

CREATE INDEX [IX_AspNetUsers_ProvinceId] ON [AspNetUsers] ([ProvinceId]);

CREATE INDEX [IX_Provinces_CountryId] ON [Provinces] ([CountryId]);

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id]);

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250303132358_AddAddressColumnChanges', N'9.0.1');
