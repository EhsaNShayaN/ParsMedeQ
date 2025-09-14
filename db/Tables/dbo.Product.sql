CREATE TABLE [dbo].[Product]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ProductCategoryId] [int] NOT NULL,
[Language] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PublishDate] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PublishInfo] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Publisher] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Price] [int] NULL,
[Discount] [int] NULL,
[IsVip] [bit] NOT NULL,
[DownloadCount] [int] NOT NULL,
[Ordinal] [int] NULL,
[Deleted] [bit] NOT NULL,
[Disabled] [bit] NOT NULL,
[ExpirationDate] [datetime] NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [FK_Product_ProductCategory_ProductCategoryId] FOREIGN KEY ([ProductCategoryId]) REFERENCES [dbo].[ProductCategory] ([Id]) ON DELETE CASCADE
GO
