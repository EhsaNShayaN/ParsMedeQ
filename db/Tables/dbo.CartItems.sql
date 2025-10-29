CREATE TABLE [dbo].[CartItems]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CartId] [int] NOT NULL,
[TableId] [int] NOT NULL,
[RelatedId] [int] NOT NULL,
[RelatedName] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[UnitPrice] [decimal] (18, 0) NOT NULL,
[Quantity] [int] NOT NULL,
[Data] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CartItems] ADD CONSTRAINT [PK_CartItems] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CartItems] ADD CONSTRAINT [FK_CartItems_Cart] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Cart] ([Id])
GO
