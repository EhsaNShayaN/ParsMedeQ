CREATE TABLE [dbo].[Comment]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TestId] [int] NOT NULL,
[Icon] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Description] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RelatedId] [int] NOT NULL,
[TableId] [int] NOT NULL,
[TableName] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Data] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Answers] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsConfirmed] [bit] NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comment] ADD CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comment] ADD CONSTRAINT [FK_Comment_Users] FOREIGN KEY ([TestId]) REFERENCES [dbo].[Users] ([Id])
GO
