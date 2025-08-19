CREATE TABLE [dbo].[ResourceCategoryTranslation]
(
[Id] [int] NOT NULL,
[LanguageCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Title] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceCategoryTranslation] ADD CONSTRAINT [PK_ResourceCategoryTranslation] PRIMARY KEY CLUSTERED ([Id], [LanguageCode]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceCategoryTranslation] ADD CONSTRAINT [FK_ResourceCategoryTranslation_ResourceCategory] FOREIGN KEY ([Id]) REFERENCES [dbo].[ResourceCategory] ([Id])
GO
ALTER TABLE [dbo].[ResourceCategoryTranslation] ADD CONSTRAINT [FK_ResourceCategoryTranslation_ResourceCategory1] FOREIGN KEY ([Id]) REFERENCES [dbo].[ResourceCategory] ([Id])
GO
