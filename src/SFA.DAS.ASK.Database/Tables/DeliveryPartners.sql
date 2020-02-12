CREATE TABLE [DeliveryPartners](
	[Id] [uniqueidentifier] IDENTITY (1,1) PRIMARY KEY,
	[Name] [nvarchar](256) NOT NULL, 
    [Status] [int] NOT NULL,
    [UkPrn] [int] NOT NULL
) ON [PRIMARY] 
