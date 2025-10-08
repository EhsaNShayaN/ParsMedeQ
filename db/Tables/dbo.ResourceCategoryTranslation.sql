CREATE TABLE [dbo].[ResourceCategoryTranslation]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[LanguageCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ResourceCategoryId] [int] NOT NULL,
[Title] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceCategoryTranslation] ADD CONSTRAINT [PK_ResourceCategoryTranslation_1] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceCategoryTranslation] ADD CONSTRAINT [FK_ResourceCategoryTranslation_ResourceCategory2] FOREIGN KEY ([ResourceCategoryId]) REFERENCES [dbo].[ResourceCategory] ([Id])
GO
