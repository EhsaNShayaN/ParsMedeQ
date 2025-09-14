CREATE TABLE [dbo].[ProductCategory]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ParentId] [int] NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductCategory] ADD CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductCategory] ADD CONSTRAINT [FK_ProductCategory_ProductCategory_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[ProductCategory] ([Id])
GO
