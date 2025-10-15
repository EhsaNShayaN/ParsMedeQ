CREATE TABLE [dbo].[OrderItem]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[OrderId] [int] NOT NULL,
[TableId] [int] NOT NULL,
[RelatedId] [int] NOT NULL,
[Quantity] [int] NOT NULL,
[UnitPrice] [decimal] (18, 2) NOT NULL,
[Subtotal] AS ([Quantity]*[UnitPrice]) PERSISTED
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderItem] ADD CONSTRAINT [PK__OrderIte__3214EC07DF281839] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderItem] ADD CONSTRAINT [FK__OrderItem__Order__3BCADD1B] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]) ON DELETE CASCADE
GO
