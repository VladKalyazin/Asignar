CREATE TABLE [dbo].[Filters] (
    [FilterID]         INT           IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (30) NOT NULL,
    [UserID] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([FilterID] ASC),
	CONSTRAINT [FK_Filters_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

