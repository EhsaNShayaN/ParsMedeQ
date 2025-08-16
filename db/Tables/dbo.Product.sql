CREATE TABLE [dbo].[Product]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ProductCategoryId] [int] NOT NULL,
[ProductCategoryTitle] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Sequential] [int] NOT NULL,
[Title] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Abstract] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Anchors] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Image] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Video] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Doc] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Price] [int] NULL,
[Discount] [int] NULL,
[DownloadCount] [int] NOT NULL,
[VisitCount] [int] NOT NULL,
[SaleCount] [int] NOT NULL,
[Stock] [bit] NOT NULL,
[Deleted] [bit] NOT NULL,
[Disabled] [bit] NOT NULL,
[CreationDate] [datetime2] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Product_ProductCategoryId] ON [dbo].[Product] ([ProductCategoryId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [FK_Product_ProductCategory_ProductCategoryId] FOREIGN KEY ([ProductCategoryId]) REFERENCES [dbo].[ProductCategory] ([Id]) ON DELETE CASCADE
GO
