CREATE TABLE [DeliveryPartners](
	[Id] [int] IDENTITY (1,1) PRIMARY KEY,
	[Name] [nvarchar](256) NOT NULL, 
    [Status] [nvarchar](10) NOT NULL,
    [UkPrn] [nvarchar](10) NOT NULL
) ON [PRIMARY] 
