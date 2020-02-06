-- Add records to PostCodeRegion

DECLARE @rowCount int
SELECT @rowCount = COUNT(*) FROM DeliveryArea

IF @rowCount = 0 
BEGIN

SET IDENTITY_INSERT DeliveryArea ON

INSERT INTO DeliveryArea (Id, Area, Status, Ordering) VALUES
(1,'East Midlands','Live',4 ),
(2,'East of England','Live',6 ),
(3,'London','Live',7 ),
(4,'North East','Live',1 ),
(5,'North West','Live',2 ),
(6,'South East','Live',8 ),
(7,'South West','Live',9 ),
(8,'West Midlands','Live',5 ),
(9,'Yorkshire and the Humber','Live',3 )

SET IDENTITY_INSERT DeliveryArea OFF

END
GO

