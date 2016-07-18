CREATE TABLE [dbo].[FilterStatuses]
(
	[FilterID] INT NOT NULL , 
    [StatusID] INT NOT NULL, 
	CONSTRAINT [FK_FilterToStatusesBindings_Filters] FOREIGN KEY ([FilterID]) REFERENCES [dbo].[Filters] ([FilterID])
		ON DELETE CASCADE,
    CONSTRAINT [FK_FilterToStatusesBindings_Statuses] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[DefectStatuses] ([DefectStatusID])
		ON DELETE CASCADE, 
    CONSTRAINT [PK_FilterStatuses] PRIMARY KEY ([FilterID], [StatusID])
)
