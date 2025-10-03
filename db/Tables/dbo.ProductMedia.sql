CREATE TABLE [dbo].[ProductMedia]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ProductId] [int] NOT NULL,
[MediaId] [int] NOT NULL,
[Ordinal] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductMedia] ADD CONSTRAINT [PK_ProductMedia] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
