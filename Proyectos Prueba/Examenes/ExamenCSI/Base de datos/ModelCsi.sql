
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_UsUsuario_UsCatTipoUsuario') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE UsUsuario DROP CONSTRAINT FK_UsUsuario_UsCatTipoUsuario
;



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsCatTipoUsuario') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsCatTipoUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsUsuario') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsUsuario
;


CREATE TABLE UsCatTipoUsuario ( 
	IdTipoUsuario int NOT NULL,
	TipoUsuario varchar(50),
	Descripcion varchar(50)
)
;

CREATE TABLE UsUsuario ( 
	IdUsuario int identity(1,1)  NOT NULL,
	IdTipoUsuario int,
	Usuario varchar(50),
	Contrasena varchar(50)
)
;


ALTER TABLE UsCatTipoUsuario ADD CONSTRAINT PK_UsTipoUsuario 
	PRIMARY KEY CLUSTERED (IdTipoUsuario)
;

ALTER TABLE UsUsuario ADD CONSTRAINT PK_UsUsuario 
	PRIMARY KEY CLUSTERED (IdUsuario)
;



ALTER TABLE UsUsuario ADD CONSTRAINT FK_UsUsuario_UsCatTipoUsuario 
	FOREIGN KEY (IdTipoUsuario) REFERENCES UsCatTipoUsuario (IdTipoUsuario)
;
