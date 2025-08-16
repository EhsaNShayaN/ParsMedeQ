CREATE TABLE [dbo].[Resource]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TableId] [int] NOT NULL,
[Title] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Abstract] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Anchors] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Keywords] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ResourceCategoryId] [int] NOT NULL,
[ResourceCategoryTitle] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Image] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MimeType] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Doc] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Language] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PublishDate] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PublishInfo] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Publisher] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Price] [int] NULL,
[Discount] [int] NULL,
[IsVip] [bit] NOT NULL,
[DownloadCount] [int] NOT NULL,
[Ordinal] [int] NULL,
[Deleted] [bit] NOT NULL,
[Disabled] [bit] NOT NULL,
[ExpirationDate] [datetime] NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Resource] ADD CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Resource_ResourceCategoryId] ON [dbo].[Resource] ([ResourceCategoryId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Resource] ADD CONSTRAINT [FK_Resource_ResourceCategory_ResourceCategoryId] FOREIGN KEY ([ResourceCategoryId]) REFERENCES [dbo].[ResourceCategory] ([Id]) ON DELETE CASCADE
GO
