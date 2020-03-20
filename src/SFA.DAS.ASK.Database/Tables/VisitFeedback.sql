
CREATE TABLE [dbo].[VisitFeedback](
	[Id] [uniqueidentifier] NOT NULL,
	[VisitId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[FeedbackAnswers] [nvarchar](max) NOT NULL,
	[IncorrectDetailsComments] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_VisitFeedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


