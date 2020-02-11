CREATE TABLE [dbo].[Organisations](
	[Id] [uniqueidentifier] NOT NULL,
	[UkPrn] [int] NOT NULL,
	[OrganisationName] [nvarchar](250) NOT NULL,
	[OrganisationType] [int] NULL,
	[OtherOrganisationType] [nvarchar](250) NULL,
	[BuildingAndStreet1] [nvarchar](250) NULL,
	[BuildingAndStreet2] [nvarchar](250) NULL,
	[TownOrCity] [nvarchar](250) NULL,
	[County] [nvarchar](250) NULL,
	[Postcode] [nvarchar](8) NOT NULL,
 CONSTRAINT [PK_Organisations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
