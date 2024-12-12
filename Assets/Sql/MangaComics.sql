Create table MangaComics(
	Id int Primary key Identity(1,1) not null,
	Name varchar(255) not null,
	Volume int not null,
	IsRead bit not null,
);

Insert Into MangaComics(Name, Volume, IsRead) Values('berserk', 1, 0)
Insert Into MangaComics(Name, Volume, IsRead) Values('fire force', 10, 1)
