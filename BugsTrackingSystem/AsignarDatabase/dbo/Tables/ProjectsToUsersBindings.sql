CREATE TABLE [dbo].[ProjectsToUsersBindings] (
    [BindingID] INT IDENTITY (1, 1) NOT NULL,
    [ProjectID] INT NOT NULL,
    [UserID]    INT NOT NULL,
    PRIMARY KEY CLUSTERED ([BindingID] ASC),
    CONSTRAINT [FK_Bindings_Projects] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Projects] ([ProjectID]),
    CONSTRAINT [FK_Bindings_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

