CREATE TABLE [dbo].[ProjectsToUsersBindings] (
    [ProjectID] INT NOT NULL,
    [UserID]    INT NOT NULL,
    CONSTRAINT [FK_Bindings_Projects] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Projects] ([ProjectID]),
    CONSTRAINT [FK_Bindings_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]), 
    CONSTRAINT [PK_ProjectsToUsersBindings] PRIMARY KEY ([ProjectID], [UserID])
);

