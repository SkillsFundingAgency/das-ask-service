CREATE TABLE [dbo].[SupportRequests](
	[Id] [uniqueidentifier] NOT NULL,	
	[AdditionalComments] [nvarchar](max) NULL,
	[CurrentStatus] [int] NOT NULL,
	[OrganisationId] [uniqueidentifier] NOT NULL,
	[OrganisationContactId] [uniqueidentifier] NOT NULL,
	[DeliveryPartnerId] [uniqueidentifier] NOT NULL	
 CONSTRAINT [PK_SupportRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO