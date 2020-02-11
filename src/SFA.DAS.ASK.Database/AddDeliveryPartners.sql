-- Add records to PostCodeRegion

DECLARE @rowCount int
SELECT @rowCount = COUNT(*) FROM DeliveryPartners

IF @rowCount = 0 
BEGIN

SET IDENTITY_INSERT DeliveryPartners ON

INSERT INTO DeliveryPartners (Id, Name, Status, UkPrn) VALUES
(1,'B L Training Limited','Live', '10000486'),
(2,'Workpays Limited','Live', '10037289'),
(3,'Education Development Trust','Live', '10001298'),
(4,'CXK Limited','Live', '10001648'),

SET IDENTITY_INSERT DeliveryPartners OFF

END
GO

