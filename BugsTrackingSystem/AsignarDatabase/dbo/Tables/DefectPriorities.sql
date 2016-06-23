CREATE TABLE [dbo].[DefectPriorities] (
    [DefectPriorityID] INT           IDENTITY (1, 1) NOT NULL,
    [PriorityName]     NVARCHAR (15) NULL,
    PRIMARY KEY CLUSTERED ([DefectPriorityID] ASC)
);

