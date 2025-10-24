CREATE TABLE [dbo].[Order]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[OrderNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TotalAmount] [decimal] (18, 0) NOT NULL,
[DiscountAmount] [decimal] (18, 0) NOT NULL CONSTRAINT [DF__Order__DiscountA__28B808A7] DEFAULT ((0)),
[FinalAmount] AS ([TotalAmount]-[DiscountAmount]) PERSISTED,
[Status] [tinyint] NOT NULL CONSTRAINT [DF__Order__Status__29AC2CE0] DEFAULT ((0)),
[UpdateDate] [datetime] NULL CONSTRAINT [DF__Order__UpdateDat__2B947552] DEFAULT (sysutcdatetime()),
[CreationDate] [datetime] NOT NULL CONSTRAINT [DF__Order__CreationD__2AA05119] DEFAULT (sysutcdatetime())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [PK__Order__3214EC0771F66E22] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [UQ__Order__CAC5E74355B53E8C] UNIQUE NONCLUSTERED ([OrderNumber]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_Order_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
GO
