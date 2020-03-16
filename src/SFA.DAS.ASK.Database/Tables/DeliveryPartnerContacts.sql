CREATE TABLE [dbo].[DeliveryPartnerContacts]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [DeliveryPartnerOrganisationId] UNIQUEIDENTIFIER NOT NULL, 
    [FirstName] NVARCHAR(250) NULL, 
    [LastName] NVARCHAR(250) NULL, 
    [PhoneNumber] NVARCHAR(250) NULL, 
    [Email] NVARCHAR(250) NULL
)
