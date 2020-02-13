CREATE TABLE [DeliveryPartners](
	[Id] [uniqueidentifier] PRIMARY KEY,
	[Name] [nvarchar](256) NOT NULL, 
    [Status] [int] NOT NULL,
    [UkPrn] [int] NOT NULL
) ON [PRIMARY] 
