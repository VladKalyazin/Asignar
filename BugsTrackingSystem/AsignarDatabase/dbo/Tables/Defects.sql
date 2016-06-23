CREATE TABLE [dbo].[Defects] (
    [DefectID]         INT            IDENTITY (1, 1) NOT NULL,
    [Subject]          NVARCHAR (50)  NULL,
    [ProjectID]        INT            NOT NULL,
    [AssigneeUserID]   INT            NOT NULL,
    [DefectPriorityID] INT            NOT NULL,
    [DefectStatusID]   INT            NOT NULL,
    [CreationDate]     DATETIME       NULL,
    [ModificationDate] DATETIME       NULL,
    [Description]      NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([DefectID] ASC),
    CONSTRAINT [FK_Defects_Priorities] FOREIGN KEY ([DefectPriorityID]) REFERENCES [dbo].[DefectPriorities] ([DefectPriorityID]),
    CONSTRAINT [FK_Defects_Projects] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Projects] ([ProjectID]),
    CONSTRAINT [FK_Defects_Statuses] FOREIGN KEY ([DefectStatusID]) REFERENCES [dbo].[DefectStatuses] ([DefectStatusID]),
    CONSTRAINT [FK_Defects_Users] FOREIGN KEY ([AssigneeUserID]) REFERENCES [dbo].[Users] ([UserID])
);

