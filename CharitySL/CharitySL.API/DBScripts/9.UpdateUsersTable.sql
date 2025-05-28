ALTER TABLE [AspNetUsers] ADD [IntroducedBy] nvarchar(300) NULL;

ALTER TABLE [AspNetUsers] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250302033831_UpdateUsersTable', N'9.0.1');