CREATE TABLE [dbo].[Ticket]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[Title] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MediaPath] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Status] [tinyint] NOT NULL,
[Code] [int] NOT NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ticket] ADD CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ticket] ADD CONSTRAINT [FK_Ticket_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
GO
