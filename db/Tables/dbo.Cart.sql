CREATE TABLE [dbo].[Cart]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NULL,
[AnonymousId] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cart] ADD CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
