CREATE TABLE [dbo].[VisitActivities](
	[Id] [uniqueidentifier] NOT NULL,
	[VisitId] [uniqueidentifier] NOT NULL,
	[ActivityType] [int] NOT NULL,
 CONSTRAINT [PK_VisitActivities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

