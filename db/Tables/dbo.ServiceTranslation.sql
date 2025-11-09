CREATE TABLE [dbo].[ServiceTranslation]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[LanguageCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ServiceId] [int] NOT NULL,
[Title] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Image] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ServiceTranslation] ADD CONSTRAINT [PK_ServiceTranslation] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ServiceTranslation] ADD CONSTRAINT [FK_ServiceTranslation_Service] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service] ([Id])
GO
