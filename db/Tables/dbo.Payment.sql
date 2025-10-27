CREATE TABLE [dbo].[Payment]
(
[Id] [int] NOT NULL,
[Amount] [decimal] (18, 0) NOT NULL,
[PaymentMethod] [tinyint] NOT NULL,
[TransactionId] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Status] [tinyint] NOT NULL CONSTRAINT [DF__Payment__Status__3F9B6DFF] DEFAULT ('Pending'),
[PaidDate] [datetime] NULL,
[CreationDate] [datetime] NOT NULL CONSTRAINT [DF__Payment__Creatio__408F9238] DEFAULT (sysutcdatetime())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Payment] ADD CONSTRAINT [PK__Payment__3214EC0798BA99CF] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Payment] ADD CONSTRAINT [FK_Payment_Order] FOREIGN KEY ([Id]) REFERENCES [dbo].[Order] ([Id])
GO
