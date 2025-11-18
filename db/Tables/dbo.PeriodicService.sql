CREATE TABLE [dbo].[PeriodicService]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[OrderItemId] [int] NOT NULL,
[ServiceDate] [datetime] NOT NULL,
[Done] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PeriodicService] ADD CONSTRAINT [PK_PeriodicService] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PeriodicService] ADD CONSTRAINT [FK_PeriodicService_OrderItems] FOREIGN KEY ([OrderItemId]) REFERENCES [dbo].[OrderItems] ([Id])
GO
