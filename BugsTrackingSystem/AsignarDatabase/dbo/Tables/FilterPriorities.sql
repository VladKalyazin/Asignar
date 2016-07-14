CREATE TABLE [dbo].[FilterPriorities]
(
	[FilterID] INT NOT NULL , 
    [PriorityID] INT NOT NULL, 
	CONSTRAINT [FK_FilterToPrioritiesBindings_Filters] FOREIGN KEY ([FilterID]) REFERENCES [dbo].[Filters] ([FilterID]),
    CONSTRAINT [FK_FilterToPrioritiesBindings_Projects] FOREIGN KEY ([PriorityID]) REFERENCES [dbo].[DefectPriorities] ([DefectPriorityID]), 
    CONSTRAINT [PK_FilterPriorities] PRIMARY KEY ([FilterID], [PriorityID])
)
