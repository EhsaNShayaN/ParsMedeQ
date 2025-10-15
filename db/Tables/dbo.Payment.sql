CREATE TABLE [dbo].[Payment]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[OrderId] [int] NOT NULL,
[Amount] [decimal] (18, 2) NOT NULL,
[PaymentMethod] [tinyint] NOT NULL,
[TransactionId] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Status] [tinyint] NOT NULL CONSTRAINT [DF__Payment__Status__3F9B6DFF] DEFAULT ('Pending'),
[PaidDate] [datetime2] NULL,
[CreationDate] [datetime2] NOT NULL CONSTRAINT [DF__Payment__Creatio__408F9238] DEFAULT (sysutcdatetime())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Payment] ADD CONSTRAINT [PK__Payment__3214EC0798BA99CF] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Payment] ADD CONSTRAINT [FK__Payment__OrderId__3EA749C6] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]) ON DELETE CASCADE
GO
