CREATE TABLE [dbo].[ResourceCategory]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Title] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TableId] [int] NOT NULL,
[Count] [int] NOT NULL,
[ParentId] [int] NULL,
[CreationDate] [datetime2] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceCategory] ADD CONSTRAINT [PK_ResourceCategory] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ResourceCategory_ParentId] ON [dbo].[ResourceCategory] ([ParentId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceCategory] ADD CONSTRAINT [FK_ResourceCategory_ResourceCategory_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[ResourceCategory] ([Id])
GO
