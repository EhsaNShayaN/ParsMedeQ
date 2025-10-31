CREATE TABLE [dbo].[ProvinceTranslation]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[LanguageCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProvinceId] [int] NOT NULL,
[Title] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProvinceTranslation] ADD CONSTRAINT [PK_ProvinceTranslation] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProvinceTranslation] ADD CONSTRAINT [FK_ProvinceTranslation_Province] FOREIGN KEY ([ProvinceId]) REFERENCES [dbo].[Province] ([Id])
GO
