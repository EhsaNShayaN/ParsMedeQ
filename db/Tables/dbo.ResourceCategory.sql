CREATE TABLE [dbo].[ResourceCategory]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TableId] [int] NOT NULL,
[Count] [int] NOT NULL,
[ParentId] [int] NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceCategory] ADD CONSTRAINT [PK_ResourceCategory] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ResourceCategory_ParentId] ON [dbo].[ResourceCategory] ([ParentId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceCategory] ADD CONSTRAINT [FK_ResourceCategory_ResourceCategory_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[ResourceCategory] ([Id])
GO
