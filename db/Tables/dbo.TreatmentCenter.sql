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
ALTER TABLE [dbo].[TreatmentCenter] ADD CONSTRAINT [FK_TreatmentCenter_Province] FOREIGN KEY ([ProvinceId]) REFERENCES [dbo].[Province] ([Id])
GO
ALTER TABLE [dbo].[TreatmentCenter] ADD CONSTRAINT [FK_TreatmentCenter_Province1] FOREIGN KEY ([CityId]) REFERENCES [dbo].[Province] ([Id])
GO
