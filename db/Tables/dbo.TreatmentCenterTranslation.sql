CREATE TABLE [dbo].[TreatmentCenterTranslation]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[LanguageCode] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TreatmentCenterId] [int] NOT NULL,
[Title] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Image] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TreatmentCenterTranslation] ADD CONSTRAINT [PK_TreatmentCenterTranslation] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TreatmentCenterTranslation] ADD CONSTRAINT [FK_TreatmentCenterTranslation_TreatmentCenter] FOREIGN KEY ([TreatmentCenterId]) REFERENCES [dbo].[TreatmentCenter] ([Id])
GO
