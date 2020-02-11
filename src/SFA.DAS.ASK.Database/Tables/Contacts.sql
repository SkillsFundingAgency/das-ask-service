CREATE TABLE [Contacts](
	[Id] [uniqueidentifier] NOT NULL,
	[DisplayName] [nvarchar](120) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[OrganisationId] [uniqueidentifier] NULL,
	[Status] [nvarchar](20) NOT NULL,
	[PhoneNumber] [NVARCHAR] (50) NULL,
	[GivenNames] [NVARCHAR](120) NOT NULL DEFAULT '',
	[FamilyName] [NVARCHAR](120) NOT NULL DEFAULT '',
	[SignInType] [NVARCHAR](20) NOT NULL DEFAULT '',
    [SigninIdId] [uniqueidentifier] NULL,
	[CreatedAt] [datetime2](7) NOT NULL  DEFAULT GETUTCDATE(),
	[UpdatedAt] [datetime2](7) NULL,
	[DeletedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
)
GO



