CREATE TABLE [dbo].[DeliveryPartnerContacts](
	[Id] [uniqueidentifier] NOT NULL,
	[DeliveryPartnerId] [uniqueidentifier] NOT NULL,
	[SignInId] [uniqueidentifier] NULL,
	[DisplayName] [nvarchar](250) NOT NULL,
 [FirstName] NCHAR(250) NULL, 
    [LastName] NCHAR(250) NULL, 
    [PhoneNumber] NCHAR(250) NULL, 
    [Email] NCHAR(250) NULL, 
    CONSTRAINT [PK_DeliveryPartnerContacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_DeliveryPartnerContacts_SignInId] ON [dbo].[DeliveryPartnerContacts]
(
	[SignInId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO