CREATE TABLE [dbo].[ProductTranslation]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ProductId] [int] NOT NULL,
[LanguageCode] [varchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Title] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Image] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FileId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductTranslation] ADD CONSTRAINT [PK_ProductTranslation_1] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductTranslation] ADD CONSTRAINT [FK_ProductTranslation_Product1] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
GO
