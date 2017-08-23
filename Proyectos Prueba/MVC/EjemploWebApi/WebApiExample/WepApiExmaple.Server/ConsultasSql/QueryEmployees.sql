CREATE TABLE Employees
(
	Id int primary key identity,
	FirstName Varchar(50),
	LastName Varchar(50),
	Gender Varchar(1),
	Salary decimal (10,2)
)
GO

INSERT INTO Employees values ('Marco','Perez','M',59.02)
INSERT INTO Employees values ('Fernanda','Alonso','F',1559.02)
INSERT INTO Employees values ('Carlos','Robles','M',1759.02)
INSERT INTO Employees values ('Karla','Lima','F',5259.02)
INSERT INTO Employees values ('Sara','Lopez','F',6959.02)