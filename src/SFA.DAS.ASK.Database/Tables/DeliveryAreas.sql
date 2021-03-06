﻿CREATE TABLE [DeliveryAreas](
	[Id] [int] IDENTITY (1,1) PRIMARY KEY,
	[Area] [nvarchar](256) NOT NULL, 
    [Status] [int] NOT NULL, 
    [Ordering] INT NOT NULL DEFAULT 0,
    [DeliveryPartnerId] [uniqueidentifier] NOT NULL
) ON [PRIMARY] 
GO