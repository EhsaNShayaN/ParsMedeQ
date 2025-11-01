CREATE TABLE [dbo].[TreatmentCenter]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[LocationId] [int] NOT NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TreatmentCenter] ADD CONSTRAINT [PK_TreatmentCenter] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TreatmentCenter] ADD CONSTRAINT [FK_TreatmentCenter_Location] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id])
GO
