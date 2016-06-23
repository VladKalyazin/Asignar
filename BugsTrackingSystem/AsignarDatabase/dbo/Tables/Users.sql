CREATE TABLE [dbo].[Users] (
    [UserID]    INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (30) NULL,
    [Surname]   NVARCHAR (30) NULL,
    [Login]     NVARCHAR (20) NULL,
    [Email]     NVARCHAR (35) NULL,
    [Password]  NVARCHAR (20) NULL,
    [RoleID]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles] ([RoleID])
);

