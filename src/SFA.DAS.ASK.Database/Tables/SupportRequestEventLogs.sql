CREATE TABLE [dbo].[SupportRequestEventLogs](
	[Id] [uniqueidentifier] NOT NULL,	
	[SupportRequestId] [uniqueidentifier] NOT NULL,
	[EventDate] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
	[Email] [nvarchar](250) NOT NULL
 CONSTRAINT [PK_SupportRequestEventLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO