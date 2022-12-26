set ansi_padding on
go
set quoted_identifier on
go
set ansi_nulls on
go

create database [alisavetRating]
go

use [alisavetRating] 
go

create table [dbo].[Images]
(
[ID_Image] [int] not null identity(1,1),
[Image] [varbinary] (max) not null,
[Format] [varchar] (30) not null

constraint [PK_Image] primary key clustered ([ID_Image] ASC) on [PRIMARY]
)
go

create table [dbo].[Employee]
(
[ID_Employee] [int] not null identity(1,1),
[Surname] [varchar] (50) not null,
[Name] [varchar] (50) not null,
[MiddleName] [varchar] (50) null,
[Image_ID] [int] null,
[Description] [varchar] (300) null

constraint [PK_Employee] primary key clustered ([ID_Employee] ASC) on [PRIMARY],
constraint [FK_Employee_Image] foreign key ([Image_ID])
references [dbo].[Images]([ID_Image])
)
go

create table [dbo].[Branch]
(
[ID_Branch] [int] not null identity(1,1),
[Name] [varchar] (50) not null,

constraint [PK_Branch] primary key clustered ([ID_Branch] ASC) on [PRIMARY]
)
go

create table [dbo].[Rates]
(
[ID_Rate] [int] not null identity(1,1),
[Employee_ID] [int] not null,
[Branch_ID] [int] not null,
[ReceiptDate] [date] not null,
[Rate] [int] not null

constraint [PK_Rate] primary key clustered ([ID_Rate] ASC) on [PRIMARY],
constraint [FK_Rates_Employee] foreign key ([Employee_ID])
references [dbo].[Employee]([ID_Employee]),
constraint [FK_Rates_Branch] foreign key ([Branch_ID])
references [dbo].[Branch]([ID_Branch])
)
go

create view [dbo].[Employee_show] ("ID", "Фамилия", "Имя", "Отчество", "Описание")
as
select
[dbo].[Employee].[ID_Employee],
[dbo].[Employee].[Surname], [dbo].[Employee].[Name], [dbo].[Employee].[MiddleName],
[dbo].[Employee].[Description]
from [dbo].[Employee]
go

create view [dbo].[Employee_edit] ("ID", "Фамилия", "Имя", "Отчество", "Описание", "Фотография", "ID Фото")
as
select
[dbo].[Employee].[ID_Employee],
[dbo].[Employee].[Surname], [dbo].[Employee].[Name], [dbo].[Employee].[MiddleName],
[dbo].[Employee].[Description],
[dbo].[Images].[Image],
[dbo].[Employee].[Image_ID]
from [dbo].[Employee]
left outer join [dbo].[Images] on [Image_ID] = [ID_Image]
go

create view [dbo].[Employee_show_for_rating] ("ID", "ФИО")
as
select
[dbo].[Employee].[ID_Employee],
[dbo].[Employee].[Surname] + ' ' + [dbo].[Employee].[Name] + ' ' + [dbo].[Employee].[MiddleName]
from [dbo].[Employee]
go

create view [dbo].[Rates_show] ("Дата", "Оценка", "Branch_ID", "Employee_ID")
as
select
convert(varchar, [dbo].[Rates].[ReceiptDate], 104),
[dbo].[Rates].[Rate],
[dbo].[Rates].[Branch_ID],
[dbo].[Rates].[Employee_ID]
from [dbo].[Rates]
go

insert into [dbo].[Branch] ([Name]) values ('Переделкино')
insert into [dbo].[Branch] ([Name]) values ('Бутово')
insert into [dbo].[Branch] ([Name]) values ('Лобачевского')