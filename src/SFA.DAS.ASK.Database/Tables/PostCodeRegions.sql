-- PostCode Regions Lookup

CREATE TABLE PostcodeRegions (
    PostcodePrefix NVARCHAR(2) NOT NULL PRIMARY KEY,
    Region  NVARCHAR(50)  NOT NULL,
    DeliveryAreaId [int] NULL
)
GO