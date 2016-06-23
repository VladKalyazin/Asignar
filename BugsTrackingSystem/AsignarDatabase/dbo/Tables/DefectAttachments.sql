CREATE TABLE [dbo].[DefectAttachments] (
    [AttachmentID]   INT            IDENTITY (1, 1) NOT NULL,
    [AttachmentLink] NVARCHAR (100) NULL,
    [DefectID]       INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([AttachmentID] ASC),
    CONSTRAINT [FK_DefectAttachments_Defects] FOREIGN KEY ([DefectID]) REFERENCES [dbo].[Defects] ([DefectID])
);

