CREATE TABLE [dbo].[OutboxMessages]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[EventType] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Event] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[OccuredOn] [datetimeoffset] NOT NULL,
[IsIntegrationEvent] [bit] NOT NULL,
[ProcessedOn] [datetimeoffset] NULL,
[Error] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OutboxMessages] ADD CONSTRAINT [PK_OutboxMessages] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
