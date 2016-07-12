CREATE TABLE [dbo].[Users] (
    [UserID]    INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (30) NOT NULL,
    [Surname]   NVARCHAR (30) NOT NULL,
    [Email]     NVARCHAR (35) NOT NULL unique,
    [Password]  NVARCHAR (100) NOT NULL,
    [RoleID]    INT           NOT NULL,
    [RegistrationDate] DATETIME2 NOT NULL , 
    [PhotoLink] NVARCHAR(250) NULL, 
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles] ([RoleID])
);

