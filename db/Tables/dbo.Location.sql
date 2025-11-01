CREATE TABLE [dbo].[Location]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ParentId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Location] ADD CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Location] ADD CONSTRAINT [FK_Location_Location] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Location] ([Id])
GO
