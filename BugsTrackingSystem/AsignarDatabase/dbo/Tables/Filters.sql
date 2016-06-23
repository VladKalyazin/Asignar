CREATE TABLE [dbo].[Filters] (
    [FilterID]         INT           IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (30) NULL,
    [ProjectID]        INT           NULL,
    [AssigneeUserID]   INT           NULL,
    [DefectPriorityID] INT           NULL,
    [DefectStatusID]   INT           NULL,
    PRIMARY KEY CLUSTERED ([FilterID] ASC),
    CONSTRAINT [FK_Filters_Priorities] FOREIGN KEY ([DefectPriorityID]) REFERENCES [dbo].[DefectPriorities] ([DefectPriorityID]),
    CONSTRAINT [FK_Filters_Projects] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Projects] ([ProjectID]),
    CONSTRAINT [FK_Filters_Statuses] FOREIGN KEY ([DefectStatusID]) REFERENCES [dbo].[DefectStatuses] ([DefectStatusID]),
    CONSTRAINT [FK_Filters_Users] FOREIGN KEY ([AssigneeUserID]) REFERENCES [dbo].[Users] ([UserID])
);

