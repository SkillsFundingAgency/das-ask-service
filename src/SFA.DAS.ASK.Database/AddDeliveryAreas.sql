-- Add records to PostCodeRegion

DECLARE @rowCount int
SELECT @rowCount = COUNT(*) FROM DeliveryAreas

IF @rowCount = 0 
BEGIN

SET IDENTITY_INSERT DeliveryAreas ON

INSERT INTO DeliveryAreas (Id, Area, Status, Ordering, DeliveryPartnerId) VALUES
(1,'East Midlands','Live',4,2),
(2,'East of England','Live',6,2),
(3,'London','Live',7,3),
(4,'North East','Live',1,1),
(5,'North West','Live',2,1),
(6,'South East','Live',8,4),
(7,'South West','Live',9,4),
(8,'West Midlands','Live',5,2),
(9,'Yorkshire and the Humber','Live',3,1)

SET IDENTITY_INSERT DeliveryAreas OFF

END
GO

