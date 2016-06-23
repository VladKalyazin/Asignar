CREATE TABLE [dbo].[Roles] (
    [RoleID] INT           IDENTITY (1, 1) NOT NULL,
    [Role]   NVARCHAR (15) NULL,
    PRIMARY KEY CLUSTERED ([RoleID] ASC)
);

