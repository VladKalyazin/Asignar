CREATE TABLE [dbo].[DefectStatuses] (
    [DefectStatusID] INT           IDENTITY (1, 1) NOT NULL,
    [StatusName]     NVARCHAR (15) NOT NULL,
    PRIMARY KEY CLUSTERED ([DefectStatusID] ASC)
);

