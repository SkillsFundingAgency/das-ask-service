CREATE TABLE [EmailTemplates](
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[TemplateName] [nvarchar](100) NOT NULL,
	[TemplateId]  [nvarchar](100) NOT NULL,
	[Recipients]  [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[DeletedAt] [datetime2](7) NULL,	
	[UpdatedAt] [datetime2](7) NULL,	
    [RecipientTemplate] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_EmailTemplates] PRIMARY KEY ([Id]),
) ON [PRIMARY] 
GO

CREATE UNIQUE INDEX [IXU_EmailTemplates] ON [EmailTemplates] ([TemplateName])
GO