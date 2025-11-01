CREATE TABLE [dbo].[LocationTranslation]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[LanguageCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LocationId] [int] NOT NULL,
[Title] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LocationTranslation] ADD CONSTRAINT [PK_LocationTranslation] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LocationTranslation] ADD CONSTRAINT [FK_LocationTranslation_Location] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id])
GO
