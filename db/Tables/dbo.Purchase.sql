CREATE TABLE [dbo].[Purchase]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[RelatedId] [int] NOT NULL,
[TableId] [int] NOT NULL,
[Purchased] [bit] NOT NULL,
[Data] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Purchase] ADD CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Purchase] ADD CONSTRAINT [FK_Purchase_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
GO
