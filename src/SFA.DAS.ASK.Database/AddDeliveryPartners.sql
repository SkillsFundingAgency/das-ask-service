-- Add records to PostCodeRegion

DECLARE @rowCount int
SELECT @rowCount = COUNT(*) FROM DeliveryPartners

IF @rowCount = 0 
BEGIN

SET IDENTITY_INSERT DeliveryAreas ON

DECLARE @DeliveryPartnerId uniqueidentifier

SET @DeliveryPartnerId = NewId()
INSERT INTO DeliveryPartners (Id, Name, Status, UkPrn) VALUES
(@DeliveryPartnerId,'B L Training Limited',1, '10000486')
INSERT INTO DeliveryAreas (Id, Area, Status, Ordering, DeliveryPartnerId) VALUES
(4,'North East',1,1,@DeliveryPartnerId),
(5,'North West',1,2,@DeliveryPartnerId),
(9,'Yorkshire and the Humber',1,3,@DeliveryPartnerId)

SET @DeliveryPartnerId = NewId()
INSERT INTO DeliveryPartners (Id, Name, Status, UkPrn) VALUES
(@DeliveryPartnerId,'Workpays Limited',1, '10037289')
INSERT INTO DeliveryAreas (Id, Area, Status, Ordering, DeliveryPartnerId) VALUES
(1,'East Midlands',1,4,@DeliveryPartnerId),
(2,'East of England',1,6,@DeliveryPartnerId),
(8,'West Midlands',1,5,@DeliveryPartnerId)

SET @DeliveryPartnerId = NewId()
INSERT INTO DeliveryPartners (Id, Name, Status, UkPrn) VALUES
(@DeliveryPartnerId,'Education Development Trust',1, '10001298')
INSERT INTO DeliveryAreas (Id, Area, Status, Ordering, DeliveryPartnerId) VALUES
(3,'London',1,7,@DeliveryPartnerId)

SET @DeliveryPartnerId = NewId()
INSERT INTO DeliveryPartners (Id, Name, Status, UkPrn) VALUES
(@DeliveryPartnerId,'CXK Limited',1, '10001648')
INSERT INTO DeliveryAreas (Id, Area, Status, Ordering, DeliveryPartnerId) VALUES
(6,'South East',1,8,@DeliveryPartnerId),
(7,'South West',1,9,@DeliveryPartnerId)


SET IDENTITY_INSERT DeliveryAreas OFF

END
GO

