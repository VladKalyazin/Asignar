CREATE TABLE [dbo].[Projects] (
    [ProjectID]    INT           IDENTITY (1, 1) NOT NULL,
    [ProjectName]  NVARCHAR (30) NOT NULL,
    [Prefix]       NCHAR (3)     NOT NULL,
    [CreationDate] DATETIME2      NOT NULL ,
    [Deleted] BIT NOT NULL, 
    PRIMARY KEY CLUSTERED ([ProjectID] ASC)
);

