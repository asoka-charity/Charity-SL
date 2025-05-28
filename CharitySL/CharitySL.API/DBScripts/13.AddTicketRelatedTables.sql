CREATE TABLE [LookupContactedBy] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_LookupContactedBy] PRIMARY KEY ([Id])
);

CREATE TABLE [TicketReservation] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [Email] nvarchar(200) NULL,
    [PhoneNumber] nvarchar(200) NULL,
    [NumberOfTickets] int NOT NULL,
    [IsStudent] bit NOT NULL,
    [ContactedBy] nvarchar(200) NULL,
    [Comments] nvarchar(500) NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_TicketReservation] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250317170305_AddTicketRelatedTables', N'9.0.1');