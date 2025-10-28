CREATE TABLE [dbo].[Product]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ProductCategoryId] [int] NOT NULL,
[Price] [int] NULL,
[Discount] [int] NULL,
[Stock] [int] NOT NULL,
[WarrantyExpirationTime] [int] NOT NULL,
[PeriodicServiceInterval] [int] NOT NULL,
[Deleted] [bit] NOT NULL,
[Disabled] [bit] NOT NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [FK_Product_ProductCategory_ProductCategoryId] FOREIGN KEY ([ProductCategoryId]) REFERENCES [dbo].[ProductCategory] ([Id]) ON DELETE CASCADE
GO
