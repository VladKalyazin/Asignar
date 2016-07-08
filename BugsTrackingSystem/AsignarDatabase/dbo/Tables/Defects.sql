CREATE TABLE [dbo].[Defects] (
    [DefectID]         INT            IDENTITY (1, 1) NOT NULL,
    [Subject]          NVARCHAR (200)  NOT NULL,
    [ProjectID]        INT            NOT NULL,
    [AssigneeUserID]   INT            NOT NULL,
    [DefectPriorityID] INT            NOT NULL,
    [DefectStatusID]   INT            NOT NULL,
    [CreationDate]     DATETIME       NOT NULL DEFAULT SYSUTCDATETIME(),
    [ModificationDate] DATETIME       NULL,
    [Description]      NVARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([DefectID] ASC),
    CONSTRAINT [FK_Defects_Priorities] FOREIGN KEY ([DefectPriorityID]) REFERENCES [dbo].[DefectPriorities] ([DefectPriorityID]),
    CONSTRAINT [FK_Defects_Projects] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Projects] ([ProjectID]),
    CONSTRAINT [FK_Defects_Statuses] FOREIGN KEY ([DefectStatusID]) REFERENCES [dbo].[DefectStatuses] ([DefectStatusID]),
    CONSTRAINT [FK_Defects_Users] FOREIGN KEY ([AssigneeUserID]) REFERENCES [dbo].[Users] ([UserID])
);

