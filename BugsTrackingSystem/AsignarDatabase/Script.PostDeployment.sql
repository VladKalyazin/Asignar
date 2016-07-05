use [AsignarDatabase]
go

insert into [dbo].[Roles] ([RoleName])
values 
	('admin'), 
	('common')

go

insert into [dbo].[DefectStatuses] ([StatusName])
values
	('Open'),
	('In progress'),
	('Done'),
	('In testing'),
	('Closed')

go

insert into [dbo].[DefectPriorities] ([PriorityName])
values
	('Critical'),
	('Major'),
	('Minor')

go

insert into [dbo].[Users] (
	[FirstName],
	[Surname],
	[Email],
	[Password],
	[RoleID],
	[RegistrationDate]
)
values
	('Vlad', 'Kalyazin', '999vlad@gmail.com', '123', (select [RoleID] from [dbo].[Roles] where [RoleName] = 'admin'), SYSDATETIME()),
	('George', 'Orwell', 'orwell444@gmail.com', '123', (select [RoleID] from [dbo].[Roles] where [RoleName] = 'common'), SYSDATETIME()),
	('Daniel', 'Brown', 'danielbrown@gmail.com', '123', (select [RoleID] from [dbo].[Roles] where [RoleName] = 'common'), SYSDATETIME()),
	('John', 'Miller', 'jmiller@gmail.com', '123', (select [RoleID] from [dbo].[Roles] where [RoleName] = 'common'), SYSDATETIME()),
	('Steven', 'Wilson', 'wilsteve@gmail.com', '123', (select [RoleID] from [dbo].[Roles] where [RoleName] = 'common'), SYSDATETIME())

go

insert into [dbo].[Projects] (
	[ProjectName],
	[Prefix],
	[CreationDate],
	[Deleted]
)
values
	('The Titan', 'TTD', SYSDATETIME(), 0),
	('The Financier', 'TDF', SYSDATETIME(), 0),
	('The Stoic', 'TSD', SYSDATETIME(), 0),
	('The Property', 'TPR', SYSDATETIME(), 0),
	('Hobbit', 'JTH', SYSDATETIME(), 0),
	('Cloak', 'CLK', SYSDATETIME(), 0),
	('Switcher', 'SWR', SYSDATETIME(), 0),
	('Wrapper', 'WRP', SYSDATETIME(), 0),
	('Book', 'BUK', SYSDATETIME(), 0),
	('Customer', 'CST', SYSDATETIME(), 0),
	('Crop', 'CRP', SYSDATETIME(), 0),
	('Smile', 'SML', SYSDATETIME(), 0),
	('Bridge', 'BRG', SYSDATETIME(), 0),
	('Film', 'FLM', SYSDATETIME(), 0),
	('Implementation', 'IMP', SYSDATETIME(), 0),
	('Risk', 'Rsk', SYSDATETIME(), 0),
	('Lightning', 'LGH', SYSDATETIME(), 0),
	('The Past Times', 'TPT', SYSDATETIME(), 0),
	('Mistery', 'MST', SYSDATETIME(), 0),
	('Test', 'TST', SYSDATETIME(), 0),
	('Mitt', 'MIT', SYSDATETIME(), 0)

go

insert into [dbo].[ProjectsToUsersBindings] ([ProjectID], [UserID])
	select top 40 p.[ProjectID], u.[UserID]
	from [dbo].[Projects] as p cross join
		[dbo].[Users] as u
	order by NEWID()

go
