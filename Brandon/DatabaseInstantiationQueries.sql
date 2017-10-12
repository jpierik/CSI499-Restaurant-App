use csi4999

drop table StatusHistory
drop table CurrentStatus
drop table WaitingParty
drop table Users
drop table Seatings
drop table Restaurant

create table Restaurant (RestaurantId int primary key identity(1,1), address1 varchar(50), Name varchar(50), NoOfTables int)
go

create table Seatings (TableNumber int, RestaurantID int foreign key references Restaurant, maxOccupancy int,
	primary key (TableNumber, RestaurantId))

create table Users ( UserId int primary key identity(1,1), RestaurantID int foreign key references
Restaurant, username varchar(16), alevel int not null default 0, pwd varchar(16), email varchar(100))

create table WaitingParty(PartyId int primary key identity(1,1), RestaurantID int foreign key references
Restaurant, NoOfGuests int not null)

create table CurrentStatus(StatusId int primary key identity(1,1), TableID int,
RestaurantId int, SatDate datetime, NoOfOccupants int not null, foreign key (TableID, RestaurantID) references Seatings)

create table StatusHistory(HistoryId int primary key identity(1,1), TableID int, RestaurantID int, SatDate datetime, ClearDate datetime,
foreign key (TableID, RestaurantID) references Seatings)