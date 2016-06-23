CREATE TABLE [dbo].[Projects] (
    [ProjectID]    INT           IDENTITY (1, 1) NOT NULL,
    [ProjectName]  NVARCHAR (30) NULL,
    [Prefix]       NCHAR (3)     NULL,
    [CreationDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([ProjectID] ASC)
);

