IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_DirCatColonias_DirCatEstados') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE DirCatColonias DROP CONSTRAINT FK_DirCatColonias_DirCatEstados
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_DirDirecciones_DirCatEstados') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE DirDirecciones DROP CONSTRAINT FK_DirDirecciones_DirCatEstados
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_LogLogger_LogCatTipoLog') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE LogLogger DROP CONSTRAINT FK_LogLogger_LogCatTipoLog
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_OpeEstados_OpeCatTipoTicket') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE OpeEstados DROP CONSTRAINT FK_OpeEstados_OpeCatTipoTicket
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_OpeZonasPorEstados_DirCatEstados') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE OpeZonasPorEstados DROP CONSTRAINT FK_OpeZonasPorEstados_DirCatEstados
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_OpeZonasPorEstados_OpeCatZonas') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE OpeZonasPorEstados DROP CONSTRAINT FK_OpeZonasPorEstados_OpeCatZonas
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_UsUsuarios_UsCatNivelUsuario') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE UsUsuarios DROP CONSTRAINT FK_UsUsuarios_UsCatNivelUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_UsUsuarios_UsEstatusUsuario') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE UsUsuarios DROP CONSTRAINT FK_UsUsuarios_UsEstatusUsuario
;



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Usp_LogAlmacenaLog'))
DROP PROCEDURE Usp_LogAlmacenaLog
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('DirCatColonias') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE DirCatColonias
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('DirCatEstados') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE DirCatEstados
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('DirCatMunicipios') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE DirCatMunicipios
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('DirDirecciones') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE DirDirecciones
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('LogCatTipoLog') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE LogCatTipoLog
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('LogLogger') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE LogLogger
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('OpeCatTipoTicket') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE OpeCatTipoTicket
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('OpeCatZonas') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE OpeCatZonas
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('OpeEstados') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE OpeEstados
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('OpeZonasPorEstados') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE OpeZonasPorEstados
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsCatNivelUsuario') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsCatNivelUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsCatTipoUsuario') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsCatTipoUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsEstatusUsuario') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsEstatusUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsUsuarios') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsUsuarios
;


CREATE TABLE DirCatColonias ( 
	IdColonia smallint NOT NULL,
	IdEstado smallint NOT NULL,
	IdMunicipio smallint NOT NULL,
	NombreColonia varchar(50) NOT NULL,
	CodigoPostal varchar(5) NOT NULL,
	ClaveColonia varchar(50) NOT NULL,
	Borrado bit NOT NULL
)
;

CREATE TABLE DirCatEstados ( 
	IdEstado smallint NOT NULL,
	NombreOficial varchar(50) NOT NULL,
	NombreNoOficial varchar(50) NOT NULL,
	ClaveEstado varchar(2) NOT NULL,
	Borrado bit NOT NULL
)
;

CREATE TABLE DirCatMunicipios ( 
	IdCatMunicipio smallint NOT NULL,
	IdMunicipio smallint NOT NULL,
	IdEstado smallint NOT NULL,
	NombreOficial varchar(50) NOT NULL,
	NombreCiudad varchar(50) NOT NULL,
	ClaveMunicipio varchar(4) NOT NULL,
	Borrado bit NOT NULL
)
;

CREATE TABLE DirDirecciones ( 
	IdDireccion int identity(1,1)  NOT NULL,
	IdEstado smallint NOT NULL,
	IdMunicipio smallint NOT NULL,
	NombreColonia varchar(50) NOT NULL,
	CodigoPostal varchar(5) NOT NULL,
	Calle varchar(300) NOT NULL,
	NumeroExterior varchar(30) NULL,
	NumeroInterior varchar(30) NOT NULL,
	Referencias varchar(700) NULL,
	Borrado bit NULL
)
;

CREATE TABLE LogCatTipoLog ( 
	IdTipoLog int NOT NULL,
	TipoLog varchar(50) NOT NULL,
	Descripcion varchar(300) NULL,
	Borrado bit NULL
)
;

CREATE TABLE LogLogger ( 
	IdLog bigint identity(1,1)  NOT NULL,
	IdTipoLog int NULL,
	Proyecto varchar(max) NOT NULL,
	Clase varchar(max) NOT NULL,
	Metodo varchar(max) NULL,
	Mensage varchar(max) NULL,
	Log varchar(max) NULL,
	Excepcion varchar(max) NULL,
	Auxiliar varchar(max) NULL,
	FechaCreacion datetime NOT NULL
)
;

CREATE TABLE OpeCatTipoTicket ( 
	IdTipoTicket int NOT NULL,
	TipoTicket varchar(50) NOT NULL,
	Descripcion varchar(200) NULL,
	Borrado bit NOT NULL
)
;

CREATE TABLE OpeCatZonas ( 
	IdZona smallint NOT NULL,
	NombreZona varchar(100) NOT NULL,
	Descripcion varchar(300) NULL,
	Borrado bit NOT NULL
)
;

CREATE TABLE OpeEstados ( 
	IdEstado int NOT NULL,
	IdTipoTicket int NULL,
	Estado varchar(50) NOT NULL,
	Descripcion varchar(200) NULL,
	Borrado bit NOT NULL
)
;

CREATE TABLE OpeZonasPorEstados ( 
	IdZonasEstados int identity(1,1)  NOT NULL,
	IdEstado smallint NULL,
	IdZona smallint NULL,
	Borrado bit NULL
)
;

CREATE TABLE UsCatNivelUsuario ( 
	IdNivelUsuario int NOT NULL,
	NivelUsuario varchar(50) NOT NULL,
	Descripcion varchar(200) NULL,
	Borrado bit NOT NULL
)
;

CREATE TABLE UsCatTipoUsuario ( 
	IdTipoUsuario int NOT NULL,
	TipoUsuario varchar(50) NOT NULL,
	Descripcion varchar(200) NULL,
	Borrado bit NOT NULL
)
;

CREATE TABLE UsEstatusUsuario ( 
	IdEstatusUsuario int NOT NULL,
	EstatusUsuario varchar(50) NOT NULL,
	Descripcion varchar(200) NULL,
	Borrado bit NOT NULL
)
;

CREATE TABLE UsUsuarios ( 
	idUsuarios int identity(1,1)  NOT NULL,
	IdTipoUsuario int NULL,
	IdNivelUsuario int NULL,
	IdEstatusUsuario int NULL,
	Usuario varchar(50) NULL,
	Contrasena varchar(50) NOT NULL,
	Borrado bit NOT NULL
)
;


ALTER TABLE DirCatMunicipios
	ADD CONSTRAINT UQ_DirCatMunicipios_IdMunicipio UNIQUE (IdMunicipio)
;

ALTER TABLE DirCatColonias ADD CONSTRAINT PK_DirCatColonias 
	PRIMARY KEY CLUSTERED (IdColonia)
;

ALTER TABLE DirCatEstados ADD CONSTRAINT PK_DirCatEstados 
	PRIMARY KEY CLUSTERED (IdEstado)
;

ALTER TABLE DirCatMunicipios ADD CONSTRAINT PK_DirCatMunicipios 
	PRIMARY KEY CLUSTERED (IdCatMunicipio)
;

ALTER TABLE DirDirecciones ADD CONSTRAINT PK_DirDirecciones 
	PRIMARY KEY CLUSTERED (IdDireccion)
;

ALTER TABLE LogCatTipoLog ADD CONSTRAINT PK_LogCatTipoLog 
	PRIMARY KEY CLUSTERED (IdTipoLog)
;

ALTER TABLE LogLogger ADD CONSTRAINT PK_LogLogger 
	PRIMARY KEY CLUSTERED (IdLog)
;

ALTER TABLE OpeCatTipoTicket ADD CONSTRAINT PK_OpeCatTipoTicket 
	PRIMARY KEY CLUSTERED (IdTipoTicket)
;

ALTER TABLE OpeCatZonas ADD CONSTRAINT PK_OpeZonas 
	PRIMARY KEY CLUSTERED (IdZona)
;

ALTER TABLE OpeEstados ADD CONSTRAINT PK_OpeEstados 
	PRIMARY KEY CLUSTERED (IdEstado)
;

ALTER TABLE OpeZonasPorEstados ADD CONSTRAINT PK_OpeZonasPorEstados 
	PRIMARY KEY CLUSTERED (IdZonasEstados)
;

ALTER TABLE UsCatNivelUsuario ADD CONSTRAINT PK_UsCatNivelUsuario 
	PRIMARY KEY CLUSTERED (IdNivelUsuario)
;

ALTER TABLE UsCatTipoUsuario ADD CONSTRAINT PK_UsCatTipoUsuario 
	PRIMARY KEY CLUSTERED (IdTipoUsuario)
;

ALTER TABLE UsEstatusUsuario ADD CONSTRAINT PK_UsEstatusUsuario 
	PRIMARY KEY CLUSTERED (IdEstatusUsuario)
;

ALTER TABLE UsUsuarios ADD CONSTRAINT PK_UsUsuarios 
	PRIMARY KEY CLUSTERED (idUsuarios)
;



ALTER TABLE DirCatColonias ADD CONSTRAINT FK_DirCatColonias_DirCatEstados 
	FOREIGN KEY (IdEstado) REFERENCES DirCatEstados (IdEstado)
;

ALTER TABLE DirDirecciones ADD CONSTRAINT FK_DirDirecciones_DirCatEstados 
	FOREIGN KEY (IdEstado) REFERENCES DirCatEstados (IdEstado)
;

ALTER TABLE LogLogger ADD CONSTRAINT FK_LogLogger_LogCatTipoLog 
	FOREIGN KEY (IdTipoLog) REFERENCES LogCatTipoLog (IdTipoLog)
;

ALTER TABLE OpeEstados ADD CONSTRAINT FK_OpeEstados_OpeCatTipoTicket 
	FOREIGN KEY (IdTipoTicket) REFERENCES OpeCatTipoTicket (IdTipoTicket)
;

ALTER TABLE OpeZonasPorEstados ADD CONSTRAINT FK_OpeZonasPorEstados_DirCatEstados 
	FOREIGN KEY (IdEstado) REFERENCES DirCatEstados (IdEstado)
;

ALTER TABLE OpeZonasPorEstados ADD CONSTRAINT FK_OpeZonasPorEstados_OpeCatZonas 
	FOREIGN KEY (IdZona) REFERENCES OpeCatZonas (IdZona)
;

ALTER TABLE UsUsuarios ADD CONSTRAINT FK_UsUsuarios_UsCatNivelUsuario 
	FOREIGN KEY (IdNivelUsuario) REFERENCES UsCatNivelUsuario (IdNivelUsuario)
;

ALTER TABLE UsUsuarios ADD CONSTRAINT FK_UsUsuarios_UsEstatusUsuario 
	FOREIGN KEY (IdEstatusUsuario) REFERENCES UsEstatusUsuario (IdEstatusUsuario)
;





-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 09/05/2017
-- Description:	Se encargara de almacenar un Log
-- =============================================
CREATE PROCEDURE [dbo].[Usp_LogAlmacenaLog] 
	@tipoLog int,
    @proyecto VarChar(MAX),
    @clase VarChar(MAX),
    @metodo VarChar(MAX),
    @mensage varchar(MAX),
    @log VarChar(MAX),
    @excepcion varchar(MAX),
    @auxiliar VarChar(MAX)
AS
BEGIN

	INSERT INTO LOGLOGGER (IdTipoLog, Proyecto, Clase, Metodo,Mensage,Log,Excepcion,Auxiliar,FechaCreacion)  
	VALUES (@tipoLog , @proyecto, @clase,@metodo,@mensage,@log ,@Excepcion,@auxiliar, GETDATE());

	SELECT @@IDENTITY
END

;
