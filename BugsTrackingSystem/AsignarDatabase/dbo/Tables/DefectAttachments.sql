CREATE TABLE [dbo].[DefectAttachments]
(
	[AttachmentID] INT NOT NULL PRIMARY KEY, 
    [Link] NCHAR(250) NULL, 
    [DefectID] INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_DefectAttachments_Defects] FOREIGN KEY ([DefectID]) REFERENCES [dbo].[Defects] ([DefectID])
)
