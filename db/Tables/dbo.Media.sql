CREATE TABLE [dbo].[Media]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TableId] [int] NOT NULL,
[Path] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MimeType] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FileName] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Media] ADD CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
