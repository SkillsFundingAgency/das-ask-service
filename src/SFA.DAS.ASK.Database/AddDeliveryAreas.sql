-- Add records to PostCodeRegion

DECLARE @rowCount int
SELECT @rowCount = COUNT(*) FROM DeliveryAreas

IF @rowCount = 0 
BEGIN

SET IDENTITY_INSERT DeliveryAreas ON

INSERT INTO DeliveryAreas (Id, Area, Status, Ordering, DeliveryPartnerId) VALUES
(1,'East Midlands',1,4,2),
(2,'East of England',1,6,2),
(3,'London',1,7,3),
(4,'North East',1,1,1),
(5,'North West',1,2,1),
(6,'South East',1,8,4),
(7,'South West',1,9,4),
(8,'West Midlands',1,5,2),
(9,'Yorkshire and the Humber',1,3,1)

SET IDENTITY_INSERT DeliveryAreas OFF

END
GO

