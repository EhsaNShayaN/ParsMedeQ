CREATE TABLE [dbo].[TreatmentCenter]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ProvinceId] [int] NOT NULL,
[CityId] [int] NOT NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TreatmentCenter] ADD CONSTRAINT [PK_TreatmentCenter] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TreatmentCenter] ADD CONSTRAINT [FK_TreatmentCenter_Location] FOREIGN KEY ([CityId]) REFERENCES [dbo].[Location] ([Id])
GO
ALTER TABLE [dbo].[TreatmentCenter] ADD CONSTRAINT [FK_TreatmentCenter_Location1] FOREIGN KEY ([ProvinceId]) REFERENCES [dbo].[Location] ([Id])
GO
