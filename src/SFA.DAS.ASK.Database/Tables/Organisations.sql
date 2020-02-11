CREATE TABLE [Organisations](
	[Id] [uniqueidentifier] NOT NULL,
	[OrganisationName] [nvarchar](400) NOT NULL,
	[OrganisationType] [nvarchar](20) NOT NULL,  
	[OrganisationReferenceId] [nvarchar](200) NULL,
	OrganisationData [nvarchar](max) NULL,
    RAGRatings [nvarchar](max) NULL,
	[Status] [nvarchar](20) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
	[UpdatedAt] [datetime2](7) NULL,
	[DeletedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Organisations] PRIMARY KEY ([Id])
 )
 GO
