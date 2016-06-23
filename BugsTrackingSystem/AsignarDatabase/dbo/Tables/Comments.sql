CREATE TABLE [dbo].[Comments] (
    [CommentID]   INT            IDENTITY (1, 1) NOT NULL,
    [DefectID]    INT            NOT NULL,
    [UserID]      INT            NOT NULL,
    [CommentText] NVARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([CommentID] ASC),
    CONSTRAINT [FK_Comments_Defects] FOREIGN KEY ([DefectID]) REFERENCES [dbo].[Defects] ([DefectID]),
    CONSTRAINT [FK_Comments_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

