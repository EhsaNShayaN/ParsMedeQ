CREATE TABLE [dbo].[SectionTranslation]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[LanguageCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SectionId] [int] NOT NULL,
[Title] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Image] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SectionTranslation] ADD CONSTRAINT [PK_SectionTranslation] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SectionTranslation] ADD CONSTRAINT [FK_SectionTranslation_Section] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Section] ([Id])
GO
