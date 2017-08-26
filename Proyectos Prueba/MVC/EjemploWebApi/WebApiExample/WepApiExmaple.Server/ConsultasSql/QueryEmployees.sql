CREATE DATABASE WebApiExample

USE WebApiExample

GO

CREATE TABLE Employees
(
	Id int primary key identity,
	FirstName Varchar(50),
	LastName Varchar(50),
	Gender Varchar(1),
	Salary decimal (10,2)
)

CREATE TABLE Users
(
	Id int primary key identity,
	UserName Varchar(50),
	Password Varchar(50)
)
GO

INSERT INTO Employees values ('Marco','Perez','M',59.02)
INSERT INTO Employees values ('Fernanda','Alonso','F',1559.02)
INSERT INTO Employees values ('Carlos','Robles','M',1759.02)
INSERT INTO Employees values ('Karla','Lima','F',5259.02)
INSERT INTO Employees values ('Sara','Lopez','F',6959.02)

INSERT INTO Users values ('Marco','1234')
INSERT INTO Users values ('Fernanda','1234')