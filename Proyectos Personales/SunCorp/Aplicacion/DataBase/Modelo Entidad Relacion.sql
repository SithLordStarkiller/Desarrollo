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
	NumeroDeSerie varchar(max),
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
