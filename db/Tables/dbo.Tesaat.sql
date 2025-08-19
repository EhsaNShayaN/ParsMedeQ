CREATE TABLE [dbo].[Tesaat]
(
[TableName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LastId] [int] NULL,
[UpdatyyedIds] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Action] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Data] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
