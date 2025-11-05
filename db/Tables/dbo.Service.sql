CREATE TABLE [dbo].[Service]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Title] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Image] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
