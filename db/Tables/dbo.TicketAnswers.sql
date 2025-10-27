CREATE TABLE [dbo].[TicketAnswers]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TicketId] [int] NOT NULL,
[UserId] [int] NULL,
[Answer] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MediaPath] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TicketAnswers] ADD CONSTRAINT [PK_TicketAnswers_1] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TicketAnswers] ADD CONSTRAINT [FK_TicketAnswers_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [dbo].[Ticket] ([Id])
GO
