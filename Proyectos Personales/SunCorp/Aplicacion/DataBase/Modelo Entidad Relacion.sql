IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_DirCatColonia_DirCatEstado') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE DirCatColonia DROP CONSTRAINT FK_DirCatColonia_DirCatEstado
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_DirCatColonia_DirCatMunicipio') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE DirCatColonia DROP CONSTRAINT FK_DirCatColonia_DirCatMunicipio
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_DirCatColonia_DirCatTipoAsentamiento') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE DirCatColonia DROP CONSTRAINT FK_DirCatColonia_DirCatTipoAsentamiento
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_DirCatColonia_DirCatTipoZona') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE DirCatColonia DROP CONSTRAINT FK_DirCatColonia_DirCatTipoZona
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_DirDirecciones_DirCatColonia') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE DirDirecciones DROP CONSTRAINT FK_DirDirecciones_DirCatColonia
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_DirDirecciones_DirCatEstado') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE DirDirecciones DROP CONSTRAINT FK_DirDirecciones_DirCatEstado
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_OpEstadosPorUsuarios_UsTipoUsuario') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE OpEstadosPorUsuarios DROP CONSTRAINT FK_OpEstadosPorUsuarios_UsTipoUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ProCatModelo_ProCatMarca') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE ProCatModelo DROP CONSTRAINT FK_ProCatModelo_ProCatMarca
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ProProducto_ProCatMarca') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE ProProducto DROP CONSTRAINT FK_ProProducto_ProCatMarca
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ProProducto_ProCatModelo') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE ProProducto DROP CONSTRAINT FK_ProProducto_ProCatModelo
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ProProducto_ProDiviciones') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE ProProducto DROP CONSTRAINT FK_ProProducto_ProDiviciones
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ProVendedorTienda_ProVendedor') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE ProVendedorTienda DROP CONSTRAINT FK_ProVendedorTienda_ProVendedor
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ProVendedorTienda_TieTienda') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE ProVendedorTienda DROP CONSTRAINT FK_ProVendedorTienda_TieTienda
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ProVenTieProducto_ProProducto') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE ProVenTieProducto DROP CONSTRAINT FK_ProVenTieProducto_ProProducto
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ProVenTieProducto_ProVendedorTienda') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE ProVenTieProducto DROP CONSTRAINT FK_ProVenTieProducto_ProVendedorTienda
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_SisArrbolMenu_SisArrbolMenu') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE SisArbolMenu DROP CONSTRAINT FK_SisArrbolMenu_SisArrbolMenu
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_SisTipoUsuarioPorMenu_SisArbolMenu') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE SisTipoUsuarioPorMenu DROP CONSTRAINT FK_SisTipoUsuarioPorMenu_SisArbolMenu
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_SisTipoUsuarioPorMenu_UsTipoUsuario') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE SisTipoUsuarioPorMenu DROP CONSTRAINT FK_SisTipoUsuarioPorMenu_UsTipoUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_TieSucursal_ConContacto') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE TieSucursal DROP CONSTRAINT FK_TieSucursal_ConContacto
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_TieSucursal_DirDirecciones') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE TieSucursal DROP CONSTRAINT FK_TieSucursal_DirDirecciones
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_TieSucursal_TieTienda') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE TieSucursal DROP CONSTRAINT FK_TieSucursal_TieTienda
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_TieSucursal_UsZona') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE TieSucursal DROP CONSTRAINT FK_TieSucursal_UsZona
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_TieTienda_ConContacto') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE TieTienda DROP CONSTRAINT FK_TieTienda_ConContacto
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_TieTienda_DirDirecciones') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE TieTienda DROP CONSTRAINT FK_TieTienda_DirDirecciones
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_UsUsuarioPorZona_UsUsuarios') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE UsUsuarioPorZona DROP CONSTRAINT FK_UsUsuarioPorZona_UsUsuarios
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_UsUsuarioPorZona_UsZona') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE UsUsuarioPorZona DROP CONSTRAINT FK_UsUsuarioPorZona_UsZona
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_UsUsuarios_UsTipoUsuario') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE UsUsuarios DROP CONSTRAINT FK_UsUsuarios_UsTipoUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_UsZonaPorEstado_DirCatEstado') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE UsZonaPorEstado DROP CONSTRAINT FK_UsZonaPorEstado_DirCatEstado
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_UsZonaPorEstado_UsZona') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE UsZonaPorEstado DROP CONSTRAINT FK_UsZonaPorEstado_UsZona
;



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ConContacto') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE ConContacto
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('DirCatColonia') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE DirCatColonia
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('DirCatEstado') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE DirCatEstado
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('DirCatMunicipio') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE DirCatMunicipio
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('DirCatTipoAsentamiento') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE DirCatTipoAsentamiento
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('DirCatTipoZona') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE DirCatTipoZona
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('DirDirecciones') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE DirDirecciones
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('OpCatEstadosNas') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE OpCatEstadosNas
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('OpCatTipoEstadoNas') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE OpCatTipoEstadoNas
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('OpEstadosPorUsuarios') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE OpEstadosPorUsuarios
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ProArchivos') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE ProArchivos
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ProArchivosPorProductos') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE ProArchivosPorProductos
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ProCatMarca') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE ProCatMarca
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ProCatModelo') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE ProCatModelo
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ProDiviciones') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE ProDiviciones
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ProFamilias') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE ProFamilias
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ProProducto') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE ProProducto
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ProVendedor') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE ProVendedor
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ProVendedorTienda') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE ProVendedorTienda
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ProVenTieProducto') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE ProVenTieProducto
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('SisArbolMenu') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SisArbolMenu
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('SisTipoUsuarioPorMenu') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SisTipoUsuarioPorMenu
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('TieSucursal') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE TieSucursal
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('TieTienda') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE TieTienda
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsEstatusUsuario') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsEstatusUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsTipoUsuario') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsTipoUsuario
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsUsuarioPorZona') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsUsuarioPorZona
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsUsuarios') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsUsuarios
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsZona') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsZona
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('UsZonaPorEstado') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE UsZonaPorEstado
;


CREATE TABLE ConContacto ( 
	IdContacto int identity(1,1)  NOT NULL,
	TelefonoFijoPrincipal varchar(max),
	TelefonoFijoSacundario varchar(max),
	TelefonoMovilPrimario varchar(max),
	TelefonoMovilSecundario varchar(max),
	CorreoElectronico varchar(max),
	SitioWeb varchar(max),
	RedSocialPrimaria varchar(max),
	RedSocialSecundaria varchar(max),
	Borrado bit
)
;

CREATE TABLE DirCatColonia ( 
	IdColonia int NOT NULL,
	IdMunicipios int,
	IdEstado int,
	IdTipoAsentamiento int,
	IdTipoZona int,
	NombreColonia varchar(max),
	CodigoPostal varchar(max),
	Borrado bit
)
;

CREATE TABLE DirCatEstado ( 
	IdEstado int NOT NULL,
	NombreEstado varchar(max),
	NombreOficial varchar(max),
	Creador varchar(100),
	FechaCreacion datetime,
	ModificadoPor varchar(100),
	FechaModificacion datetime,
	Borrado bit
)
;

CREATE TABLE DirCatMunicipio ( 
	IdMunicipios int NOT NULL,
	IdEstado int,
	IdMunicipio int,
	NombreMunicipio varchar(max),
	Creador varchar(100),
	FechaCreacion datetime,
	ModificadoPor varchar(100),
	FechaModificacion datetime,
	Borrado bit
)
;

CREATE TABLE DirCatTipoAsentamiento ( 
	IdTipoAsentmiento int NOT NULL,
	TipoAsentamiento varchar(max),
	Borrado bit
)
;

CREATE TABLE DirCatTipoZona ( 
	IdTipoZona int NOT NULL,
	TipoZona varbinary(max),
	Borrado bit
)
;

CREATE TABLE DirDirecciones ( 
	IdDirecciones int identity(1,1)  NOT NULL,
	IdEstado int,
	IdMunicipio int,
	IdColonia int,
	Calle varchar(max),
	NoExt varchar(max),
	NoInt varchar(max),
	Referencias varchar(max),
	Borrado bit
)
;

CREATE TABLE OpCatEstadosNas ( 
	IdEstadoNas int NOT NULL,
	NombreEstadoNas varchar(max),
	IdTipoEstado int,
	Borrado bit
)
;

CREATE TABLE OpCatTipoEstadoNas ( 
	IdTipoEstado int NOT NULL,
	NombreTipoEstado varchar(max),
	Borrado bit
)
;

CREATE TABLE OpEstadosPorUsuarios ( 
	IdEstadosUsuarios int identity(1,1)  NOT NULL,
	IdEstadoNas int,
	IdTipoUsuario int
)
;

CREATE TABLE ProArchivos ( 
	IdArchivo int identity(1,1)  NOT NULL,
	NombreArchivo varchar(max),
	Extencion varchar(max),
	Archivo varbinary(max),
	Tamaño int
)
;

CREATE TABLE ProArchivosPorProductos ( 
	IdAchProd int identity(1,1)  NOT NULL,
	IdProducto int,
	IdArchivo int
)
;

CREATE TABLE ProCatMarca ( 
	IdMarca int NOT NULL,
	NombreMarca varchar(max),
	Descripcion varchar(150),
	Creador varchar(100),
	FechaCreacion datetime,
	Modificado varchar(100),
	FechaModificacion datetime,
	Borrado bit
)
;

CREATE TABLE ProCatModelo ( 
	IdModelo int NOT NULL,
	IdMarca int,
	NombreModelo varchar(200),
	Descripcion varchar(200),
	Creador varchar(100),
	FechaCreacion datetime,
	ModificadoPor varchar(100),
	FechaModificacion datetime,
	Borrado bit
)
;

CREATE TABLE ProDiviciones ( 
	IdDivicion int NOT NULL,
	NombreDivicion varchar(max),
	Descripcion varchar(max),
	Creador varchar(100),
	FechaCreacion datetime,
	ModificadoPor varchar(100),
	FechaModificacion datetime,
	Borrado bit
)
;

CREATE TABLE ProFamilias ( 
	IdFamilia int NOT NULL,
	NombreFamilia varchar(max),
	Descripcion varchar(max),
	Creador varchar(100),
	FechaCreacion datetime,
	ModificadoPor datetime,
	FechaModificacion datetime,
	Borrado bit
)
;

CREATE TABLE ProProducto ( 
	IdProducto int identity(1,1)  NOT NULL,
	IdModelo int,
	IdMarca int,
	IdFamilia int,
	IdDivicion int,
	NombreProducto varchar(max),
	Descripcion varchar(max),
	CodigoBarras varchar(max),
	Detalles varchar(max),
	EsReparable bit,
	HaySeries bit,
	Borrado bit,
	Creador varchar(100),
	FechaCreacion datetime,
	ModificadoPor varchar(100),
	FechaModificacion datetime
)
;

CREATE TABLE ProVendedor ( 
	IdVendedor int identity(1,1)  NOT NULL,
	NombreVendedor varchar(max),
	ClaveVendedor varchar(200) NOT NULL,
	IdDirecciones int
)
;

CREATE TABLE ProVendedorTienda ( 
	IdTendaVendedor int identity(1,1)  NOT NULL,
	IdVendedor int,
	IdTienda int,
	NombreRelacion varchar(max)
)
;

CREATE TABLE ProVenTieProducto ( 
	IdTiendaVendedor int NOT NULL,
	IdProducto int NOT NULL
)
;

CREATE TABLE SisArbolMenu ( 
	IdMenu int NOT NULL,
	IdMenuPadre int,
	NombreMenu varchar(max) NOT NULL,
	Controller varchar(max),
	Method varchar(50),
	Url varchar(max)
)
;

CREATE TABLE SisTipoUsuarioPorMenu ( 
	IdPermisos int NOT NULL,
	IdTipoUsuario int,
	IdMenu int,
	Borrado bit
)
;

CREATE TABLE TieSucursal ( 
	IdSucursal int identity(1,1)  NOT NULL,
	IdZona int,
	IdContacto int,
	IdDirecciones int,
	IdTienda int,
	NombreTienda varchar(max),
	NombreGerente varchar(max),
	Comentarios varchar(max),
	Borrado bit,
	Activo int,
	NoSucursal varchar(max)
)
;

CREATE TABLE TieTienda ( 
	IdTienda int identity(1,1)  NOT NULL,
	IdDireccion int,
	IdContacto int,
	NombreTienda varchar(100),
	NombreResponsable varchar(max),
	FechaAlta datetime,
	Activo bit,
	Borrado bit
)
;

CREATE TABLE UsEstatusUsuario ( 
	IdEstatusUsuario int NOT NULL,
	EstatusUsuario varchar(max),
	Descripcion varchar(max) NOT NULL,
	Borrado bit NOT NULL
)
;

CREATE TABLE UsTipoUsuario ( 
	IdTipoUsuario int NOT NULL,
	TipoUsuario varchar(max),
	Descripcion varchar(max),
	Borrado bit NOT NULL
)
;

CREATE TABLE UsUsuarioPorZona ( 
	IdUsuarioZona int identity(1,1)  NOT NULL,
	IdZona int,
	IdUsuarios int
)
;

CREATE TABLE UsUsuarios ( 
	IdUsuario int identity(1,1)  NOT NULL,
	Usuario varchar(50) NOT NULL,
	Contrasena varchar(50) NOT NULL,
	IdTipoUsuario int,
	IdEstatusUsuario int,
	Borrado bit NOT NULL
)
;

CREATE TABLE UsZona ( 
	IdZona int NOT NULL,
	NombreZona varchar(max),
	Descripcion varchar(max),
	Borrado bit
)
;

CREATE TABLE UsZonaPorEstado ( 
	IdZonaEstado int identity(1,1)  NOT NULL,
	IdZona int,
	IdEstado int,
	Borrado bit
)
;


ALTER TABLE ProCatModelo
	ADD CONSTRAINT UQ_ProCatModelo_NombreModelo UNIQUE (NombreModelo)
;

ALTER TABLE ProVendedor
	ADD CONSTRAINT UQ_ProVendedor_ClaveVendedor UNIQUE (ClaveVendedor)
;

ALTER TABLE TieTienda
	ADD CONSTRAINT UQ_TieTienda_NombreTienda UNIQUE (NombreTienda)
;

ALTER TABLE UsUsuarios
	ADD CONSTRAINT UQ_UsUsuarios_Usuario UNIQUE (Usuario)
;

ALTER TABLE ConContacto ADD CONSTRAINT PK_ConContacto 
	PRIMARY KEY CLUSTERED (IdContacto)
;

ALTER TABLE DirCatColonia ADD CONSTRAINT PK_DirCatColonia 
	PRIMARY KEY CLUSTERED (IdColonia)
;

ALTER TABLE DirCatEstado ADD CONSTRAINT PK_DirEstado 
	PRIMARY KEY CLUSTERED (IdEstado)
;

ALTER TABLE DirCatMunicipio ADD CONSTRAINT PK_DirCatMunicipio 
	PRIMARY KEY CLUSTERED (IdMunicipios)
;

ALTER TABLE DirCatTipoAsentamiento ADD CONSTRAINT PK_DirCatTipoAsentamiento 
	PRIMARY KEY CLUSTERED (IdTipoAsentmiento)
;

ALTER TABLE DirCatTipoZona ADD CONSTRAINT PK_DirCatTipoZona 
	PRIMARY KEY CLUSTERED (IdTipoZona)
;

ALTER TABLE DirDirecciones ADD CONSTRAINT PK_DirDirecciones 
	PRIMARY KEY CLUSTERED (IdDirecciones)
;

ALTER TABLE OpCatEstadosNas ADD CONSTRAINT PK_OpCatEstadosNas 
	PRIMARY KEY CLUSTERED (IdEstadoNas)
;

ALTER TABLE OpCatTipoEstadoNas ADD CONSTRAINT PK_OpCatTipoEstadoNas 
	PRIMARY KEY CLUSTERED (IdTipoEstado)
;

ALTER TABLE OpEstadosPorUsuarios ADD CONSTRAINT PK_OpEstadosPorUsuarios 
	PRIMARY KEY CLUSTERED (IdEstadosUsuarios)
;

ALTER TABLE ProArchivos ADD CONSTRAINT PK_Archivos 
	PRIMARY KEY CLUSTERED (IdArchivo)
;

ALTER TABLE ProArchivosPorProductos ADD CONSTRAINT PK_ProArchivosPorProductos 
	PRIMARY KEY CLUSTERED (IdAchProd)
;

ALTER TABLE ProCatMarca ADD CONSTRAINT PK_Marca 
	PRIMARY KEY CLUSTERED (IdMarca)
;

ALTER TABLE ProCatModelo ADD CONSTRAINT PK_ProCatModelo 
	PRIMARY KEY CLUSTERED (IdModelo)
;

ALTER TABLE ProDiviciones ADD CONSTRAINT PK_ProDiviciones 
	PRIMARY KEY CLUSTERED (IdDivicion)
;

ALTER TABLE ProFamilias ADD CONSTRAINT PK_ProFamililia 
	PRIMARY KEY CLUSTERED (IdFamilia)
;

ALTER TABLE ProProducto ADD CONSTRAINT PK_ProProducto 
	PRIMARY KEY CLUSTERED (IdProducto)
;

ALTER TABLE ProVendedor ADD CONSTRAINT PK_ProVendedor 
	PRIMARY KEY CLUSTERED (IdVendedor)
;

ALTER TABLE ProVendedorTienda ADD CONSTRAINT PK_ProVendedorTienda 
	PRIMARY KEY CLUSTERED (IdTendaVendedor)
;

ALTER TABLE ProVenTieProducto ADD CONSTRAINT PK_ProVenTieProducto 
	PRIMARY KEY CLUSTERED (IdTiendaVendedor, IdProducto)
;

ALTER TABLE SisArbolMenu ADD CONSTRAINT PK_SisArrbolMenu 
	PRIMARY KEY CLUSTERED (IdMenu)
;

ALTER TABLE SisTipoUsuarioPorMenu ADD CONSTRAINT PK_UsPermisos 
	PRIMARY KEY CLUSTERED (IdPermisos)
;

ALTER TABLE TieSucursal ADD CONSTRAINT PK_TipSucursal 
	PRIMARY KEY CLUSTERED (IdSucursal)
;

ALTER TABLE TieTienda ADD CONSTRAINT PK_TieTienda 
	PRIMARY KEY CLUSTERED (IdTienda)
;

ALTER TABLE UsEstatusUsuario ADD CONSTRAINT PK_UsEstatusUsuario 
	PRIMARY KEY CLUSTERED (IdEstatusUsuario)
;

ALTER TABLE UsTipoUsuario ADD CONSTRAINT PK_UsTipoUsuario 
	PRIMARY KEY CLUSTERED (IdTipoUsuario)
;

ALTER TABLE UsUsuarioPorZona ADD CONSTRAINT PK_UsUsuarioPorZona 
	PRIMARY KEY CLUSTERED (IdUsuarioZona)
;

ALTER TABLE UsUsuarios ADD CONSTRAINT PK_UsUsuarios 
	PRIMARY KEY CLUSTERED (IdUsuario)
;

ALTER TABLE UsZona ADD CONSTRAINT PK_UsZona 
	PRIMARY KEY CLUSTERED (IdZona)
;

ALTER TABLE UsZonaPorEstado ADD CONSTRAINT PK_UsZonaPorEstado 
	PRIMARY KEY CLUSTERED (IdZonaEstado)
;



ALTER TABLE DirCatColonia ADD CONSTRAINT FK_DirCatColonia_DirCatEstado 
	FOREIGN KEY (IdEstado) REFERENCES DirCatEstado (IdEstado)
;

ALTER TABLE DirCatColonia ADD CONSTRAINT FK_DirCatColonia_DirCatMunicipio 
	FOREIGN KEY (IdMunicipios) REFERENCES DirCatMunicipio (IdMunicipios)
;

ALTER TABLE DirCatColonia ADD CONSTRAINT FK_DirCatColonia_DirCatTipoAsentamiento 
	FOREIGN KEY (IdTipoAsentamiento) REFERENCES DirCatTipoAsentamiento (IdTipoAsentmiento)
;

ALTER TABLE DirCatColonia ADD CONSTRAINT FK_DirCatColonia_DirCatTipoZona 
	FOREIGN KEY (IdTipoZona) REFERENCES DirCatTipoZona (IdTipoZona)
;

ALTER TABLE DirDirecciones ADD CONSTRAINT FK_DirDirecciones_DirCatColonia 
	FOREIGN KEY (IdColonia) REFERENCES DirCatColonia (IdColonia)
;

ALTER TABLE DirDirecciones ADD CONSTRAINT FK_DirDirecciones_DirCatEstado 
	FOREIGN KEY (IdEstado) REFERENCES DirCatEstado (IdEstado)
;

ALTER TABLE OpEstadosPorUsuarios ADD CONSTRAINT FK_OpEstadosPorUsuarios_UsTipoUsuario 
	FOREIGN KEY (IdTipoUsuario) REFERENCES UsTipoUsuario (IdTipoUsuario)
;

ALTER TABLE ProCatModelo ADD CONSTRAINT FK_ProCatModelo_ProCatMarca 
	FOREIGN KEY (IdMarca) REFERENCES ProCatMarca (IdMarca)
;

ALTER TABLE ProProducto ADD CONSTRAINT FK_ProProducto_ProCatMarca 
	FOREIGN KEY (IdMarca) REFERENCES ProCatMarca (IdMarca)
;

ALTER TABLE ProProducto ADD CONSTRAINT FK_ProProducto_ProCatModelo 
	FOREIGN KEY (IdModelo) REFERENCES ProCatModelo (IdModelo)
;

ALTER TABLE ProProducto ADD CONSTRAINT FK_ProProducto_ProDiviciones 
	FOREIGN KEY (IdDivicion) REFERENCES ProDiviciones (IdDivicion)
;

ALTER TABLE ProVendedorTienda ADD CONSTRAINT FK_ProVendedorTienda_ProVendedor 
	FOREIGN KEY (IdVendedor) REFERENCES ProVendedor (IdVendedor)
;

ALTER TABLE ProVendedorTienda ADD CONSTRAINT FK_ProVendedorTienda_TieTienda 
	FOREIGN KEY (IdTienda) REFERENCES TieTienda (IdTienda)
;

ALTER TABLE ProVenTieProducto ADD CONSTRAINT FK_ProVenTieProducto_ProProducto 
	FOREIGN KEY (IdProducto) REFERENCES ProProducto (IdProducto)
;

ALTER TABLE ProVenTieProducto ADD CONSTRAINT FK_ProVenTieProducto_ProVendedorTienda 
	FOREIGN KEY (IdTiendaVendedor) REFERENCES ProVendedorTienda (IdTendaVendedor)
;

ALTER TABLE SisArbolMenu ADD CONSTRAINT FK_SisArrbolMenu_SisArrbolMenu 
	FOREIGN KEY (IdMenuPadre) REFERENCES SisArbolMenu (IdMenu)
;

ALTER TABLE SisTipoUsuarioPorMenu ADD CONSTRAINT FK_SisTipoUsuarioPorMenu_SisArbolMenu 
	FOREIGN KEY (IdMenu) REFERENCES SisArbolMenu (IdMenu)
;

ALTER TABLE SisTipoUsuarioPorMenu ADD CONSTRAINT FK_SisTipoUsuarioPorMenu_UsTipoUsuario 
	FOREIGN KEY (IdTipoUsuario) REFERENCES UsTipoUsuario (IdTipoUsuario)
;

ALTER TABLE TieSucursal ADD CONSTRAINT FK_TieSucursal_ConContacto 
	FOREIGN KEY (IdContacto) REFERENCES ConContacto (IdContacto)
;

ALTER TABLE TieSucursal ADD CONSTRAINT FK_TieSucursal_DirDirecciones 
	FOREIGN KEY (IdDirecciones) REFERENCES DirDirecciones (IdDirecciones)
;

ALTER TABLE TieSucursal ADD CONSTRAINT FK_TieSucursal_TieTienda 
	FOREIGN KEY (IdTienda) REFERENCES TieTienda (IdTienda)
;

ALTER TABLE TieSucursal ADD CONSTRAINT FK_TieSucursal_UsZona 
	FOREIGN KEY (IdZona) REFERENCES UsZona (IdZona)
;

ALTER TABLE TieTienda ADD CONSTRAINT FK_TieTienda_ConContacto 
	FOREIGN KEY (IdContacto) REFERENCES ConContacto (IdContacto)
;

ALTER TABLE TieTienda ADD CONSTRAINT FK_TieTienda_DirDirecciones 
	FOREIGN KEY (IdDireccion) REFERENCES DirDirecciones (IdDirecciones)
;

ALTER TABLE UsUsuarioPorZona ADD CONSTRAINT FK_UsUsuarioPorZona_UsUsuarios 
	FOREIGN KEY (IdUsuarios) REFERENCES UsUsuarios (IdUsuario)
;

ALTER TABLE UsUsuarioPorZona ADD CONSTRAINT FK_UsUsuarioPorZona_UsZona 
	FOREIGN KEY (IdZona) REFERENCES UsZona (IdZona)
;

ALTER TABLE UsUsuarios ADD CONSTRAINT FK_UsUsuarios_UsTipoUsuario 
	FOREIGN KEY (IdTipoUsuario) REFERENCES UsTipoUsuario (IdTipoUsuario)
;

ALTER TABLE UsZonaPorEstado ADD CONSTRAINT FK_UsZonaPorEstado_DirCatEstado 
	FOREIGN KEY (IdEstado) REFERENCES DirCatEstado (IdEstado)
;

ALTER TABLE UsZonaPorEstado ADD CONSTRAINT FK_UsZonaPorEstado_UsZona 
	FOREIGN KEY (IdZona) REFERENCES UsZona (IdZona)
;
