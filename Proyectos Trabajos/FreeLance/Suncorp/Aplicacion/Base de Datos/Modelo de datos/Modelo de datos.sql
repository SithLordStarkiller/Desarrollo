USE Suncorp
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_LogLogger_LogCatTipoLog') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE LogLogger DROP CONSTRAINT FK_LogLogger_LogCatTipoLog
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_UsUsuarios_UsCatNivelUsuario') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE UsUsuarios DROP CONSTRAINT FK_UsUsuarios_UsCatNivelUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_UsUsuarios_UsEstatusUsuario') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE UsUsuarios DROP CONSTRAINT FK_UsUsuarios_UsEstatusUsuario
;



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('LogCatTipoLog') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE LogCatTipoLog
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('LogLogger') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE LogLogger
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
	Proyecto varchar(50) NOT NULL,
	Clase varchar(50) NOT NULL,
	Metodo varchar(50) NULL,
	Mensage varchar(50) NULL,
	Log varchar(max) NULL,
	Excepcion varchar(max) NULL,
	Auxiliar varchar(max) NULL
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


ALTER TABLE LogCatTipoLog ADD CONSTRAINT PK_LogCatTipoLog 
	PRIMARY KEY CLUSTERED (IdTipoLog)
;

ALTER TABLE LogLogger ADD CONSTRAINT PK_LogLogger 
	PRIMARY KEY CLUSTERED (IdLog)
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



ALTER TABLE LogLogger ADD CONSTRAINT FK_LogLogger_LogCatTipoLog 
	FOREIGN KEY (IdTipoLog) REFERENCES LogCatTipoLog (IdTipoLog)
;

ALTER TABLE UsUsuarios ADD CONSTRAINT FK_UsUsuarios_UsCatNivelUsuario 
	FOREIGN KEY (IdNivelUsuario) REFERENCES UsCatNivelUsuario (IdNivelUsuario)
;

ALTER TABLE UsUsuarios ADD CONSTRAINT FK_UsUsuarios_UsEstatusUsuario 
	FOREIGN KEY (IdEstatusUsuario) REFERENCES UsEstatusUsuario (IdEstatusUsuario)
;
