CREATE TABLE [dbo].[ProductCategory]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Title] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ParentId] [int] NULL,
[Sequential] [int] NOT NULL,
[Abstract] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Anchors] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Cover] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Image] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Deleted] [bit] NOT NULL,
[Disabled] [bit] NOT NULL,
[CreationDate] [datetime2] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductCategory] ADD CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ProductCategory_ParentId] ON [dbo].[ProductCategory] ([ParentId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductCategory] ADD CONSTRAINT [FK_ProductCategory_ProductCategory_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[ProductCategory] ([Id])
GO
