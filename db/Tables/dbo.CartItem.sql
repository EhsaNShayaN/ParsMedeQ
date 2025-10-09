CREATE TABLE [dbo].[CartItem]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CartId] [int] NOT NULL,
[TableId] [int] NOT NULL,
[RelatedId] [int] NOT NULL,
[RelatedName] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[UnitPrice] [decimal] (18, 0) NOT NULL,
[Quantity] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CartItem] ADD CONSTRAINT [PK_CartItem] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CartItem] ADD CONSTRAINT [FK_CartItem_Cart] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Cart] ([Id])
GO
