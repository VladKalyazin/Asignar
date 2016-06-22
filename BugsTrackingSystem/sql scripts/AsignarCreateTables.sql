create database [AsignarDB]

go

create table [AsignarDB].[dbo].[Roles] (
	[RoleID] int identity(1, 1) primary key not null,
	[Role] nvarchar(15)
)

go

create table [AsignarDB].[dbo].[Users] (
	[UserID] int identity(1, 1) primary key not null,
	[FirstName] nvarchar(30),
	[Surname] nvarchar(30),
	[Login] nvarchar(20),
	[Email] nvarchar(35),
	[Password] nvarchar(20),
	[RoleID] int,
	CONSTRAINT FK_Users_Roles FOREIGN KEY ([RoleID])     
		REFERENCES [Roles] ([RoleID])      
)

go

create table [AsignarDB].[dbo].[Projects] (
	[ProjectID] int identity(1, 1) primary key not null,
	[ProjectName] nvarchar(30),
	[Prefix] nchar(3),
	[CreationDate] datetime
)

go

create table [AsignarDB].[dbo].[ProjectsToUsersBindings] (
	[BindingID] int identity(1, 1) primary key not null,
	[ProjectID] int,
	[UserID] int,
	CONSTRAINT FK_Bindings_Projects FOREIGN KEY ([ProjectID])     
		REFERENCES [Projects] ([ProjectID]),
	CONSTRAINT FK_Bindings_Users FOREIGN KEY ([UserID])     
		REFERENCES [Users] ([UserID])    
)

go

create table [AsignarDB].[dbo].[DefectPriorities] (
	[DefectPriorityID] int identity(1, 1) primary key not null,
	[PriorityName] nvarchar(15)
)

go

create table [AsignarDB].[dbo].[DefectStatuses] (
	[DefectStatusID] int identity(1, 1) primary key not null,
	[StatusName] nvarchar(15)
)

go

create table [AsignarDB].[dbo].[Defects] (
	[DefectID] int identity(1, 1) primary key not null,
	[Subject] nvarchar(50),
	[ProjectID] int,
	[AssigneeUserID] int,
	[DefectPriorityID] int,
	[DefectStatusID] int,
	[CreationDate] datetime,
	[ModificationDate] datetime,
	[Description] nvarchar(200),
	CONSTRAINT FK_Defects_Projects FOREIGN KEY ([ProjectID])     
		REFERENCES [Projects] ([ProjectID]),
	CONSTRAINT FK_Defects_Users FOREIGN KEY ([AssigneeUserID])     
		REFERENCES [Users] ([UserID]),
	CONSTRAINT FK_Defects_Priorities FOREIGN KEY ([DefectPriorityID])     
		REFERENCES [DefectPriorities] ([DefectPriorityID]),   
	CONSTRAINT FK_Defects_Statuses FOREIGN KEY ([DefectStatusID])     
		REFERENCES [DefectStatuses] ([DefectStatusID]) 
)

go

create table [AsignarDB].[dbo].[DefectAttachments] (
	[AttachmentID] int identity(1, 1) primary key not null,
	[AttachmentLink] nvarchar(100),
	[DefectID] int,
	CONSTRAINT FK_DefectAttachments_Defects FOREIGN KEY ([DefectID])     
		REFERENCES [Defects] ([DefectID])   
)

go

create table [AsignarDB].[dbo].[Comments] (
	[CommentID] int identity(1, 1) primary key not null,
	[DefectID] int,
	[UserID] int,
	[CommentText] nvarchar(500),
	CONSTRAINT FK_Comments_Defects FOREIGN KEY ([DefectID])     
		REFERENCES [Defects] ([DefectID]),
	CONSTRAINT FK_Comments_Users FOREIGN KEY ([UserID])     
		REFERENCES [Users] ([UserID])   
)

go

create table [AsignarDB].[dbo].[Filters] (
	[FilterID] int identity(1, 1) primary key not null,
	[Search] nvarchar(30),
	[ProjectID] int,
	[AssigneeUserID] int,
	[DefectPriorityID] int,
	[DefectStatusID] int,
	CONSTRAINT FK_Filters_Projects FOREIGN KEY ([ProjectID])     
		REFERENCES [Projects] ([ProjectID]),
	CONSTRAINT FK_Filters_Users FOREIGN KEY ([AssigneeUserID])     
		REFERENCES [Users] ([UserID]),
	CONSTRAINT FK_Filters_Priorities FOREIGN KEY ([DefectPriorityID])     
		REFERENCES [DefectPriorities] ([DefectPriorityID]),   
	CONSTRAINT FK_Filters_Statuses FOREIGN KEY ([DefectStatusID])     
		REFERENCES [DefectStatuses] ([DefectStatusID]) 
)

go
