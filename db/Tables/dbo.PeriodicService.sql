CREATE TABLE [dbo].[PeriodicService]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[ProductId] [int] NOT NULL,
[ServiceDate] [datetime] NOT NULL,
[Done] [bit] NOT NULL,
[HasNext] [bit] NOT NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PeriodicService] ADD CONSTRAINT [PK_PeriodicService] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PeriodicService] ADD CONSTRAINT [FK_PeriodicService_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[PeriodicService] ADD CONSTRAINT [FK_PeriodicService_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
GO
