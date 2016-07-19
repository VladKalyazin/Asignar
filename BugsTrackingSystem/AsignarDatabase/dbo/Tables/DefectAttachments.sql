CREATE TABLE [dbo].[DefectAttachments]
(
	[AttachmentID] INT IDENTITY (1, 1) NOT NULL, 
    [Link] NVARCHAR(250) NULL, 
    [DefectID] INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL, 
	PRIMARY KEY CLUSTERED ([AttachmentID] ASC),
    CONSTRAINT [FK_DefectAttachments_Defects] FOREIGN KEY ([DefectID]) REFERENCES [dbo].[Defects] ([DefectID])
)
