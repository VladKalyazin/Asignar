CREATE TABLE [dbo].[FilterUsers]
(
	[FilterID] INT NOT NULL , 
    [UserID] INT NOT NULL, 
	CONSTRAINT [FK_FilterToUsersBindings_Filters] FOREIGN KEY ([FilterID]) REFERENCES [dbo].[Filters] ([FilterID])
		ON DELETE CASCADE,
    CONSTRAINT [FK_FilterToUsersBindings_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
		ON DELETE CASCADE, 
    CONSTRAINT [PK_FilterUsers] PRIMARY KEY ([FilterID], [UserID])
)
