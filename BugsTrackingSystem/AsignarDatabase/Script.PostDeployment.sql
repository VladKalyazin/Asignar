use [AsignarDatabase]
go

insert into [dbo].[Roles] ([Role])
values 
	('admin'), 
	('common')

go

insert into [dbo].[Users] (
	[FirstName],
	[Surname],
	[Email],
	[Login],
	[Password],
	[RoleID]
)
values
	('Vlad', 'Kalyazin', '999vlad@gmail.com', 'admin', '123', (select [RoleID] from [dbo].[Roles] where [Role] = 'admin'))

go
