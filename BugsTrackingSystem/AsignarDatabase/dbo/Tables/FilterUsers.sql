CREATE TABLE [dbo].[FilterUsers]
(
	[FilterID] INT NOT NULL , 
    [UserID] INT NOT NULL, 
	CONSTRAINT [FK_FilterToUsersBindings_Filters] FOREIGN KEY ([FilterID]) REFERENCES [dbo].[Filters] ([FilterID]),
    CONSTRAINT [FK_FilterToUsersBindings_Projects] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]), 
    CONSTRAINT [PK_FilterUsers] PRIMARY KEY ([FilterID], [UserID])
)
