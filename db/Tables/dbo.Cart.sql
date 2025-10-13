CREATE TABLE [dbo].[Cart]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[AnonymousId] [uniqueidentifier] NULL,
[UserId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cart] ADD CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cart] ADD CONSTRAINT [FK_Cart_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
GO
