CREATE TABLE [dbo].[FilterPriorities]
(
	[FilterID] INT NOT NULL , 
    [PriorityID] INT NOT NULL, 
	CONSTRAINT [FK_FilterToPrioritiesBindings_Filters] FOREIGN KEY ([FilterID]) REFERENCES [dbo].[Filters] ([FilterID])
		ON DELETE CASCADE,
    CONSTRAINT [FK_FilterToPrioritiesBindings_Projects] FOREIGN KEY ([PriorityID]) REFERENCES [dbo].[DefectPriorities] ([DefectPriorityID])
		ON DELETE CASCADE, 
    CONSTRAINT [PK_FilterPriorities] PRIMARY KEY ([FilterID], [PriorityID])
)
