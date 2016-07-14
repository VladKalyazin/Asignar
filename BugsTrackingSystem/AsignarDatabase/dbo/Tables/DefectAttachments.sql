CREATE TABLE [dbo].[DefectAttachments]
(
	[AttachmentID] INT NOT NULL PRIMARY KEY, 
    [Link] NCHAR(250) NULL, 
    [DefectID] INT NOT NULL,
	CONSTRAINT [FK_DefectAttachments_Defects] FOREIGN KEY ([DefectID]) REFERENCES [dbo].[Defects] ([DefectID])
)
