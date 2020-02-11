CREATE TABLE [dbo].[OrganisationContacts](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](250) NOT NULL,
	[LastName] [nvarchar](250) NOT NULL,
	[JobRole] [nvarchar](250) NULL,
	[PhoneNumber] [nvarchar](250) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[OrganisationId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_OrganisationContacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

