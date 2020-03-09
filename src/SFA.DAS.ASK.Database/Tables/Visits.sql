
CREATE TABLE [dbo].[Visits](
	[Id] [uniqueidentifier] NOT NULL,
	[SupportRequestId] [uniqueidentifier] NOT NULL,
	[VisitDate] [datetime2](7) NOT NULL,
	[OrganisationContactId] [uniqueidentifier] NOT NULL
 CONSTRAINT [PK_Visits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


