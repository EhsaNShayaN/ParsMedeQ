CREATE TABLE [dbo].[Section]
(
[Id] [int] NOT NULL,
[Hidden] [bit] NOT NULL,
[Title] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Section] ADD CONSTRAINT [PK_Section] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
