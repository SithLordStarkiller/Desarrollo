-- ===============================================================================================================================================
-- Autor:					Esteban Cruz Lagunes
-- Solucion:				Suncorp
-- Modulo:					ETLCargarCEPOMEX
-- Nombre:					Limpiar
-- Fecha de creacion:		18/05/2017
-- Fecha de Modificacion:	Ninguna
--
-- Descripcion:	Se encarga de realizar un respaldo de la tabla de DirDirecciones y eliminar las tablas que se relacionan 
--			   con el modulo de DirDirecciones
-- ================================================================================================================================================

CREATE TABLE DirDireccionesR (
   IdDireccion          INT                  ,
   IdEstado             INT                 null,
   IdMunicipio          INT                 null,
   NombreColonia        VARCHAR(50)         null,
   CodigoPostal         VARCHAR(5)          null,
   Calle                VARCHAR(300)        null,
   NumeroExterior       VARCHAR(30)         null,
   NumeroInterio        VARCHAR(30)         null,
   Referencia           VARCHAR(700)        null,
   Borrado              BIT					null
)

INSERT INTO DirDireccionesR(
	IdDireccion,
    IdEstado,
    IdMunicipio,
    NombreColonia,
    CodigoPostal,
    Calle,
    NumeroExterior,
    NumeroInterio,
    Referencia,
    Borrado
	)
SELECT 
	IdDireccion,
    IdEstado,
    IdMunicipio,
    NombreColonia,
    CodigoPostal,
    Calle,
    NumeroInterior,
    NumeroInterior,
    Referencias,
    Borrado
FROM DirDirecciones

DELETE FROM DirDirecciones
DELETE FROM CEPOMEX
DELETE FROM DirCatColonias
DELETE FROM DirCatMunicipios
DELETE FROM DirCatEstados