CREATE TABLE [dbo].[Province]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ParentId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Province] ADD CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Province] ADD CONSTRAINT [FK_Province_Province] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Province] ([Id])
GO
