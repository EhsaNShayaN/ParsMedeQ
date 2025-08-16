CREATE TABLE [dbo].[Users]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[RegistrantId] [int] NOT NULL,
[Email] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Mobile] [varchar] (11) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsEmailConfirmed] [bit] NOT NULL,
[IsMobileConfirmed] [bit] NOT NULL,
[FirstName] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LastName] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Salt] [varchar] (2500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Password] [varchar] (2500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
