CREATE TABLE [dbo].[PlanningMeetings]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [SupportRequestId] UNIQUEIDENTIFIER NULL, 
    [ContactId] UNIQUEIDENTIFIER NULL, 
    [DeliveryPartnerId] UNIQUEIDENTIFIER NULL, 
    [MeetingType] INT NULL, 
    [MeetingTimeAndDate] DATETIME NULL
)
