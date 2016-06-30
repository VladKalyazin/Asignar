﻿use [AsignarDatabase]
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

insert into [dbo].[Users] (
	[FirstName],
	[Surname],
	[Email],
	[Login],
	[Password],
	[RoleID],
	[RegistrationDate]
)
values
	('Vlad', 'Kalyazin', '999vlad@gmail.com', 'admin', '123', (select [RoleID] from [dbo].[Roles] where [RoleName] = 'admin'), SYSDATETIME()),
	('George', 'Orwell', 'orwell444@gmail.com', 'orw', '123', (select [RoleID] from [dbo].[Roles] where [RoleName] = 'common'), SYSDATETIME()),
	('Daniel', 'Brown', 'danielbrown@gmail.com', 'dan', '123', (select [RoleID] from [dbo].[Roles] where [RoleName] = 'common'), SYSDATETIME()),
	('John', 'Miller', 'jmiller@gmail.com', 'mill', '123', (select [RoleID] from [dbo].[Roles] where [RoleName] = 'common'), SYSDATETIME()),
	('Steven', 'Wilson', 'wilsteve@gmail.com', 'steve', '123', (select [RoleID] from [dbo].[Roles] where [RoleName] = 'common'), SYSDATETIME())

go

insert into [dbo].[Projects] (
	[ProjectName],
	[Prefix],
	[CreationDate]
)
values
	('The Titan', 'TTD', SYSDATETIME()),
	('The Financier', 'TDF', SYSDATETIME()),
	('The Stoic', 'TSD', SYSDATETIME()),
	('The Property', 'TPR', SYSDATETIME()),
	('Hobbit', 'JTH', SYSDATETIME()),
	('Cloak', 'CLK', SYSDATETIME()),
	('Switcher', 'SWR', SYSDATETIME()),
	('Wrapper', 'WRP', SYSDATETIME()),
	('Book', 'BUK', SYSDATETIME()),
	('Customer', 'CST', SYSDATETIME()),
	('Crop', 'CRP', SYSDATETIME()),
	('Smile', 'SML', SYSDATETIME()),
	('Bridge', 'BRG', SYSDATETIME()),
	('Film', 'FLM', SYSDATETIME()),
	('Implementation', 'IMP', SYSDATETIME()),
	('Risk', 'Rsk', SYSDATETIME()),
	('Lightning', 'LGH', SYSDATETIME()),
	('The Past Times', 'TPT', SYSDATETIME()),
	('Mistery', 'MST', SYSDATETIME())

go