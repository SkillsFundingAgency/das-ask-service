CREATE TABLE [dbo].[PlanningMeetings]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [SupportRequestId] UNIQUEIDENTIFIER NULL, 
    [OrganisationContactId] UNIQUEIDENTIFIER NULL, 
    [DeliveryPartnerContactId] UNIQUEIDENTIFIER NULL, 
    [MeetingType] INT NULL, 
    [MeetingTimeAndDate] DATETIME NULL, 
    [Status] INT NULL
)