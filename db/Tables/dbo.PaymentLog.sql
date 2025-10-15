CREATE TABLE [dbo].[PaymentLog]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[PaymentId] [int] NOT NULL,
[LogType] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RawData] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreationDate] [datetime2] NOT NULL CONSTRAINT [DF__PaymentLo__Creat__45544755] DEFAULT (sysutcdatetime())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PaymentLog] ADD CONSTRAINT [PK__PaymentL__3214EC073BE21106] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PaymentLog] ADD CONSTRAINT [FK__PaymentLo__Payme__4460231C] FOREIGN KEY ([PaymentId]) REFERENCES [dbo].[Payment] ([Id]) ON DELETE CASCADE
GO
