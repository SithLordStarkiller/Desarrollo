USE Sql server 2008
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_LogLogger_LogCatTipoLog') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE LogLogger DROP CONSTRAINT FK_LogLogger_LogCatTipoLog
;



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('LogCatTipoLog') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE LogCatTipoLog
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('LogLogger') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE LogLogger
;


CREATE TABLE LogCatTipoLog ( 
	IdTipoLog int NOT NULL,
	TipoLog varchar(50) NOT NULL,
	Descripcion varchar(300),
	Borrado bit
)
;

CREATE TABLE LogLogger ( 
	IdLog bigint identity(1,1)  NOT NULL,
	IdTipoLog int,
	Proyecto varchar(max) NOT NULL,
	Clase varchar(max) NOT NULL,
	Metodo varchar(max),
	Mensage varchar(max),
	Log varchar(max),
	Excepcion varchar(max),
	Auxiliar varchar(max),
	FechaCreacion datetime NOT NULL
)
;


ALTER TABLE LogCatTipoLog ADD CONSTRAINT PK_LogCatTipoLog 
	PRIMARY KEY CLUSTERED (IdTipoLog)
;

ALTER TABLE LogLogger ADD CONSTRAINT PK_LogLogger 
	PRIMARY KEY CLUSTERED (IdLog)
;



ALTER TABLE LogLogger ADD CONSTRAINT FK_LogLogger_LogCatTipoLog 
	FOREIGN KEY (IdTipoLog) REFERENCES LogCatTipoLog (IdTipoLog)
;
