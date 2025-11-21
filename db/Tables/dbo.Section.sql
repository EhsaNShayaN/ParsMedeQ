CREATE TABLE [dbo].[Section]
(
[Id] [int] NOT NULL,
[Visible] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Section] ADD CONSTRAINT [PK_Section] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
