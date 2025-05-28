ALTER TABLE [ProjectContributions] ADD [ActualAmount] decimal(18,2) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250321204308_AddActualAmountColumn', N'9.0.1');