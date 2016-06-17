create table [dbo].[Roles] (
	[RoleID] int identity(1, 1) primary key not null,
	[Role] nvarchar(100)
)

go

create table [dbo].[Users] (
	[UserID] int identity(1, 1) primary key not null,
	[FirstName] nvarchar(100),
	[Surname] nvarchar(100),
	[Login] nvarchar(100),
	[Email] nvarchar(100),
	[Password] nvarchar(100),
	[RoleID] int,
	CONSTRAINT FK_Users_Roles FOREIGN KEY ([RoleID])     
		REFERENCES [Roles] ([RoleID])      
)

go

create table [dbo].[Projects] (
	[ProjectID] int identity(1, 1) primary key not null,
	[ProjectName] nvarchar(100),
	[Prefix] nvarchar(100),
	[CreationDate] datetime
)

go

create table [dbo].[ProjectsToUsersBindings] (
	[BindingID] int identity(1, 1) primary key not null,
	[ProjectID] int,
	[UserID] int,
	CONSTRAINT FK_Bindings_Projects FOREIGN KEY ([ProjectID])     
		REFERENCES [Projects] ([ProjectID]),
	CONSTRAINT FK_Bindings_Users FOREIGN KEY ([UserID])     
		REFERENCES [Users] ([UserID])    
)

go

create table [dbo].[DefectPriorities] (
	[DefectPriorityID] int identity(1, 1) primary key not null,
	[PriorityName] nvarchar(100)
)

go

create table [dbo].[DefectStatuses] (
	[DefectStatusID] int identity(1, 1) primary key not null,
	[StatusName] nvarchar(100)
)

go

create table [dbo].[Defects] (
	[DefectID] int identity(1, 1) primary key not null,
	[Subject] nvarchar(100),
	[ProjectID] int,
	[AssigneeUserID] int,
	[DefectPriorityID] int,
	[DefectStatusID] int,
	[CreationDate] datetime,
	[ModificationDate] datetime,
	[Description] nvarchar(1000),
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

create table [dbo].[DefectAttachments] (
	[AttachmentID] int identity(1, 1) primary key not null,
	[AttachmentLink] nvarchar(200),
	[DefectID] int,
	CONSTRAINT FK_DefectAttachments_Defects FOREIGN KEY ([DefectID])     
		REFERENCES [Defects] ([DefectID])   
)

go

create table [dbo].[Comments] (
	[CommentID] int identity(1, 1) primary key not null,
	[ProjectID] int,
	[UserID] int,
	[CommentText] nvarchar(1000),
	CONSTRAINT FK_Comments_Projects FOREIGN KEY ([ProjectID])     
		REFERENCES [Projects] ([ProjectID]),
	CONSTRAINT FK_Comments_Users FOREIGN KEY ([UserID])     
		REFERENCES [Users] ([UserID])   
)

go

create table [dbo].[Filters] (
	[FilterID] int identity(1, 1) primary key not null,
	[Search] nvarchar(100),
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
