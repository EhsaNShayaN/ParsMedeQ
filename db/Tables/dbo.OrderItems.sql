CREATE TABLE [dbo].[OrderItems]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[OrderId] [int] NOT NULL,
[TableId] [int] NOT NULL,
[RelatedId] [int] NOT NULL,
[RelatedName] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Quantity] [int] NOT NULL,
[UnitPrice] [decimal] (18, 0) NOT NULL,
[Subtotal] AS ([Quantity]*[UnitPrice]) PERSISTED
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderItems] ADD CONSTRAINT [PK__OrderIte__3214EC07DF281839] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
