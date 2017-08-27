CREATE DATABASE CertificacionMvc;
GO

USE CertificacionMvc;
GO

CREATE TABLE Departamentos
(
	IdDepartamento int primary key,
	Deparatamento Varchar(50)
)

CREATE TABLE Empleados
(
	IdEmpleado int primary key identity,
	Nombre Varchar(50),
	Apellido Varchar(50),
	Genero Varchar(10),
	FechaIngreso DateTime,
	Salario decimal (10,2),
	Departamento int
)

