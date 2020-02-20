CREATE TABLE [dbo].[TempSupportRequests](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](250) NULL,
	[LastName] [nvarchar](250) NULL,
	[JobRole] [nvarchar](250) NULL,
	[PhoneNumber] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[OrganisationName] [nvarchar](250) NULL,
	[OrganisationType] [int] NULL,
	[OtherOrganisationType] [nvarchar](250) NULL,
	[BuildingAndStreet1] [nvarchar](250) NULL,
	[BuildingAndStreet2] [nvarchar](250) NULL,
	[TownOrCity] [nvarchar](250) NULL,
	[County] [nvarchar](250) NULL,
	[Postcode] [nvarchar](8) NULL,
	[AdditionalComments] [nvarchar](max) NULL,
	[Agree] [bit] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[SupportRequestType] [int] NULL,
	[ReferenceId] [nvarchar](250) NULL,
	[Status] [int] NOT NULL,
	[DfeSignInId] [uniqueidentifier] NULL,
	[SelectedDfeSignInOrganisationId] [uniqueidentifier] NULL
 CONSTRAINT [PK_TempSupportRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TempSupportRequests] ADD  CONSTRAINT [DF_TempSupportRequests_Agree]  DEFAULT ((0)) FOR [Agree]
GO

ALTER TABLE [dbo].[TempSupportRequests] ADD  CONSTRAINT [DF_TempSupportRequests_Status]  DEFAULT ((0)) FOR [Status]
GO
