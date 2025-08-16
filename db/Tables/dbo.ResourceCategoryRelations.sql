CREATE TABLE [dbo].[ResourceCategoryRelations]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TableId] [int] NOT NULL,
[ResourceCategoryId] [int] NOT NULL,
[ResourceId] [int] NOT NULL,
[CreationDate] [datetime2] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceCategoryRelations] ADD CONSTRAINT [PK_ResourceCategoryRelations] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [ix_ResourceCategoryRelations_ResourceId] ON [dbo].[ResourceCategoryRelations] ([TableId], [ResourceId]) INCLUDE ([ResourceCategoryId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResourceCategoryRelations] ADD CONSTRAINT [FK_ResourceCategoryRelations_Resource_ResourceId] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource] ([Id])
GO
ALTER TABLE [dbo].[ResourceCategoryRelations] ADD CONSTRAINT [FK_ResourceCategoryRelations_ResourceCategory_ResourceCategoryId] FOREIGN KEY ([ResourceCategoryId]) REFERENCES [dbo].[ResourceCategory] ([Id])
GO
