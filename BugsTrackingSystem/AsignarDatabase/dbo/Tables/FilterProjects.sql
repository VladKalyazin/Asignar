CREATE TABLE [dbo].[FilterProjects]
(
	[FilterID] INT NOT NULL , 
    [ProjectID] INT NOT NULL, 
	CONSTRAINT [FK_FilterToProjectsBindings_Filters] FOREIGN KEY ([FilterID]) REFERENCES [dbo].[Filters] ([FilterID])
		ON DELETE CASCADE,
    CONSTRAINT [FK_FilterToProjectsBindings_Projects] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Projects] ([ProjectID])
		ON DELETE CASCADE, 
    CONSTRAINT [PK_FilterProjects] PRIMARY KEY ([FilterID], [ProjectID])
)
