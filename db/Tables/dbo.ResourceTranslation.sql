CREATE TABLE [dbo].[ResourceTranslation]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ResourceId] [int] NOT NULL,
[LanguageCode] [varchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Title] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Abstract] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Anchors] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Keywords] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceTranslation] ADD CONSTRAINT [PK_ResourceTranslation_1] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceTranslation] ADD CONSTRAINT [FK_ResourceTranslation_Resource1] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource] ([Id])
GO
