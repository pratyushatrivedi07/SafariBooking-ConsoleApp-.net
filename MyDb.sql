
CREATE Table  Users (
    UserId int Identity primary key not null,
     UserName varchar(30) not null ,
    Password varchar(255) not null,
	Name varchar(30) not null,
	RoleId int not null Foreign Key REFERENCES  Role(Id)
); 

select * from Users


Create Table Role(
	Id int identity primary key not null,
	Name nvarchar(20) not null
);
select  * from Role


Create Table Parks(
	ParkId int identity(101,1) primary key not null,
	Name nvarchar(20) not null,
	Location nvarchar(50) not null,
	Fee money not null
);

select * from Parks
delete from Parks where ParkId = 102


CREATE TABLE  Gate (
	GateId int identity(200,1) primary key not null,
    Name varchar(30) not null ,
    ParkId int not null

	CONSTRAINT FK_ParkID FOREIGN KEY(ParkId)
	REFERENCES Parks(ParkId)
);
select * from Gate


CREATE TABLE  Vehicle (
    VId int identity(300,1) primary key not null,
    VType varchar(30) not null ,
	Name nvarchar(30) not null,
    EntryCost money  null,
	Capacity nvarchar(20) null,      
	ParkId int not null Foreign Key REFERENCES  Parks(ParkId)
);

select * from Vehicle


CREATE TABLE  SafariDetail (
    SafariId int identity(2000,1) primary key not null,
    SafariName varchar(50) not null,
	SafariDate Date  not null,
	SafariTime nvarchar(50) not null,
	ParkId int not null  Foreign key  REFERENCES  Parks(ParkId),
	SafariCost Decimal not null 
);

select * from SafariDetail


Create Table Tourist(
	Id int identity(401,1) primary key not null,
	Name varchar(30) not null ,
	Gender varchar(10) not null,
	DateOfBirth Date not null,
	MobileNo nvarchar(10) not null,
	City varchar(20) not null, 
	Country varchar(30) not null,
	EmailId nvarchar(30) not null,
	IdentityName nvarchar(20) not null,
	IdentityNumber nvarchar(20) not null,		 
);

select * from Tourist
delete from Tourist


CREATE TABLE  IdentityProof (
    IdentityId int identity(11,1) primary key  not null,
    IdentityName varchar(50) not null   
);

select * from IdentityProof


Create Table Booking(
	Id int identity primary key not null,
	Status nvarchar(20),
	PId int not null Foreign key  REFERENCES Parks(ParkId),
	SafariId int not null  Foreign key  REFERENCES  SafariDetail(SafariId),
	GateId int not null  Foreign key  REFERENCES  Gate(GateId),
	VehicleId int not null Foreign key  REFERENCES  Vehicle(VId),
	TotalCost money not null 

);

select * from Booking
delete from Booking


--=========================STORED PROCEDURES=========================================
Create or Alter Proc usp_ParkByLocation (@location nvarchar(30))
As
Begin
if(not exists(select Location from Parks where Location = @location))
begin 
throw 50005,'No Parks to show',1;
end
select * from Parks 
where Location = @location
end

exec usp_ParkByLocation Rajasthan



Create or Alter Proc usp_SafariByPark (@ParkId int)
As
Begin
if(not exists(select SafariName from SafariDetail where ParkId = @ParkId))
begin 
throw 50005,'No Safari',1;
end
select * from SafariDetail
where ParkId = @ParkId
end

exec usp_SafariByPark 102



Create or Alter Proc usp_VehicleByPark (@ParkId int)
As
Begin
if(not exists(select Name, Capacity, EntryCost from Vehicle where ParkId = @ParkId and VType like '%Park'))
begin 
throw 50005,'No Vehicles in this Park',1;
end
select VId,Name, Capacity, EntryCost from Vehicle
where ParkId = @ParkId and VType like '%Park'
end

exec usp_VehicleByPark 101


Create or Alter Proc usp_GateByPark (@ParkId int)
As
Begin
if(not exists(select GateId, Name from Gate where ParkId = @ParkId))
begin 
throw 50005,'No Park or Gates',1;
end
select GateId,Name from Gate
where ParkId = @ParkId
end

exec usp_GateByPark 101



create or alter Proc usp_BookingStatus(@Id int)
As
Begin
if(not exists( select b.Id from Booking b where b.Id = @Id))
begin
throw 50005,'No Booking',1;
end
Select b.Id as BookingId, b.Status,  p.Name as Parkname, v.Name as VehicleName, s.SafariName, s.SafariDate as Date, b.TotalCost from Booking b
join Parks p on b.PId = p.ParkId
join Vehicle v on b.VehicleId = v.VId
join SafariDetail s on b.SafariId = s.SafariId
where b.Id = @Id
Group by b.Id, b.Status ,p.Name, v.Name, s.SafariName, s.SafariDate, b.TotalCost
end

exec usp_BookingStatus 13

select * from AspNetUsers
select * from AspNetRoles
select * from AspNetUserRoles

insert into  AspNetRoles values(NEWID(),'Admin','ADMIN',GETDATE())
insert into  AspNetRoles values(NEWID(),'Tourist','TOURIST',GETDATE())

insert into AspNetUserRoles values ('15134bca-9630-47bd-9391-bf8e07ce8294','2464CD9A-BDD6-4D9B-9BDB-1CB3678A2EBC')
insert into AspNetUserRoles values ('e5a51397-8570-4a46-aa87-a929dceb27cf','DB973153-3CCE-4E7E-89FC-A01544677BE5')


