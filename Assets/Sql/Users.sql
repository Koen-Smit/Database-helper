Create table Users(
	Id int Primary key Identity(1,1) not null,
	Name varchar(255) not null,
	Age int not null,
	IsActive bit not null,
);

Insert Into Users(Name, Age, IsActive) Values('Karel', 14, 0)
Insert Into Users(Name, Age, IsActive) Values('Ingrid', 42, 1)