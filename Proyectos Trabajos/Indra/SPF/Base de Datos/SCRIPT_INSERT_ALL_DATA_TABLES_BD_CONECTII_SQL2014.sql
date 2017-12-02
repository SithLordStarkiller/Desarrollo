USE [master]
GO
/****** Object:  Database [ConecII]    Script Date: 01/09/2017 19:24:55 ******/
CREATE DATABASE [ConecII]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ConecII', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER16\MSSQL\DATA\Conec2_data.mdf' , SIZE = 6144KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ConecII_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER16\MSSQL\DATA\Conec2_log.ldf' , SIZE = 12352KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ConecII] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ConecII].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ConecII] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ConecII] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ConecII] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ConecII] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ConecII] SET ARITHABORT OFF 
GO
ALTER DATABASE [ConecII] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ConecII] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ConecII] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ConecII] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ConecII] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ConecII] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ConecII] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ConecII] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ConecII] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ConecII] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ConecII] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ConecII] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ConecII] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ConecII] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ConecII] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ConecII] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ConecII] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ConecII] SET RECOVERY FULL 
GO
ALTER DATABASE [ConecII] SET  MULTI_USER 
GO
ALTER DATABASE [ConecII] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ConecII] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ConecII] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ConecII] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ConecII] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ConecII', N'ON'
GO
ALTER DATABASE [ConecII] SET QUERY_STORE = OFF
GO
USE [ConecII]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ConecII]
GO
/****** Object:  User [conec_dev]    Script Date: 01/09/2017 19:24:55 ******/
CREATE USER [conec_dev] FOR LOGIN [conec_dev] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [conec_dba]    Script Date: 01/09/2017 19:24:55 ******/
CREATE USER [conec_dba] FOR LOGIN [conec_dba] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [conec_dev]
GO
ALTER ROLE [db_datareader] ADD MEMBER [conec_dev]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [conec_dev]
GO
ALTER ROLE [db_owner] ADD MEMBER [conec_dba]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [conec_dba]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [conec_dba]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [conec_dba]
GO
/****** Object:  Schema [Catalogo]    Script Date: 01/09/2017 19:24:55 ******/
CREATE SCHEMA [Catalogo]
GO
/****** Object:  Schema [Configuracion]    Script Date: 01/09/2017 19:24:55 ******/
CREATE SCHEMA [Configuracion]
GO
/****** Object:  Schema [Cuota]    Script Date: 01/09/2017 19:24:55 ******/
CREATE SCHEMA [Cuota]
GO
/****** Object:  Schema [Factor]    Script Date: 01/09/2017 19:24:55 ******/
CREATE SCHEMA [Factor]
GO
/****** Object:  Schema [Seguridad]    Script Date: 01/09/2017 19:24:55 ******/
CREATE SCHEMA [Seguridad]
GO
/****** Object:  Schema [Solicitud]    Script Date: 01/09/2017 19:24:55 ******/
CREATE SCHEMA [Solicitud]
GO
/****** Object:  UserDefinedTableType [dbo].[ConfiguracionServicio]    Script Date: 01/09/2017 19:24:55 ******/
CREATE TYPE [dbo].[ConfiguracionServicio] AS TABLE(
	[IdTipoServicio] [int] NULL,
	[IdCentroCostos] [int] NULL,
	[IdRegimenFiscal] [int] NULL,
	[IdTipoPago] [int] NULL,
	[IdActividad] [int] NULL,
	[Tiempo] [decimal](18, 2) NULL,
	[IdTipoDocumento] [int] NULL,
	[Aplica] [bit] NULL,
	[Obigatoriedad] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Factores]    Script Date: 01/09/2017 19:24:55 ******/
CREATE TYPE [dbo].[Factores] AS TABLE(
	[IdFactor] [int] NULL,
	[IdClasificacionFactor] [int] NULL,
	[Descripcion] [varchar](300) NULL,
	[IdEstado] [int] NULL,
	[IdMunicipio] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[FactoresEntidadFederativa]    Script Date: 01/09/2017 19:24:55 ******/
CREATE TYPE [dbo].[FactoresEntidadFederativa] AS TABLE(
	[IdClasificadorFactor] [int] NULL,
	[IdFactor] [int] NULL,
	[Descripcion] [varchar](300) NULL,
	[IdEntidFed] [int] NULL,
	[FechaInicial] [date] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[FactoresMunicipio]    Script Date: 01/09/2017 19:24:55 ******/
CREATE TYPE [dbo].[FactoresMunicipio] AS TABLE(
	[IdFactor] [int] NULL,
	[Descripcion] [varchar](300) NULL,
	[IdEntidFed] [int] NULL,
	[IdMunicipio] [int] NULL
)
GO
/****** Object:  View [Catalogo].[vwCentrosCosto]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Catalogo].[vwCentrosCosto]
AS
SELECT        idCentroCosto AS IdCentroCosto, ccDescripcion AS Descripcion, ccVigente AS Activo
FROM            [DELL-623].personal.catalogo.centroCosto AS centroCosto_1

GO
/****** Object:  View [Catalogo].[vwEstados]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Catalogo].[vwEstados]
AS
SELECT        idPais AS IdPais, idEstado AS IdEntidFed, estDescripcion AS Descripcion
FROM            [DELL-623].sicogua.catalogo.estado AS estado

GO
/****** Object:  View [Catalogo].[vwGruposTarifario]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Catalogo].[vwGruposTarifario]
AS
SELECT        idGrupoTarifario AS IdGrupoTarifario, gtDescripcion AS Descripcion, gtVigente AS Activo
FROM            [DELL-623].cove.catalogo.grupoTarifario AS grupoTarifario_1

GO
/****** Object:  View [Catalogo].[vwJerarquias]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Catalogo].[vwJerarquias]
AS
SELECT        idJerarquia AS IdJerarquia, jerDescripcion AS Descripcion, jerSueldo AS Sueldo, jerVigente AS Activo, jerNivel AS Nivel
FROM            [DELL-623].sicogua.catalogo.jerarquia AS jerarquia_1

GO
/****** Object:  View [Catalogo].[vwMunicipios]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Catalogo].[vwMunicipios]
AS
SELECT        idPais AS IdPais, idEstado AS IdEstado, idMunicipio AS IdMunicipio, munDescripcion AS Descripcion, munVigente AS Activo
FROM            [DELL-623].sicogua.catalogo.visMunicipio AS Municipio

GO
/****** Object:  Table [Catalogo].[Actividades]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Actividades](
	[IdActividad] [int] IDENTITY(1,1) NOT NULL,
	[IdFase] [int] NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](100) NULL,
	[SePuedeAplicarPlazo] [bit] NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Actividades] PRIMARY KEY CLUSTERED 
(
	[IdActividad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Anios]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Anios](
	[Anio] [int] NOT NULL,
	[Descripcion] [varchar](100) NULL,
 CONSTRAINT [PK_Anio] PRIMARY KEY CLUSTERED 
(
	[Anio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[ClasificacionFactor]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[ClasificacionFactor](
	[IdClasificacionFactor] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](100) NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_ClasificacionFactor] PRIMARY KEY CLUSTERED 
(
	[IdClasificacionFactor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Cuotas]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Cuotas](
	[IdCuota] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoServicio] [int] NOT NULL,
	[IdReferencia] [int] NOT NULL,
	[IdDependencia] [int] NOT NULL,
	[Concepto] [varchar](1500) NOT NULL,
	[IdJerarquia] [int] NULL,
	[IdGrupoTarifario] [int] NULL,
	[CuotaBase] [decimal](18, 2) NOT NULL,
	[IdMedidaCobro] [int] NOT NULL,
	[Iva] [decimal](18, 2) NULL,
	[FechaAutorizacion] [date] NOT NULL,
	[FechaEntradaVigor] [date] NOT NULL,
	[FechaTermino] [date] NULL,
	[FechaPublicaDof] [date] NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Cuotas] PRIMARY KEY CLUSTERED 
(
	[IdCuota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Dependencias]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Dependencias](
	[IdDependencia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Id_CveDependencia] PRIMARY KEY CLUSTERED 
(
	[IdDependencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Divisiones]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Divisiones](
	[IdDivision] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](300) NOT NULL,
	[Descripcion] [varchar](300) NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Divisiones] PRIMARY KEY CLUSTERED 
(
	[IdDivision] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Fracciones]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Fracciones](
	[IdFraccion] [smallint] NOT NULL,
	[Nombre] [varchar](1500) NOT NULL,
	[Descripcion] [varchar](1500) NOT NULL,
	[IdGrupo] [int] NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Fraccion] PRIMARY KEY CLUSTERED 
(
	[IdFraccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Grupos]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Grupos](
	[IdGrupo] [int] IDENTITY(1,1) NOT NULL,
	[IdDivision] [int] NOT NULL,
	[Nombre] [varchar](300) NOT NULL,
	[Descripcion] [varchar](300) NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Grupo] PRIMARY KEY CLUSTERED 
(
	[IdGrupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[MedidasCobro]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[MedidasCobro](
	[IdMedidaCobro] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](100) NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_MedidaCobro] PRIMARY KEY CLUSTERED 
(
	[IdMedidaCobro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Meses]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Meses](
	[IdMes] [int] NOT NULL,
	[Nombre] [varchar](100) NULL,
 CONSTRAINT [PK_Id_Mes] PRIMARY KEY CLUSTERED 
(
	[IdMes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Periodos]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Periodos](
	[IdPeriodo] [int] IDENTITY(1,1) NOT NULL,
	[Periodo] [varchar](50) NOT NULL,
	[Descripcion] [varchar](300) NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_IdPeriodo] PRIMARY KEY CLUSTERED 
(
	[IdPeriodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Referencias]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Referencias](
	[IdReferencia] [int] IDENTITY(1,1) NOT NULL,
	[ClaveReferencia] [int] NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_ClaveReferencia] PRIMARY KEY CLUSTERED 
(
	[IdReferencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Sector]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Sector](
	[IdSector] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](30) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[TiposDocumento]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[TiposDocumento](
	[IdTipoDocumento] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](100) NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NULL,
	[IdActividad] [int] NOT NULL,
	[Confidencial] [bit] NULL,
 CONSTRAINT [PK_IdTipoDocumento] PRIMARY KEY CLUSTERED 
(
	[IdTipoDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[TiposPago]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[TiposPago](
	[IdTipoPago] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](300) NULL,
	[Actividad] [bit] NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_TiposPago] PRIMARY KEY CLUSTERED 
(
	[IdTipoPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[TiposRegimenFiscal]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[TiposRegimenFiscal](
	[IdRegimenFiscal] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](100) NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_TipoRegimenFiscal] PRIMARY KEY CLUSTERED 
(
	[IdRegimenFiscal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[TiposServicio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[TiposServicio](
	[IdTipoServicio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](300) NOT NULL,
	[Clave] [varchar](4) NOT NULL,
	[FechaEntradaVigor] [date] NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_IdTipoServicio] PRIMARY KEY CLUSTERED 
(
	[IdTipoServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Tmp_Cuotas]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Tmp_Cuotas](
	[IdCuota] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoServicio] [int] NOT NULL,
	[IdReferencia] [int] NOT NULL,
	[IdDependencia] [int] NOT NULL,
	[Concepto] [varchar](1500) NOT NULL,
	[IdJerarquia] [int] NULL,
	[IdGrupoTarifario] [int] NULL,
	[CuotaBase] [decimal](18, 2) NOT NULL,
	[IdMedidaCobro] [int] NOT NULL,
	[Iva] [decimal](18, 2) NULL,
	[FechaAutorizacion] [date] NOT NULL,
	[FechaEntradaVigor] [date] NOT NULL,
	[FechaTermino] [date] NULL,
	[FechaPublicaDof] [date] NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [Configuracion].[ActividadesTiposDocumento]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Configuracion].[ActividadesTiposDocumento](
	[IdActividadTipoDocumento] [int] IDENTITY(1,1) NOT NULL,
	[IdActividad] [int] NOT NULL,
	[IdTipoDocumento] [int] NOT NULL,
	[IdConfServicio] [int] NOT NULL,
	[Aplica] [bit] NULL,
	[Obligatoriedad] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [Configuracion].[AreasValidadoras]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Configuracion].[AreasValidadoras](
	[IdAreaValidadora] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoPagoActividad] [int] NOT NULL,
	[IdCentroCosto] [int] NOT NULL,
	[Obligatorio] [bit] NULL,
 CONSTRAINT [PK_IdAreaValidadora] PRIMARY KEY CLUSTERED 
(
	[IdAreaValidadora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Configuracion].[ConfServicio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Configuracion].[ConfServicio](
	[IdConfServicio] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoServicio] [int] NOT NULL,
	[IdCentroCosto] [varchar](6) NOT NULL,
	[Activo] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [Configuracion].[Fases]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Configuracion].[Fases](
	[IdFase] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](100) NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Fases] PRIMARY KEY CLUSTERED 
(
	[IdFase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Configuracion].[Notificaciones]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Configuracion].[Notificaciones](
	[IdNotificacion] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoServicio] [int] NOT NULL,
	[IdActividad] [int] NOT NULL,
	[CuerpoCorreo] [varchar](4000) NULL,
	[EsCorreo] [bit] NULL,
	[EsSistema] [bit] NOT NULL,
	[EmitirAlerta] [bit] NULL,
	[TiempoAlerta] [int] NULL,
	[Frecuencia] [int] NULL,
	[AlertaEsCorreo] [bit] NULL,
	[AlertaEsSistema] [bit] NULL,
	[CuerpoAlerta] [varchar](4000) NULL,
	[FechaInicial] [date] NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Notificaciones] PRIMARY KEY CLUSTERED 
(
	[IdNotificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Configuracion].[ReceptoresAlertas]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Configuracion].[ReceptoresAlertas](
	[IdReceptorAlerta] [int] IDENTITY(1,1) NOT NULL,
	[IdRol] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdNotificacion] [int] NOT NULL,
	[FechaInicial] [date] NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_IdReceptorAlerta] PRIMARY KEY CLUSTERED 
(
	[IdReceptorAlerta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Configuracion].[TiposPagoActividades]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Configuracion].[TiposPagoActividades](
	[IdTipoPagoActividad] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoPago] [int] NOT NULL,
	[IdConfServicio] [int] NOT NULL,
	[IdActividad] [int] NOT NULL,
	[Aplica] [bit] NOT NULL,
	[Tiempo] [int] NULL,
 CONSTRAINT [PK_TiposPagoActividades] PRIMARY KEY CLUSTERED 
(
	[IdTipoPagoActividad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Configuracion].[TiposRegimenFiscalTiposPago]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Configuracion].[TiposRegimenFiscalTiposPago](
	[IdTipoRegimenFiscalTipoPago] [int] IDENTITY(1,1) NOT NULL,
	[IdRegimenFiscal] [int] NOT NULL,
	[IdTipoPago] [int] NOT NULL,
	[IdConfServicio] [int] NOT NULL,
	[Aplica] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [Cuota].[Cuotas]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuota].[Cuotas](
	[IdCuota] [int] NOT NULL,
	[IdTipoServicio] [int] NOT NULL,
	[IdReferencia] [int] NOT NULL,
	[IdDependencia] [int] NOT NULL,
	[IdCadenaDependencia] [int] NOT NULL,
	[Concepto] [varchar](1500) NOT NULL,
	[IdJerarquia] [int] NULL,
	[IdGrupoTarifarioVehiculo] [int] NULL,
	[CuotaBase] [decimal](18, 2) NOT NULL,
	[IdMedidaCobro] [int] NOT NULL,
	[Iva] [decimal](18, 2) NULL,
	[FechaAutorizacion] [date] NOT NULL,
	[FechaEntradaVigor] [date] NOT NULL,
	[FechaTermino] [date] NULL,
	[FechaPublicaDof] [date] NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_IdCuota] PRIMARY KEY CLUSTERED 
(
	[IdCuota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bitacoras]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacoras](
	[IdBitacora] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[MAC] [varchar](50) NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [Factor].[Factores]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Factor].[Factores](
	[IdFactor] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoServicio] [int] NOT NULL,
	[IdClasificacionFactor] [int] NOT NULL,
	[IdMedidaCobro] [int] NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[Descripcion1] [varchar](100) NULL,
	[Factor] [varchar](100) NOT NULL,
	[CuotaFactor] [decimal](18, 2) NOT NULL,
	[FechaAutorizacion] [date] NOT NULL,
	[FechaEntradaVigor] [date] NOT NULL,
	[FechaTermino] [date] NULL,
	[FechaPublicacionDof] [date] NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Factores] PRIMARY KEY CLUSTERED 
(
	[IdFactor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Factor].[FactoresEntidadFederativa]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Factor].[FactoresEntidadFederativa](
	[IdFactEntidFed] [int] IDENTITY(1,1) NOT NULL,
	[IdClasificadorFactor] [int] NOT NULL,
	[IdFactor] [int] NOT NULL,
	[Descripcion] [varchar](300) NULL,
	[IdEntidFed] [int] NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Id_FactEntidFed] PRIMARY KEY CLUSTERED 
(
	[IdFactEntidFed] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Factor].[FactoresLeyIngreso]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Factor].[FactoresLeyIngreso](
	[IdFactorLI] [int] IDENTITY(1,1) NOT NULL,
	[Anio] [int] NOT NULL,
	[IdMes] [int] NOT NULL,
	[Factor] [decimal](18, 2) NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_FactoresLeyIngreso] PRIMARY KEY CLUSTERED 
(
	[IdFactorLI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Factor].[FactoresMunicipio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Factor].[FactoresMunicipio](
	[IdFactMunicipio] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](300) NULL,
	[IdFactor] [int] NOT NULL,
	[IdEntidFed] [int] NOT NULL,
	[IdMunicipio] [int] NOT NULL,
	[FechaInicial] [date] NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Id_FactMpio] PRIMARY KEY CLUSTERED 
(
	[IdFactMunicipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_EstadoMunicipio] UNIQUE NONCLUSTERED 
(
	[IdEntidFed] ASC,
	[IdMunicipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Factor].[GastosInherentes]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Factor].[GastosInherentes](
	[IdGastoInherente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](150) NOT NULL,
	[Descripcion] [varchar](250) NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Id_FactorInherente] PRIMARY KEY CLUSTERED 
(
	[IdGastoInherente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[Controles]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[Controles](
	[IdControl] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoControl] [int] NOT NULL,
	[IdModulo] [int] NOT NULL,
	[Nombre] [varchar](100) NULL,
 CONSTRAINT [PK_IdControl] PRIMARY KEY CLUSTERED 
(
	[IdControl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[Externos]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[Externos](
	[IdExterno] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoContacto] [int] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[APaterno] [varchar](100) NOT NULL,
	[AMaterno] [varchar](100) NULL,
	[RFC] [varchar](13) NULL,
 CONSTRAINT [PK_IdExterno] PRIMARY KEY CLUSTERED 
(
	[IdExterno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[ModulosControles]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[ModulosControles](
	[IdModuloControl] [int] IDENTITY(1,1) NOT NULL,
	[IdModulo] [int] NOT NULL,
	[IdControl] [int] NOT NULL,
 CONSTRAINT [PK_IdModuloControl] PRIMARY KEY CLUSTERED 
(
	[IdModuloControl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[Personas]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[Personas](
	[IdPersona] [int] IDENTITY(1,1) NOT NULL,
	[APaterno] [varchar](100) NOT NULL,
	[AMaterno] [varchar](100) NULL,
	[RFC] [varchar](13) NULL,
 CONSTRAINT [PK_IdPersona] PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[Roles]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[Roles](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[IdSubrol] [int] NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Descripcion] [varchar](300) NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[RolesControles]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[RolesControles](
	[IdRolControl] [int] IDENTITY(1,1) NOT NULL,
	[IdRol] [int] NOT NULL,
	[IdControl] [int] NOT NULL,
 CONSTRAINT [PK_IdRolControl] PRIMARY KEY CLUSTERED 
(
	[IdRolControl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[RolesModulos]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[RolesModulos](
	[IdRolModulo] [int] IDENTITY(1,1) NOT NULL,
	[IdRol] [int] NOT NULL,
	[IdModulo] [int] NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_IdRoleModulo] PRIMARY KEY CLUSTERED 
(
	[IdRolModulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[RolesModulosControles]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[RolesModulosControles](
	[IdRolModuloControl] [int] IDENTITY(1,1) NOT NULL,
	[IdRolModulo] [int] NOT NULL,
	[IdControl] [int] NOT NULL,
	[Capura] [bit] NULL,
	[Consulta] [bit] NULL,
 CONSTRAINT [PK_IdRolModuloControl] PRIMARY KEY CLUSTERED 
(
	[IdRolModuloControl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[RolesUsuarios]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[RolesUsuarios](
	[IdRolesUsuarios] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdRol] [int] NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_IdRolesUsuarios] PRIMARY KEY CLUSTERED 
(
	[IdRolesUsuarios] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[TiposContacto]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[TiposContacto](
	[IdTipoContacto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_IdTipoContacto] PRIMARY KEY CLUSTERED 
(
	[IdTipoContacto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[TiposControl]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[TiposControl](
	[IdTipoControl] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [int] NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_IdTipoControl] PRIMARY KEY CLUSTERED 
(
	[IdTipoControl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[Tmp_Modulos]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[Tmp_Modulos](
	[IdModulo] [int] IDENTITY(1,1) NOT NULL,
	[IdPadre] [int] NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [Seguridad].[Usuarios]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seguridad].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[IdPersona] [int] NOT NULL,
	[IdExterno] [int] NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varbinary](1) NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_IdUsuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [Catalogo].[Actividades] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[Actividades] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[ClasificacionFactor] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[ClasificacionFactor] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[Dependencias] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[Dependencias] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[Divisiones] ADD  CONSTRAINT [DF__Divisione__Fecha__7EF6D905]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[Divisiones] ADD  CONSTRAINT [DF__Divisione__Activ__7FEAFD3E]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[Fracciones] ADD  CONSTRAINT [DF__Fraccione__Fecha__078C1F06]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[Fracciones] ADD  CONSTRAINT [DF__Fraccione__Activ__0880433F]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[Grupos] ADD  CONSTRAINT [DF__Grupos__FechaIni__02C769E9]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[Grupos] ADD  CONSTRAINT [DF__Grupos__Activo__03BB8E22]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[MedidasCobro] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[MedidasCobro] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[Periodos] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[Periodos] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[Referencias] ADD  CONSTRAINT [DF__Referenci__Fecha__145C0A3F]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[Referencias] ADD  CONSTRAINT [DF__Referenci__Activ__15502E78]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[TiposDocumento] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[TiposDocumento] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[TiposPago] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[TiposPago] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[TiposRegimenFiscal] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[TiposServicio] ADD  CONSTRAINT [DF__TiposServ__Fecha__2F10007B]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[TiposServicio] ADD  CONSTRAINT [DF__TiposServ__Activ__300424B4]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[Tmp_Cuotas] ADD  CONSTRAINT [DF_Cuotas_FechaInicial]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Catalogo].[Tmp_Cuotas] ADD  CONSTRAINT [DF_Cuotas_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Configuracion].[Fases] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Configuracion].[Fases] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Configuracion].[Notificaciones] ADD  CONSTRAINT [DF__Notificac__Fecha__66161CA2]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Configuracion].[Notificaciones] ADD  CONSTRAINT [DF__Notificac__Activ__670A40DB]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Configuracion].[ReceptoresAlertas] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Configuracion].[ReceptoresAlertas] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Cuota].[Cuotas] ADD  CONSTRAINT [DF_Cuotas_FechaInicial]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Cuota].[Cuotas] ADD  CONSTRAINT [DF_Cuotas_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Bitacoras] ADD  CONSTRAINT [DF__Bitacoras__Fecha__090A5324]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [dbo].[Bitacoras] ADD  CONSTRAINT [DF__Bitacoras__Activ__09FE775D]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Factor].[Factores] ADD  CONSTRAINT [DF__Factores__FechaI__03F0984C]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Factor].[Factores] ADD  CONSTRAINT [DF__Factores__Activo__04E4BC85]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Factor].[FactoresEntidadFederativa] ADD  CONSTRAINT [DF__FactoresE__Fecha__0A9D95DB]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Factor].[FactoresEntidadFederativa] ADD  CONSTRAINT [DF__FactoresE__Activ__0B91BA14]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Factor].[FactoresLeyIngreso] ADD  CONSTRAINT [DF__FactoresL__Fecha__276EDEB3]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Factor].[FactoresLeyIngreso] ADD  CONSTRAINT [DF__FactoresL__Activ__286302EC]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Factor].[GastosInherentes] ADD  CONSTRAINT [DF__GastosInh__Fecha__1BFD2C07]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Factor].[GastosInherentes] ADD  CONSTRAINT [DF__GastosInh__Activ__1CF15040]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Seguridad].[Roles] ADD  CONSTRAINT [DF__Roles__FechaInic__2077C861]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Seguridad].[Roles] ADD  CONSTRAINT [DF__Roles__Activo__216BEC9A]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Seguridad].[RolesModulos] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Seguridad].[RolesModulos] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Seguridad].[RolesUsuarios] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Seguridad].[RolesUsuarios] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Seguridad].[TiposControl] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Seguridad].[TiposControl] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Seguridad].[Tmp_Modulos] ADD  CONSTRAINT [DF__Modulos__FechaIn__1CA7377D]  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Seguridad].[Tmp_Modulos] ADD  CONSTRAINT [DF__Modulos__Activo__1D9B5BB6]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Seguridad].[Usuarios] ADD  DEFAULT (getdate()) FOR [FechaInicial]
GO
ALTER TABLE [Seguridad].[Usuarios] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Catalogo].[Actividades]  WITH CHECK ADD  CONSTRAINT [FK_Actividades_Fases] FOREIGN KEY([IdFase])
REFERENCES [Configuracion].[Fases] ([IdFase])
GO
ALTER TABLE [Catalogo].[Actividades] CHECK CONSTRAINT [FK_Actividades_Fases]
GO
ALTER TABLE [Catalogo].[Cuotas]  WITH CHECK ADD  CONSTRAINT [FK_Cuotas_Dependencias] FOREIGN KEY([IdDependencia])
REFERENCES [Catalogo].[Dependencias] ([IdDependencia])
GO
ALTER TABLE [Catalogo].[Cuotas] CHECK CONSTRAINT [FK_Cuotas_Dependencias]
GO
ALTER TABLE [Catalogo].[Cuotas]  WITH CHECK ADD  CONSTRAINT [FK_Cuotas_MedidasCobro] FOREIGN KEY([IdMedidaCobro])
REFERENCES [Catalogo].[MedidasCobro] ([IdMedidaCobro])
GO
ALTER TABLE [Catalogo].[Cuotas] CHECK CONSTRAINT [FK_Cuotas_MedidasCobro]
GO
ALTER TABLE [Catalogo].[Cuotas]  WITH CHECK ADD  CONSTRAINT [FK_Cuotas_Referencias] FOREIGN KEY([IdReferencia])
REFERENCES [Catalogo].[Referencias] ([IdReferencia])
GO
ALTER TABLE [Catalogo].[Cuotas] CHECK CONSTRAINT [FK_Cuotas_Referencias]
GO
ALTER TABLE [Catalogo].[Cuotas]  WITH CHECK ADD  CONSTRAINT [FK_Cuotas_TiposServicio] FOREIGN KEY([IdTipoServicio])
REFERENCES [Catalogo].[TiposServicio] ([IdTipoServicio])
GO
ALTER TABLE [Catalogo].[Cuotas] CHECK CONSTRAINT [FK_Cuotas_TiposServicio]
GO
ALTER TABLE [Catalogo].[Fracciones]  WITH CHECK ADD  CONSTRAINT [FK_Fracciones_Grupos] FOREIGN KEY([IdGrupo])
REFERENCES [Catalogo].[Grupos] ([IdGrupo])
GO
ALTER TABLE [Catalogo].[Fracciones] CHECK CONSTRAINT [FK_Fracciones_Grupos]
GO
ALTER TABLE [Catalogo].[Grupos]  WITH CHECK ADD  CONSTRAINT [FK_GrupoDivisiones] FOREIGN KEY([IdDivision])
REFERENCES [Catalogo].[Divisiones] ([IdDivision])
GO
ALTER TABLE [Catalogo].[Grupos] CHECK CONSTRAINT [FK_GrupoDivisiones]
GO
ALTER TABLE [Catalogo].[TiposDocumento]  WITH CHECK ADD  CONSTRAINT [FK_TiposDocumento_Actividades] FOREIGN KEY([IdActividad])
REFERENCES [Catalogo].[Actividades] ([IdActividad])
GO
ALTER TABLE [Catalogo].[TiposDocumento] CHECK CONSTRAINT [FK_TiposDocumento_Actividades]
GO
ALTER TABLE [Configuracion].[AreasValidadoras]  WITH CHECK ADD  CONSTRAINT [FK_AreasValidadoras_TiposPagoActividades] FOREIGN KEY([IdTipoPagoActividad])
REFERENCES [Configuracion].[TiposPagoActividades] ([IdTipoPagoActividad])
GO
ALTER TABLE [Configuracion].[AreasValidadoras] CHECK CONSTRAINT [FK_AreasValidadoras_TiposPagoActividades]
GO
ALTER TABLE [Configuracion].[Notificaciones]  WITH CHECK ADD  CONSTRAINT [FK_Notificaciones_TiposServicio] FOREIGN KEY([IdTipoServicio])
REFERENCES [Catalogo].[TiposServicio] ([IdTipoServicio])
GO
ALTER TABLE [Configuracion].[Notificaciones] CHECK CONSTRAINT [FK_Notificaciones_TiposServicio]
GO
ALTER TABLE [Configuracion].[ReceptoresAlertas]  WITH CHECK ADD  CONSTRAINT [FK_ReceptoresAlertas_Notificaciones] FOREIGN KEY([IdNotificacion])
REFERENCES [Configuracion].[Notificaciones] ([IdNotificacion])
GO
ALTER TABLE [Configuracion].[ReceptoresAlertas] CHECK CONSTRAINT [FK_ReceptoresAlertas_Notificaciones]
GO
ALTER TABLE [Configuracion].[ReceptoresAlertas]  WITH CHECK ADD  CONSTRAINT [FK_ReceptoresAlertas_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [Seguridad].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [Configuracion].[ReceptoresAlertas] CHECK CONSTRAINT [FK_ReceptoresAlertas_Usuarios]
GO
ALTER TABLE [Cuota].[Cuotas]  WITH CHECK ADD  CONSTRAINT [FK_Cuotas_Dependencias] FOREIGN KEY([IdDependencia])
REFERENCES [Catalogo].[Dependencias] ([IdDependencia])
GO
ALTER TABLE [Cuota].[Cuotas] CHECK CONSTRAINT [FK_Cuotas_Dependencias]
GO
ALTER TABLE [Cuota].[Cuotas]  WITH CHECK ADD  CONSTRAINT [FK_Cuotas_MedidasCobro] FOREIGN KEY([IdMedidaCobro])
REFERENCES [Catalogo].[MedidasCobro] ([IdMedidaCobro])
GO
ALTER TABLE [Cuota].[Cuotas] CHECK CONSTRAINT [FK_Cuotas_MedidasCobro]
GO
ALTER TABLE [Cuota].[Cuotas]  WITH CHECK ADD  CONSTRAINT [FK_Cuotas_Referencias] FOREIGN KEY([IdReferencia])
REFERENCES [Catalogo].[Referencias] ([IdReferencia])
GO
ALTER TABLE [Cuota].[Cuotas] CHECK CONSTRAINT [FK_Cuotas_Referencias]
GO
ALTER TABLE [Cuota].[Cuotas]  WITH CHECK ADD  CONSTRAINT [FK_Cuotas_TiposServicio] FOREIGN KEY([IdTipoServicio])
REFERENCES [Catalogo].[TiposServicio] ([IdTipoServicio])
GO
ALTER TABLE [Cuota].[Cuotas] CHECK CONSTRAINT [FK_Cuotas_TiposServicio]
GO
ALTER TABLE [Factor].[Factores]  WITH CHECK ADD  CONSTRAINT [FK_Factores_ClasificacionFactor] FOREIGN KEY([IdClasificacionFactor])
REFERENCES [Catalogo].[ClasificacionFactor] ([IdClasificacionFactor])
GO
ALTER TABLE [Factor].[Factores] CHECK CONSTRAINT [FK_Factores_ClasificacionFactor]
GO
ALTER TABLE [Factor].[Factores]  WITH CHECK ADD  CONSTRAINT [FK_Factores_MedidasCobro] FOREIGN KEY([IdMedidaCobro])
REFERENCES [Catalogo].[MedidasCobro] ([IdMedidaCobro])
GO
ALTER TABLE [Factor].[Factores] CHECK CONSTRAINT [FK_Factores_MedidasCobro]
GO
ALTER TABLE [Factor].[Factores]  WITH CHECK ADD  CONSTRAINT [FK_Factores_TiposServicio] FOREIGN KEY([IdTipoServicio])
REFERENCES [Catalogo].[TiposServicio] ([IdTipoServicio])
GO
ALTER TABLE [Factor].[Factores] CHECK CONSTRAINT [FK_Factores_TiposServicio]
GO
ALTER TABLE [Factor].[FactoresEntidadFederativa]  WITH CHECK ADD  CONSTRAINT [FK_FactoresEntidadFederativa_ClasificacionFactor] FOREIGN KEY([IdClasificadorFactor])
REFERENCES [Catalogo].[ClasificacionFactor] ([IdClasificacionFactor])
GO
ALTER TABLE [Factor].[FactoresEntidadFederativa] CHECK CONSTRAINT [FK_FactoresEntidadFederativa_ClasificacionFactor]
GO
ALTER TABLE [Factor].[FactoresLeyIngreso]  WITH CHECK ADD  CONSTRAINT [FK_FactoresLeyIngreso_Anios] FOREIGN KEY([Anio])
REFERENCES [Catalogo].[Anios] ([Anio])
GO
ALTER TABLE [Factor].[FactoresLeyIngreso] CHECK CONSTRAINT [FK_FactoresLeyIngreso_Anios]
GO
ALTER TABLE [Factor].[FactoresLeyIngreso]  WITH CHECK ADD  CONSTRAINT [FK_FactoresLeyIngreso_Meses] FOREIGN KEY([IdMes])
REFERENCES [Catalogo].[Meses] ([IdMes])
GO
ALTER TABLE [Factor].[FactoresLeyIngreso] CHECK CONSTRAINT [FK_FactoresLeyIngreso_Meses]
GO
ALTER TABLE [Factor].[FactoresMunicipio]  WITH CHECK ADD  CONSTRAINT [FK_FactoresMunicipio_FactoresMunicipio] FOREIGN KEY([IdFactor])
REFERENCES [Factor].[Factores] ([IdFactor])
GO
ALTER TABLE [Factor].[FactoresMunicipio] CHECK CONSTRAINT [FK_FactoresMunicipio_FactoresMunicipio]
GO
ALTER TABLE [Seguridad].[Controles]  WITH CHECK ADD  CONSTRAINT [FK_Controles_TiposControl] FOREIGN KEY([IdTipoControl])
REFERENCES [Seguridad].[TiposControl] ([IdTipoControl])
GO
ALTER TABLE [Seguridad].[Controles] CHECK CONSTRAINT [FK_Controles_TiposControl]
GO
ALTER TABLE [Seguridad].[Externos]  WITH CHECK ADD  CONSTRAINT [FK_Externos_TiposContacto] FOREIGN KEY([IdTipoContacto])
REFERENCES [Seguridad].[TiposContacto] ([IdTipoContacto])
GO
ALTER TABLE [Seguridad].[Externos] CHECK CONSTRAINT [FK_Externos_TiposContacto]
GO
ALTER TABLE [Seguridad].[ModulosControles]  WITH CHECK ADD  CONSTRAINT [FK_ModulosControles_Controles] FOREIGN KEY([IdControl])
REFERENCES [Seguridad].[Controles] ([IdControl])
GO
ALTER TABLE [Seguridad].[ModulosControles] CHECK CONSTRAINT [FK_ModulosControles_Controles]
GO
ALTER TABLE [Seguridad].[RolesControles]  WITH CHECK ADD  CONSTRAINT [FK_RolesControles_Controles] FOREIGN KEY([IdControl])
REFERENCES [Seguridad].[Controles] ([IdControl])
GO
ALTER TABLE [Seguridad].[RolesControles] CHECK CONSTRAINT [FK_RolesControles_Controles]
GO
ALTER TABLE [Seguridad].[RolesModulosControles]  WITH CHECK ADD  CONSTRAINT [FK_RolesModulosControles_Controles] FOREIGN KEY([IdControl])
REFERENCES [Seguridad].[Controles] ([IdControl])
GO
ALTER TABLE [Seguridad].[RolesModulosControles] CHECK CONSTRAINT [FK_RolesModulosControles_Controles]
GO
ALTER TABLE [Seguridad].[RolesModulosControles]  WITH CHECK ADD  CONSTRAINT [FK_RolesModulosControles_RolesModulos] FOREIGN KEY([IdRolModulo])
REFERENCES [Seguridad].[RolesModulos] ([IdRolModulo])
GO
ALTER TABLE [Seguridad].[RolesModulosControles] CHECK CONSTRAINT [FK_RolesModulosControles_RolesModulos]
GO
ALTER TABLE [Seguridad].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Personas] FOREIGN KEY([IdPersona])
REFERENCES [Seguridad].[Personas] ([IdPersona])
GO
ALTER TABLE [Seguridad].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Personas]
GO
/****** Object:  StoredProcedure [Catalogo].[sp_ActividadesObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_ActividadesObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	IF @Todos = @activo 
		BEGIN
			SELECT	IdActividad, 
					IdFase, 
					Nombre, 
					Descripcion, 
					SePuedeAplicarPlazo, 
					FechaInicial, 
					FechaFinal, 
					Activo,
					Paginas = 0
				FROM Catalogo.Actividades
				WHERE Activo = @activo
			
		END
	ELSE
		BEGIN
			SELECT	IdActividad, 
					IdFase, 
					Nombre, 
					Descripcion, 
					SePuedeAplicarPlazo, 
					FechaInicial, 
					FechaFinal, 
					Activo,
					Paginas	= CASE WHEN @Todos = @Zero THEN
						IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
					ELSE @Zero END
			FROM Catalogo.Actividades
			CROSS JOIN (SELECT COUNT(IdActividad) AS Total
						 FROM Catalogo.Actividades
						 WHERE Activo = @activo) AS R
			WHERE Activo = @activo
			ORDER BY IdActividad 
			OFFSET ((@pagina - 1) * @filas)  
			ROWS FETCH NEXT @filas ROWS ONLY;
		END
END




GO
/****** Object:  StoredProcedure [Catalogo].[sp_AniosObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Finaliza Actualizar-------------------------------------------


--------------------------------------------Iniciar Obtener-------------------------------------------
-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de Anios
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_AniosObtener]
	@Todos	BIT = 0,
	@pagina INT = 1,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	SELECT 
			Anio,			
			Descripcion,
			CASE WHEN @Todos = @Zero THEN
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
			ELSE @Zero END AS Paginas
			
	FROM Catalogo.Anios
	CROSS JOIN (SELECT COUNT(Anio) AS Total
			     FROM Catalogo.Anios) AS R
	ORDER BY Anio 
	OFFSET ((@pagina - 1) * @filas)  
	ROWS FETCH NEXT @filas ROWS ONLY;
END




GO
/****** Object:  StoredProcedure [Catalogo].[sp_AniosObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Finaliza ObtenerPorId-------------------------------------------

--------------------------------------------Inicia ObtenerPorCriterio-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de una Anios
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_AniosObtenerPorCriterio]
	@Anio VARCHAR(50)
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			Anio,
			Descripcion
			
	FROM Catalogo.Anios
	WHERE Anio = @Anio

END




GO
/****** Object:  StoredProcedure [Catalogo].[sp_AniosObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------Finalizar Obtener-------------------------------------------

--------------------------------------------Inicia ObtenerPorId-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de una Anios
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_AniosObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			Anio,
			Descripcion
			
	FROM Catalogo.Anios
	WHERE Anio = @Identificador

END




GO
/****** Object:  StoredProcedure [Catalogo].[sp_CentrosCostoObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_CentrosCostoObtener]
AS BEGIN
   	--EXEC Catalogo.sp_CentrosCostoObtener
	SELECT 	IdCentroCosto = trf.idCentroCosto,
			Descripcion = trf.Descripcion	
	FROM Catalogo.vwCentrosCosto AS trf
	ORDER BY trf.idCentroCosto

END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_ClasificacionFactor_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
CREATE PROCEDURE [Catalogo].[sp_ClasificacionFactor_Actualizar]

	@Identificador int,
	@Nombre varchar(50),
	@Descripcion varchar(100)
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.ClasificacionFactor
		SET 

			Nombre = @Nombre,
			Descripcion = @Descripcion
			
		WHERE IdClasificacionFactor = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdClasificacionFactor 		:', @Identificador,'
								  , Nombre	:',	@Nombre,'
								  , Descripcion	:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_ClasificacionFactor_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_ClasificacionFactor_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.ClasificacionFactor
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdClasificacionFactor = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdClasificacionFactor :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_ClasificacionFactor_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_ClasificacionFactor_Insertar]
	@Nombre varchar(50),
	@Descripcion varchar(100)

AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT into Catalogo.ClasificacionFactor
		(
			Nombre,
			Descripcion	
		) 
		values
		( 
			@Nombre,
			@Descripcion
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									Nombre	:',	@Nombre,'
								  , Descripcion		:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_ClasificacionFactor_Validar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 31/08/2017
-- Description:	Valida el registro del concepto
--				del tipo documento.
-- =============================================

CREATE PROCEDURE [Catalogo].[sp_ClasificacionFactor_Validar]
	@Identificador int,
	@Nombre varchar(50)
AS
BEGIN	
	
	DECLARE @RESULTADO					VARCHAR(100) = '';
	DECLARE @FUE_MODIFICADO_EL_REGISTRO	BIT = 1;
	DECLARE @ES_DUPLICADO				BIT = 0;
	DECLARE @TIENE_DEPENDENCIA			BIT = 0;

	-- =============================================
	-- Si no existe entonces es un registro modificado 
	-- o nuevo, si existe entonces es el mismo registro
	-- y se permite actualizar sin validar
	-- =============================================
	IF  EXISTS(SELECT * FROM Catalogo.ClasificacionFactor
				WHERE	IdClasificacionFactor = @Identificador
				AND		RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@Nombre)))
		BEGIN
			SET @FUE_MODIFICADO_EL_REGISTRO = 0;
		END
	ELSE
		BEGIN
			-- =============================================
			-- Indica si hay duplicidad en la referencia.
			-- =============================================
			IF	EXISTS(SELECT * FROM Catalogo.ClasificacionFactor
						  WHERE RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@Nombre)))
				BEGIN
					SET @ES_DUPLICADO = 1;
				END
			-- =============================================
			-- Indica si hay dependencias con otras tablas.
			-- =============================================
			IF	EXISTS(SELECT * FROM Factor.Factores
						WHERE IdClasificacionFactor = @Identificador )
				BEGIN
					SET @TIENE_DEPENDENCIA = 1;
				END
			ELSE IF EXISTS(SELECT * FROM Factor.FactoresEntidadFederativa
						WHERE IdClasificadorFactor = @Identificador )
				BEGIN
					SET @TIENE_DEPENDENCIA = 1;
				END
		END

	
	-- =============================================
	-- Reglas de negocio:
	--	1.	Si @FUE_MODIFICADO_EL_REGISTRO = 1, Ya no entra a 
	--		validar y envia el valor vacio.
	--	2.	Si @FUE_MODIFICADO_EL_REGISTRO = 0, entonces entra
	--		a la validación.
	--
	--		2.1 Si	@ES_DUPLICADO = 1 manda el
	--			el mensaje "Ya existe el clasificador del factor".	
	--
	--		2.2 Si @TIENE_DEPENDENCIA = 1 manda el 
	--			mensaje "No puede cambiar el clasificador del factor,
	--			ya que existen registros asociados al
	--			clasificador".
	-- =============================================
	IF @FUE_MODIFICADO_EL_REGISTRO = 1
		BEGIN
			IF @ES_DUPLICADO = 1
				SET @RESULTADO = 'El nombre del clasificador del factor ya existe, no se puede guardar su información.';	
			ELSE IF @TIENE_DEPENDENCIA = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'No puede cambiar la información del clasificador del factor, ya que existen registros asociados al clasificador del factor que esta editado.';
		END
		

	SELECT @RESULTADO AS Resultado
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_ClasificacionFactorObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_ClasificacionFactorObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	IF @Todos = @activo 
	BEGIN
		SELECT 
				IdClasificacionFactor,
				Nombre,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				Paginas = 0
			FROM Catalogo.ClasificacionFactor
			WHERE Activo = @activo
	END
	ELSE
	BEGIN
			SELECT 
				IdClasificacionFactor,
				Nombre,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				CASE WHEN @Todos = @Zero THEN
					IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
				ELSE @Zero END AS Paginas
			
			FROM Catalogo.ClasificacionFactor
			CROSS JOIN (SELECT COUNT(IdClasificacionFactor) AS Total
					 FROM Catalogo.ClasificacionFactor
					 WHERE Activo = @activo) AS R
			WHERE Activo = @activo
			ORDER BY IdClasificacionFactor 
			OFFSET ((@pagina - 1) * @filas)  
			ROWS FETCH NEXT @filas ROWS ONLY;
	END
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_ClasificacionFactorObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_ClasificacionFactorObtenerPorCriterio]
	@Activo BIT = NULL,
	@Identificador INT = NULL,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdClasificacionFactor,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo	
			
	FROM Catalogo.ClasificacionFactor
	WHERE Activo = @Activo AND (0 = @Identificador OR IdClasificacionFactor = @Identificador)

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_ClasificacionFactorObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_ClasificacionFactorObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdClasificacionFactor,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo	
			
	FROM Catalogo.ClasificacionFactor
	WHERE IdClasificacionFactor = @Identificador

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_ConfServicioObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_ConfServicioObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	BEGIN
		SELECT 
			   [IdConfServicio]
			  ,[IdTipoServicio]
			  ,[IdCentroCosto]
			  ,[Activo]
			  ,CASE 
					WHEN @Todos = @Zero THEN
							IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
					ELSE @Zero 
				END AS Paginas
		FROM [Configuracion].[ConfServicio]
		CROSS JOIN (SELECT COUNT(IdConfServicio) AS Total
					 FROM [Configuracion].[ConfServicio]
					 WHERE Activo = @activo) AS R
		WHERE Activo = @activo
		ORDER BY IdConfServicio 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_Cuotas_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [Catalogo].[sp_Cuotas_Actualizar]
	@Identificador INT = NULL,
	@IdTipoServicio INT = NULL,
	@IdReferencia INT = NULL,
	@IdDependencia INT = NULL,
	@Concepto varchar(1500),
	@IdJerarquia INT = NULL,
	@IdGrupoTarifario INT = NULL,
	@CuotaBase decimal(18,2),
	@IdMedidaCobro INT = NULL,
	@Iva decimal(18,2),
	@FechaAutorizacion date,
	@FechaEntradaVigor date,
	@FechaTermino date,
	@FechaPublicaDof date
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Cuotas
		SET 
			IdTipoServicio = @IdTipoServicio,
			IdReferencia = @IdReferencia,
			IdDependencia = @IdDependencia,
			Concepto = @Concepto,
			IdJerarquia = @IdJerarquia,
			IdGrupoTarifario = @IdGrupoTarifario,
			CuotaBase = @CuotaBase,
			IdMedidaCobro = @IdMedidaCobro,
			Iva = @Iva,
			FechaAutorizacion = @FechaAutorizacion,
			FechaEntradaVigor = @FechaEntradaVigor,
			FechaTermino = @FechaTermino,
			FechaPublicaDof = @FechaPublicaDof
			
		WHERE IdCuota = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdCuota 	:', @Identificador,'
									IdTipoServicio :', @IdTipoServicio,',
									IdReferencia :', @IdReferencia,',
									IdDependencia :', @IdDependencia,',
									Concepto :', @Concepto,'
									IdJerarquia :', @IdJerarquia,'
									IdGrupoTarifario :', @IdGrupoTarifario,'
									CuotaBase :', @CuotaBase,'
									IdMedidaCobro :', @IdMedidaCobro,'
									Iva :', @Iva,'
									FechaAutorizacion :', @FechaAutorizacion,'
									FechaEntradaVigor :', @FechaEntradaVigor,'
									FechaTermino :', @FechaTermino,'
									FechaPublicaDof :', @FechaPublicaDof,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END
GO
/****** Object:  StoredProcedure [Catalogo].[sp_Cuotas_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [Catalogo].[sp_Cuotas_CambiarEstatus]

	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Cuotas
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdCuota = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdCuota :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END
GO
/****** Object:  StoredProcedure [Catalogo].[sp_Cuotas_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Catalogo].[sp_Cuotas_Insertar]

@IdTipoServicio INT = NULL,
@IdReferencia INT = NULL,
@IdDependencia INT = NULL,
@Concepto varchar(1500),
@IdJerarquia INT = NULL,
@IdGrupoTarifario INT = NULL,
@CuotaBase decimal(18,2),
@IdMedidaCobro INT = NULL,
@Iva decimal(18,2),
@FechaAutorizacion date,
@FechaEntradaVigor date,
@FechaTermino date,
@FechaPublicaDof date


AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT INTO Catalogo.Cuotas
		(
			IdTipoServicio,
			IdReferencia,
			IdDependencia,
			Concepto,
			IdJerarquia,
			IdGrupoTarifario,
			CuotaBase,
			IdMedidaCobro,
			Iva,
			FechaAutorizacion,
			FechaEntradaVigor,
			FechaTermino,
			FechaPublicaDof,
			FechaInicial,
			Activo
		) 
		VALUES
		( 
			@IdTipoServicio,
			@IdReferencia,
			@IdDependencia,
			@Concepto,
			@IdJerarquia,
			@IdGrupoTarifario,
			@CuotaBase,
			@IdMedidaCobro,
			@Iva,
			@FechaAutorizacion,
			@FechaEntradaVigor,
			@FechaTermino,
			@FechaPublicaDof,
			'01/01/2001',
			1
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdTipoServicio :', @IdTipoServicio,',
									IdReferencia :', @IdReferencia,',
									IdDependencia :', @IdDependencia,',
									Concepto :', @Concepto,'
									IdJerarquia :', @IdJerarquia,',
									IdGrupoTarifario :', @IdGrupoTarifario,',
									CuotaBase :', @CuotaBase,'
									IdMedidaCobro :', @IdMedidaCobro,',
									Iva :', @Iva,',
									FechaAutorizacion :', @FechaAutorizacion,'
									FechaEntradaVigor :', @FechaEntradaVigor,',
									FechaTermino :', @FechaTermino,',
									FechaPublicaDof :', @FechaPublicaDof,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END
GO
/****** Object:  StoredProcedure [Catalogo].[sp_CuotasObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [Catalogo].[sp_CuotasObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	

	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	IF @Todos = @Zero 
	BEGIN
		SELECT 
			C.IdCuota,
			C.IdTipoServicio,
			TC.Nombre TipoServicio,
			
			C.IdReferencia,
			RC.ClaveReferencia Referencia,
			
			C.IdDependencia,
			DC.Nombre Dependencia,
			DC.Descripcion DescripcionDependencia,
			
			C.Concepto,
			
			C.IdJerarquia,
			JC.Descripcion Jerarquia,
			
			C.IdGrupoTarifario,
			GC.Descripcion GrupoTarifario,
			
			C.CuotaBase,
			
			C.IdMedidaCobro,
			MC.Nombre MedidaCobro,
			
			C.Iva,
			C.FechaAutorizacion,
			C.FechaEntradaVigor,
			C.FechaTermino,
			C.FechaPublicaDof,
			C.FechaInicial,
			C.FechaFinal,
			C.Activo,
			CASE WHEN @Todos = @Zero THEN
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
			ELSE @Zero END Paginas
			
		FROM Catalogo.Cuotas C
		INNER JOIN Catalogo.TiposServicio TC WITH(NOLOCK) ON C.IdTipoServicio = TC.IdTipoServicio
		INNER JOIN Catalogo.Referencias RC WITH(NOLOCK) ON C.IdReferencia = RC.IdReferencia
		INNER JOIN Catalogo.Dependencias DC WITH(NOLOCK) ON C.IdDependencia = DC.IdDependencia
		INNER JOIN Catalogo.vwJerarquias JC WITH(NOLOCK) ON C.IdJerarquia = JC.IdJerarquia
		INNER JOIN Catalogo.vwGruposTarifario GC WITH(NOLOCK) ON C.IdGrupoTarifario = GC.IdGrupoTarifario
		INNER JOIN Catalogo.MedidasCobro MC WITH(NOLOCK) ON C.IdMedidaCobro = MC.IdMedidaCobro			
		CROSS JOIN (SELECT COUNT(IdCuota) AS Total
					 FROM Catalogo.Cuotas
					 WHERE Activo = @activo) AS R
		WHERE C.Activo = @activo
		ORDER BY IdCuota 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
	ELSE	
	BEGIN
		SELECT 
			C.IdCuota,
			C.IdTipoServicio,
			TC.Nombre TipoServicio,
			
			C.IdReferencia,
			RC.ClaveReferencia Referencia,
			
			C.IdDependencia,
			DC.Nombre Dependencia,
			DC.Descripcion DescripcionDependencia,
			
			C.Concepto,
			
			C.IdJerarquia,
			JC.Descripcion Jerarquia,
			
			C.IdGrupoTarifario,
			GC.Descripcion GrupoTarifario,
			
			C.CuotaBase,
			
			C.IdMedidaCobro,
			MC.Nombre MedidaCobro,
			
			C.Iva,
			C.FechaAutorizacion,
			C.FechaEntradaVigor,
			C.FechaTermino,
			C.FechaPublicaDof,
			C.FechaInicial,
			C.FechaFinal,
			C.Activo,
			Paginas = 0
			
		FROM Catalogo.Cuotas C
		INNER JOIN Catalogo.TiposServicio TC WITH(NOLOCK) ON C.IdTipoServicio = TC.IdTipoServicio
		INNER JOIN Catalogo.Referencias RC WITH(NOLOCK) ON C.IdReferencia = RC.IdReferencia
		INNER JOIN Catalogo.Dependencias DC WITH(NOLOCK) ON C.IdDependencia = DC.IdDependencia
		INNER JOIN Catalogo.vwJerarquias JC WITH(NOLOCK) ON C.IdJerarquia = JC.IdJerarquia
		INNER JOIN Catalogo.vwGruposTarifario GC WITH(NOLOCK) ON C.IdGrupoTarifario = GC.IdGrupoTarifario
		INNER JOIN Catalogo.MedidasCobro MC WITH(NOLOCK) ON C.IdMedidaCobro = MC.IdMedidaCobro	
		WHERE C.Activo = @activo
		ORDER BY IdCuota 
	END

END
GO
/****** Object:  StoredProcedure [Catalogo].[sp_CuotasObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [Catalogo].[sp_CuotasObtenerPorCriterio]
	@Activo BIT = NULL,
	
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN

	SELECT 
			C.IdCuota,
			C.IdTipoServicio,
			TC.Nombre TipoServicio,
			
			C.IdReferencia,
			RC.ClaveReferencia Referencia,
			
			C.IdDependencia,
			DC.Nombre Dependencia,
			DC.Descripcion DescripcionDependencia,
			
			C.Concepto,
			
			C.IdJerarquia,
			JC.Descripcion Jerarquia,
			
			C.IdGrupoTarifario,
			GC.Descripcion GrupoTarifario,
			
			C.CuotaBase,
			
			C.IdMedidaCobro,
			MC.Nombre MedidaCobro,
			
			C.Iva,
			C.FechaAutorizacion,
			C.FechaEntradaVigor,
			C.FechaTermino,
			C.FechaPublicaDof,
			C.FechaInicial,
			C.FechaFinal,
			C.Activo
			
			
		FROM Catalogo.Cuotas C
		INNER JOIN Catalogo.TiposServicio TC WITH(NOLOCK) ON C.IdTipoServicio = TC.IdTipoServicio
		INNER JOIN Catalogo.Referencias RC WITH(NOLOCK) ON C.IdReferencia = RC.IdReferencia
		INNER JOIN Catalogo.Dependencias DC WITH(NOLOCK) ON C.IdDependencia = DC.IdDependencia
		INNER JOIN Catalogo.vwJerarquias JC WITH(NOLOCK) ON C.IdJerarquia = JC.IdJerarquia
		INNER JOIN Catalogo.vwGruposTarifario GC WITH(NOLOCK) ON C.IdGrupoTarifario = GC.IdGrupoTarifario
		INNER JOIN Catalogo.MedidasCobro MC WITH(NOLOCK) ON C.IdMedidaCobro = MC.IdMedidaCobro			
		
	WHERE C.Activo = @Activo 

END
GO
/****** Object:  StoredProcedure [Catalogo].[sp_CuotasValidarRegistro]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 31/08/2017
-- Description:	Validaciones de Negocio para la
--				actualización o inserción de un
--				registro.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_CuotasValidarRegistro]	 @Identificador		INT
															,@IdTipoServicio	INT
															,@IdReferencia		INT
															,@IdDependencia		INT
															,@Concepto			VARCHAR(1500)
															,@FechaAutorizacion DATE
															,@FechaEntradaVigor DATE
															,@FechaTermino		DATE
															,@FechaPublicaDof	DATE
											
AS
BEGIN
	DECLARE @FECHA_AUTORIZACION_COMPARACION			DATE = NULL,
			@FECHA_ENTRADAVIGOR_COMPARACION			DATE = NULL,
			@FECHA_TERMINO_COMPARACION				DATE = NULL;

	DECLARE @RESULTADO					VARCHAR(100) = '';
	DECLARE @FUE_MODIFICADO_EL_REGISTRO	BIT = 1;
	DECLARE @ES_DUPLICADO				BIT = 0;
	DECLARE @EXISTE_VALOR_SIMILAR		BIT = 0;
	DECLARE @TIENE_DEPENDENCIA			BIT = 0;

	-- =============================================
	-- Si no existe entonces es un registro modificado 
	-- o nuevo, si existe entonces es el mismo registro
	-- y se permite actualizar sin validar
	-- =============================================
	IF  EXISTS(SELECT * FROM Catalogo.Cuotas 
				WHERE	IdCuota 			= @Identificador
				AND		IdTipoServicio		= @IdTipoServicio 
				AND		IdReferencia		= @IdReferencia 
				AND		IdDependencia		= @IdDependencia 
				AND		LTRIM(RTRIM(Concepto))				= LTRIM(RTRIM(@Concepto))
				AND		CONVERT(DATE,FechaAutorizacion,103)	= CONVERT(DATE,@FechaAutorizacion,103)
				AND		CONVERT(DATE,FechaEntradaVigor,103)	= CONVERT(DATE,@FechaEntradaVigor,103)
				AND		CONVERT(DATE,FechaTermino,103)		= CONVERT(DATE,@FechaTermino,103)
				AND		CONVERT(DATE,FechaPublicaDof,103)	= CONVERT(DATE,@FechaPublicaDof,103)
				)
		BEGIN
			SET @FUE_MODIFICADO_EL_REGISTRO = 0;
		END
	ELSE
		BEGIN

			SELECT  @FECHA_AUTORIZACION_COMPARACION	= FechaAutorizacion,
					@FECHA_ENTRADAVIGOR_COMPARACION = FechaEntradaVigor,
					@FECHA_TERMINO_COMPARACION		= FechaTermino
			FROM	Catalogo.Cuotas 
			WHERE	IdCuota = @Identificador

			-- =============================================
			-- Consulta los registros que coincidan con los 
			-- valores de los campos indispensables para 
			-- generar la unicidad.
			-- =============================================
			IF EXISTS(SELECT * FROM Catalogo.Cuotas 
						WHERE	IdTipoServicio		= @IdTipoServicio 
						AND		IdReferencia		= @IdReferencia 
						AND		IdDependencia		= @IdDependencia 
						AND		LTRIM(RTRIM(Concepto))				= LTRIM(RTRIM(@Concepto))
						AND		CONVERT(DATE,FechaPublicaDof,103)	= CONVERT(DATE,@FechaPublicaDof,103))
				BEGIN
					SET @EXISTE_VALOR_SIMILAR = 1;
				END

			-- =============================================
			-- Consulta los registros que coincidan con el
			-- valor del concepto de la cuota.
			-- =============================================
			IF EXISTS(SELECT * FROM Catalogo.Cuotas 
					  WHERE IdCuota <> @Identificador
					  AND LTRIM(RTRIM(Concepto))	= LTRIM(RTRIM(@Concepto)))
				BEGIN
					SET @ES_DUPLICADO = 1;
				END
			-- =============================================
			-- Aún no estan registradas las dependencias
			-- 
			--
			-- =============================================
			/*
			IF EXISTS(SELECT * FROM [Catalogo].[Fracciones] 
						WHERE Nombre = LTRIM(RTRIM(@Nombre)) 
						AND IdGrupo = @IdGrupo )
			*/
		END

	-- =============================================
	-- Reglas de negocio:
	--	1.	Si @FUE_MODIFICADO_EL_REGISTRO = 1, Ya no entra a 
	--		validar y envia el valor vacio.
	--	2.	Si @FUE_MODIFICADO_EL_REGISTRO = 0, entonces entra
	--		a la validación.
	--
	--		2.1	Si  @EXISTE_VALOR_SIMILAR = 1, manda 
	--			el mensaje "Ya existe un valor similar".
	--
	--		2.2 Si	@ES_DUPLICADO = 1 manda el
	--			el mensaje "Ya existe la cuota".	
	--
	--		2.3 Si @TIENE_DEPENDENCIA = 1 manda el 
	--			mensaje "No puede cambiar la fracción,
	--			ya que existen registros asociados a
	--			la fracción".
	-- =============================================

	IF @FUE_MODIFICADO_EL_REGISTRO = 1
		BEGIN
			IF	 @FECHA_AUTORIZACION_COMPARACION IS NOT NULL AND @FechaAutorizacion <= @FECHA_AUTORIZACION_COMPARACION
				SET @RESULTADO = 'La fecha de autorización no es mayor a la fecha registrada con anterioridad, no se puede guardar su información.';
			ELSE IF @FECHA_ENTRADAVIGOR_COMPARACION IS NOT NULL AND @FechaEntradaVigor <= @FECHA_ENTRADAVIGOR_COMPARACION AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'La fecha de entrada en vigor no es mayor a la fecha registrada con anterioridad, no se puede guardar su información.';
			ELSE IF @FECHA_TERMINO_COMPARACION IS NOT NULL AND @FechaTermino<= @FECHA_TERMINO_COMPARACION AND LEN(@RESULTADO) = 0 
				SET @RESULTADO = 'La fecha de termino no es mayor a la fecha registrada con anterioridad, no se puede guardar su información.';
			ELSE IF @FECHA_TERMINO_COMPARACION IS NOT NULL AND DATEDIFF(DAY,@FECHA_TERMINO_COMPARACION, @FechaEntradaVigor ) = -1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'La fecha de entrada en vigor no es un día menor a la fecha de termino registrado previamente, no se puede guardar su información.';
			ELSE IF @EXISTE_VALOR_SIMILAR = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'Existen una o más cuotas con los datos que ha editado, no se puede guardar su información.';
			ELSE IF @ES_DUPLICADO = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'El nombre de la cuota ya existe, no se puede guardar su información.';	
			ELSE IF @TIENE_DEPENDENCIA = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'No puede cambiar la información de la cuota, ya que existen registros asociados a la cuota.';
		END

    -- Insert statements for procedure here
	SELECT @RESULTADO AS Resultado
END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_Dependencias_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_Dependencias_Actualizar]

	@Identificador int,
	@Nombre varchar(50),
	@Descripcion varchar(100)
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Dependencias
		SET 

			Nombre= @Nombre,
			Descripcion = @Descripcion
			
		WHERE IdDependencia = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdDependencia 		:', @Identificador,'
								  , Nombre	:',	@Nombre,'
								  , Descripcion		:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_Dependencias_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_Dependencias_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Dependencias
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdDependencia = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdDependencia :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_Dependencias_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_Dependencias_Insertar]
	@Nombre varchar(50),
	@Descripcion varchar(100)

AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT INTO Catalogo.Dependencias
		(
			Nombre,
			Descripcion	
		) 
		VALUES
		( 
			@Nombre,
			@Descripcion
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									Nombre	:',	@Nombre,'
								  , Descripcion		:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_Dependencias_Validar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 31/08/2017
-- Description:	Valida el registro de la 
--				dependencia.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_Dependencias_Validar]
	@Identificador int,
	@Nombre varchar(50)
AS
BEGIN	
	DECLARE @RESULTADO					VARCHAR(100) = '';
	DECLARE @FUE_MODIFICADO_EL_REGISTRO	BIT = 1;
	DECLARE @ES_DUPLICADO				BIT = 0;
	DECLARE @TIENE_DEPENDENCIA			BIT = 0;

	-- =============================================
	-- Si no existe entonces es un registro modificado 
	-- o nuevo, si existe entonces es el mismo registro
	-- y se permite actualizar sin validar
	-- =============================================
	IF  EXISTS(SELECT * FROM Catalogo.Dependencias
				WHERE	IdDependencia = @Identificador
				AND		RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@Nombre)))
		BEGIN
			SET @FUE_MODIFICADO_EL_REGISTRO = 0;
		END
	ELSE
		BEGIN
			
			-- =============================================
			-- Indica si hay duplicidad en la nombre.
			-- =============================================
			IF	EXISTS(SELECT * FROM Catalogo.Dependencias
						  WHERE RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@Nombre)))
				BEGIN
					SET @ES_DUPLICADO = 1;
				END
			-- =============================================
			-- Indica si hay dependencias con otras tablas.
			-- =============================================
			IF	EXISTS(SELECT * FROM Catalogo.Cuotas
						WHERE IdDependencia = @Identificador )
				BEGIN
					SET @TIENE_DEPENDENCIA = 1;
				END

		END

	
	-- =============================================
	-- Reglas de negocio:
	--	1.	Si @FUE_MODIFICADO_EL_REGISTRO = 1, Ya no entra a 
	--		validar y envia el valor vacio.
	--	2.	Si @FUE_MODIFICADO_EL_REGISTRO = 0, entonces entra
	--		a la validación.
	--
	--		2.1 Si	@ES_DUPLICADO = 1 manda el
	--			el mensaje "Ya existe la dependencia".	
	--
	--		2.2 Si @TIENE_DEPENDENCIA = 1 manda el 
	--			mensaje "No se puede cambiar el nombre la dependencia,
	--			ya que existen registros asociados a la
	--			misma".
	-- =============================================
	IF @FUE_MODIFICADO_EL_REGISTRO = 1
		BEGIN
			IF @ES_DUPLICADO = 1
				SET @RESULTADO = 'El nombre de la dependencia ya existe, no se puede guardar su información.';	
			ELSE IF @TIENE_DEPENDENCIA = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'No guardar la información de la dependencia que ha editado, ya que existen registros asociados al nombre de la dependencia anterior.';
		END
		

	SELECT @RESULTADO AS Resultado
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_DependenciasObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_DependenciasObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0
	IF @Todos = @Zero 
	BEGIN
		SELECT 
				IdDependencia,
				Nombre,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)	AS Paginas	
			
		FROM Catalogo.Dependencias
		CROSS JOIN (SELECT COUNT(IdDependencia) AS Total
					 FROM Catalogo.Dependencias
					 WHERE Activo = @activo) AS R
		WHERE Activo = @activo
		ORDER BY IdDependencia 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
	ELSE
	BEGIN
		SELECT 
				IdDependencia,
				Nombre,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				Paginas = @Zero 			
		FROM Catalogo.Dependencias		
		WHERE Activo = @activo
	END
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_DependenciasObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_DependenciasObtenerPorCriterio]
	@Activo BIT = NULL,
	@IdDependencia INT = NULL,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdDependencia,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo
			
	FROM Catalogo.Dependencias
	WHERE Activo = @Activo AND (0 = @IdDependencia OR IdDependencia = @IdDependencia)

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_DependenciasObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_DependenciasObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdDependencia,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo	
			
	FROM Catalogo.Dependencias
	WHERE IdDependencia = @Identificador

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_Divisiones_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_Divisiones_Actualizar]

	@Identificador int,
	@NombreDivision varchar(50),
	@DescDivision varchar(100)
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Divisiones
		SET 

			Nombre  = @NombreDivision,
			Descripcion 	= @DescDivision
			
		WHERE IdDivision = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdDivision 		:', @Identificador,'
								  , NombreDivision	:',	@NombreDivision,'
								  , DescDivision	:', @DescDivision,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_Divisiones_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_Divisiones_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Divisiones
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdDivision = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdDivision	:',@Identificador,'
								  , Activo		:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_Divisiones_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_Divisiones_Insertar]
    
    
	@NombreDivision varchar(50),
	@DescDivision varchar(100)

AS
BEGIN	
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT into Catalogo.Divisiones
		(
			Nombre,
			Descripcion		
		) 
		values
		( 
			@NombreDivision,
			@DescDivision
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									NombreDivision	:',	@NombreDivision,'
								  , DescDivision	:', @DescDivision,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_Divisiones_ValidarRegistro]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 01/09/2017
-- Description:	Valida el registro de la división.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_Divisiones_ValidarRegistro]
	@Identificador int,
	@NombreDivision varchar(50),
	@DescDivision varchar(100)
AS
BEGIN	
	DECLARE @RESULTADO					VARCHAR(100) = '';
	DECLARE @FUE_MODIFICADO_EL_REGISTRO	BIT = 1;
	DECLARE @ES_DUPLICADO				BIT = 0;
	DECLARE @TIENE_DEPENDENCIA			BIT = 0;

	-- =============================================
	-- Si no existe entonces es un registro modificado 
	-- o nuevo, si existe entonces es el mismo registro
	-- y se permite actualizar sin validar
	-- =============================================
	IF  EXISTS(SELECT * FROM Catalogo.Divisiones
				WHERE	IdDivision = @Identificador
				AND		RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@NombreDivision)))
		BEGIN
			SET @FUE_MODIFICADO_EL_REGISTRO = 0;
		END
	ELSE
		BEGIN
			-- =============================================
			-- Consulta los registros que coincidan con el
			-- nombre de la división.
			-- =============================================
			IF	EXISTS(SELECT * FROM Catalogo.Divisiones
					   WHERE RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@NombreDivision)))
				BEGIN
					SET @ES_DUPLICADO = 1;
				END
			-- =============================================
			-- Indica si hay dependencias con otras tablas.
			-- =============================================
			IF	EXISTS(SELECT * FROM Catalogo.Grupos
					   WHERE IdDivision = @Identificador)
				BEGIN
					SET @TIENE_DEPENDENCIA = 1;
				END
		END

	-- =============================================
	-- Reglas de negocio:
	--	1.	Si @FUE_MODIFICADO_EL_REGISTRO = 1, Ya no entra a 
	--		validar y envia el valor vacio.
	--	2.	Si @FUE_MODIFICADO_EL_REGISTRO = 0, entonces entra
	--		a la validación.
	--
	--		2.1 Si	@ES_DUPLICADO = 1 manda el
	--			el mensaje "Ya existe la nombre".	
	--
	--		2.2 Si @TIENE_DEPENDENCIA = 1 manda el 
	--			mensaje "No puede cambiar la división,
	--			ya que existen registros asociados a
	--			la división".
	-- =============================================
	IF @FUE_MODIFICADO_EL_REGISTRO = 1
		BEGIN
			IF @ES_DUPLICADO = 1
				SET @RESULTADO = 'El nombre de la división ya existe, no se puede guardar su información.';	
			ELSE IF @TIENE_DEPENDENCIA = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'No puede cambiar la información de la división, ya que existen registros asociados a la división que desea editar.';
		END
		

	SELECT @RESULTADO AS Resultado
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_DivisionesObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_DivisionesObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	BEGIN
		SELECT 
				IdDivision,
				Nombre,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				CASE WHEN @Todos = @Zero THEN
					IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
				ELSE @Zero END AS Paginas
			
		FROM Catalogo.Divisiones
		CROSS JOIN (SELECT COUNT(IdDivision) AS Total
					 FROM Catalogo.Divisiones
					 WHERE Activo = @activo) AS R
		WHERE Activo = @activo
		ORDER BY IdDivision 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_DivisionesObtenerListado]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 27/08/2017
-- Description:	Obtiene la informacion de Divisiones
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_DivisionesObtenerListado]
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdDivision,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo
	FROM Catalogo.Divisiones
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_DivisionesObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_DivisionesObtenerPorCriterio]
	@Activo BIT = NULL,
	@IdDivision INT = NULL,
	@pagina INT = 1,
	@filas  INT = 20
AS
BEGIN

	SET NOCOUNT ON;
	SELECT 
			IdDivision,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo
			
	FROM Catalogo.Divisiones
	WHERE Activo = @Activo AND (0 = @IdDivision OR IdDivision = @IdDivision)
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_DivisionesObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_DivisionesObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdDivision,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo	
			
	FROM Catalogo.Divisiones
	WHERE IdDivision = @Identificador

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_FactoresEntidadFederativaObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_FactoresEntidadFederativaObtenerPorCriterio]
	@Activo BIT,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
		IdFactEntidFed,
		IdClasificadorFactor,
		IdFactor,
		Descripcion,
		IdEntidFed,
		FechaInicial,
		FechaFinal,
		Activo
			
	FROM Factor.FactoresEntidadFederativa
	WHERE Activo = @Activo

END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_FraccionesActualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		INDRA - Lucio Arturo Juárez Hernández
-- Create date: 22/08/2017
-- Description:	Procedimiento que inserta un registro de la tabla fracciones.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_FraccionesActualizar]
	 @Identificador  Int
	,@Nombre varchar(1500)
	,@Descripcion varchar(1500)
	,@IdGrupo int
	,@FechaFinal date
AS
BEGIN
	DECLARE @result INT = 0;

	BEGIN TRY
		BEGIN TRANSACTION

			UPDATE [Catalogo].[Fracciones]
			SET
			    [Nombre]        = @Nombre
			   ,[Descripcion]   = @Descripcion
			   ,[IdGrupo]       = @IdGrupo
			   ,[FechaFinal]    = @FechaFinal
			WHERE [IdFraccion]	= @Identificador

		COMMIT TRANSACTION
		SET @result = @Identificador
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { IdFraccion:',CONVERT(VARCHAR(20), @Identificador),'
									Nombre:',	@Nombre,'
								  , Descripcion:', @Descripcion,'
								  , IdGrupo:', CONVERT(VARCHAR(20),@IdGrupo) ,'
								  , FechaFinal:', CONVERT(VARCHAR(20),@FechaFinal,103),'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);		

	END CATCH

	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_FraccionesCambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Lucio Arturo Juarez
-- Create date: 22/08/2017 
-- Description:	Cambia el estado de un registro a activo o desactivo.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_FraccionesCambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	
	BEGIN TRAN
		BEGIN TRY  
		
			UPDATE Catalogo.Fracciones 
			SET FechaFinal = GETDATE(), Activo = @Activo			
			WHERE IdFraccion  = @Identificador

			COMMIT TRAN
			SET @result = @Identificador
		END TRY  
	BEGIN CATCH  
		ROLLBACK TRAN
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdFraccion :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);		
	END CATCH;  
	RETURN @result
END



GO
/****** Object:  StoredProcedure [Catalogo].[sp_FraccionesInsertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		INDRA - Lucio Arturo Juárez Hernández
-- Create date: 22/08/2017
-- Description:	Procedimiento que inserta un registro de la tabla fracciones.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_FraccionesInsertar]
	 @Nombre varchar(1500)
	,@Descripcion varchar(1500)
	,@IdGrupo int
	,@FechaInicial date
	,@FechaFinal date
	,@Activo bit
AS
BEGIN
	DECLARE @result			INT = 0;
	DECLARE @Identificador	INT = 0;

	BEGIN TRY
		BEGIN TRANSACTION
			
			SET @Identificador = (SELECT ISNULL(MAX([IdFraccion]),0) FROM  [Catalogo].[Fracciones]) + 1 ;


			INSERT INTO [Catalogo].[Fracciones]
			   ([IdFraccion]
			   ,[Nombre]
			   ,[Descripcion]
			   ,[IdGrupo]
			   ,[FechaInicial]
			   ,[FechaFinal]
			   ,[Activo])
			VALUES
			   ( @Identificador 
			    ,@Nombre
				,@Descripcion
				,@IdGrupo
				,@FechaInicial
				,@FechaFinal
				,@Activo)

		COMMIT TRANSACTION
		SET @result = SCOPE_IDENTITY()
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									Nombre:',	@Nombre,'
								  , Descripcion:', @Descripcion,'
								  , IdGrupo:', CONVERT(VARCHAR(20),@IdGrupo) ,'
								  , FechaInicial:', CONVERT(VARCHAR(20), @FechaInicial,103),'
								  , FechaFinal:', CONVERT(VARCHAR(20),@FechaFinal,103),'
								  , Activo:', CONVERT(VARCHAR(2),@Activo),'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);		

	END CATCH

	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_FraccionesObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		INDRA-Lucio Arturo Juárez Hernández
-- Create date: 22/08/2017
-- Description:	Consulta de los campos de la tabla [Catalogo].[Fracciones]
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_FraccionesObtener] 	@pagina INT = 1, @filas  INT
AS
BEGIN
		SET NOCOUNT ON;

		SELECT 
			 FCN.[IdFraccion]
			,FCN.[Nombre]
			,FCN.[Descripcion]
			,FCN.[IdGrupo]
			,GPO.[IdDivision] 
			,FCN.[FechaInicial]
			,FCN.[FechaFinal]
			,FCN.[Activo]
			,ISNULL(GPO.Nombre,'') AS Grupo
			,ISNULL(DIV.Nombre,'') AS Division
		FROM [Catalogo].[Fracciones]		AS FCN
		LEFT JOIN [Catalogo].[Grupos]		AS GPO ON FCN.IdGrupo = GPO.IdGrupo
		LEFT JOIN [Catalogo].[Divisiones]	AS DIV ON GPO.IdDivision = DIV.IdDivision
		ORDER BY FCN.IdFraccion OFFSET ((@pagina - 1) * @filas) ROWS FETCH NEXT @filas ROWS ONLY; 
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_FraccionesObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_FraccionesObtenerPorCriterio]
	@Activo BIT = NULL,
	@pagina INT = 1,
	@filas  INT = 20
AS
BEGIN

	SELECT 
			 FCN.[IdFraccion]
			,FCN.[Nombre]
			,FCN.[Descripcion]
			,FCN.[IdGrupo]
			,GPO.[IdDivision] 
			,FCN.[FechaInicial]
			,FCN.[FechaFinal]
			,FCN.[Activo]
			,ISNULL(GPO.Nombre,'') AS Grupo
			,ISNULL(DIV.Nombre,'') AS Division
		FROM [Catalogo].[Fracciones]		AS FCN
		LEFT JOIN [Catalogo].[Grupos]		AS GPO ON FCN.IdGrupo = GPO.IdGrupo
		LEFT JOIN [Catalogo].[Divisiones]	AS DIV ON GPO.IdDivision = DIV.IdDivision
		WHERE FCN.Activo = ISNULL(@Activo,FCN.Activo)
		ORDER BY FCN.IdFraccion OFFSET ((@pagina - 1) * @filas) ROWS FETCH NEXT @filas ROWS ONLY; 
	
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_FraccionesValidarRegistro]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		INDRA - Lucio Arturo Juárez Hernández
-- Create date: 29/08/2017
-- Description:	Procedimiento que inserta un registro de la tabla fracciones.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_FraccionesValidarRegistro]
	 (@Identificador int
	 ,@Nombre varchar(1500)
	 ,@IdGrupo int)
AS
BEGIN
	DECLARE @RESULTADO						VARCHAR(100) = '';
	DECLARE @FUE_MODIFICADO_EL_REGISTRO		BIT = 1;
	DECLARE @EXISTE_VALOR_SIMILAR			BIT = 0;
	DECLARE @EXISTE_LA_FRACCION				BIT = 0;
	DECLARE @TIENE_DEPENDENCIA				BIT = 0;
	-- =============================================
	-- Si no existe entonces es un registro modificado 
	-- o nuevo, si existe entonces es el mismo registro
	-- y se permite actualizar sin validar
	-- =============================================
	IF  EXISTS(SELECT * FROM [Catalogo].[Fracciones] 
				WHERE Nombre		= LTRIM(RTRIM(@Nombre)) 
				AND	  IdGrupo		= @IdGrupo
				AND	  IdFraccion	= @Identificador)
		BEGIN
			SET @FUE_MODIFICADO_EL_REGISTRO = 0;
		END
	ELSE
		BEGIN
			IF  EXISTS(SELECT * FROM [Catalogo].[Fracciones] 
						  WHERE Nombre		= LTRIM(RTRIM(@Nombre)) 
						  AND	IdGrupo		= @IdGrupo)
				BEGIN
					SET @EXISTE_VALOR_SIMILAR = 1;
				END	
			
			IF	EXISTS(SELECT * FROM [Catalogo].[Fracciones] 
						  WHERE Nombre		= LTRIM(RTRIM(@Nombre))
						  AND	IdFraccion	<> @Identificador)
				BEGIN
					SET @EXISTE_LA_FRACCION = 1;
				END
		END

	-- =============================================
	-- Aún no estan registradas las dependencias
	-- 
	--
	-- =============================================
	/*
	IF EXISTS(SELECT * FROM [Catalogo].[Fracciones] 
				WHERE Nombre = LTRIM(RTRIM(@Nombre)) 
				AND IdGrupo = @IdGrupo )
	*/

	-- =============================================
	-- Reglas de negocio:
	--	1.	Si @FUE_MODIFICADO_EL_REGISTRO = 1, Ya no entra a 
	--		validar y envia el valor vacio.
	--	2.	Si @FUE_MODIFICADO_EL_REGISTRO = 0, entonces entra
	--		a la validación.
	--
	--		2.1	Si  @EXISTE_VALOR_SIMILAR = 1, manda 
	--			el mensaje "Ya existe un valor similar".
	--
	--		2.2 Si	@EXISTE_LA_FRACCION = 1 manda el
	--			el mensaje "Ya existe la fracción".	
	--
	--		2.3 Si @TIENE_DEPENDENCIA = 1 manda el 
	--			mensaje "No puede cambiar la fracción,
	--			ya que existen registros asociados a
	--			la fracción".
	-- =============================================
	IF @FUE_MODIFICADO_EL_REGISTRO = 1
		BEGIN
			IF @EXISTE_LA_FRACCION = 1
				SET @RESULTADO = 'El nombre de la fracción ya existe, no se puede guardar su información.';	
			ELSE IF @EXISTE_VALOR_SIMILAR = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'Existen una o más fracciones con los datos que ha editado, no se puede guardar su información.';
			ELSE IF @TIENE_DEPENDENCIA = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'No puede cambiar la información de la fracción, ya que existen registros asociados a la fracción';
		END
		
	SELECT @RESULTADO AS Resultado
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_FraccionObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		INDRA-Lucio Arturo Juárez Hernández
-- Create date: 22/08/2017
-- Description:	Consulta de los campos de la tabla [Catalogo].[Fracciones]
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_FraccionObtenerPorId] 	@Identificador  INT
AS
BEGIN
		SET NOCOUNT ON;

		SELECT 
			 FCN.[IdFraccion]
			,FCN.[Nombre]
			,FCN.[Descripcion]
			,FCN.[IdGrupo]
			,GPO.[IdDivision]
			,FCN.[FechaInicial]
			,FCN.[FechaFinal]
			,FCN.[Activo]
			,ISNULL(GPO.Nombre,'') AS Grupo
			,ISNULL(DIV.Nombre,'') AS Division
		FROM [Catalogo].[Fracciones]		AS FCN
		LEFT JOIN [Factor].[Factores]		AS FCT ON FCN.IdFraccion = FCT.IdFactor
		LEFT JOIN [Catalogo].[Grupos]		AS GPO ON FCN.IdGrupo = GPO.IdGrupo
		LEFT JOIN [Catalogo].[Divisiones]	AS DIV ON GPO.IdDivision = DIV.IdDivision
		WHERE FCN.IdFraccion = @Identificador
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_GastosInherentes_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Oscar Maya
-- Create date: 03/08/2017
-- Description:	Actualiza la informacion de un registro de TiposServicio para la actividad económica.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_GastosInherentes_Actualizar]
(
	@Identificador	INT,
	@Nombre VARCHAR(50),
	@Descripcion	VARCHAR(100)
	)
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANTiposServicio
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.GastosInherentes
		SET Nombre = @Nombre,
			Descripcion = @Descripcion
		WHERE IdGastoInherente = @Identificador
		-- COMMIT TRANTiposServicio
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									identificador:', @Identificador,
								  ' Nombre :',@Nombre,
								  ' Descripcion:', @Descripcion,
								  '}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END





GO
/****** Object:  StoredProcedure [Catalogo].[sp_GastosInherentes_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Oscar Maya
-- Create date: 03/08/2017
-- Description:	Cambia el estado de un registro a activo o desactivo.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_GastosInherentes_CambiarEstatus]
(
	@Identificador	INT,
	@Activo bit
	)
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANTiposServicio
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.GastosInherentes
		SET FechaFinal = GETDATE(),
		Activo = @Activo
		WHERE IdGastoInherente = @Identificador

		-- COMMIT TRANTiposServicio
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { identificador:', @Identificador,
									'Activo:', @Activo,
									'}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END





GO
/****** Object:  StoredProcedure [Catalogo].[sp_GastosInherentes_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Oscar Maya
-- Create date: 03/08/2017
-- Description:	Inserta la informacion de un registro de la TiposServicio para la actividad económica.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_GastosInherentes_Insertar]
	@Nombre VARCHAR(50),
	@Descripcion VARCHAR(100)
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANTiposServicio
	BEGIN TRAN

	BEGIN TRY
			
		INSERT INTO Catalogo.GastosInherentes 
		(
			Nombre, 
			Descripcion
		) 
		VALUES
		( 
			@Nombre, 
			@Descripcion
		) 
		--	COMMIT TRANTiposServicio
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									 Nombre :',@Nombre,
								  ', Descripcion:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END





GO
/****** Object:  StoredProcedure [Catalogo].[sp_GastosInherentes_ObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Catalogo].[sp_GastosInherentes_ObtenerPorCriterio]
	@Activo BIT,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdGastoInherente,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo	
			
	FROM Catalogo.GastosInherentes
	WHERE Activo = @Activo

END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_GastosInherentes_ObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------- GASTOS INHERENTES --------------------
-- =============================================
-- Author:		Oscar Maya
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de una TiposServicio
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_GastosInherentes_ObtenerPorId]
(
	@Identificador INT
)
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
	
		IdGastoInherente, 
		Nombre, 
		Descripcion, 
		FechaInicial, 
		FechaFinal, 
		Activo
		
	FROM Catalogo.GastosInherentes
	WHERE IdGastoInherente = @Identificador

END





GO
/****** Object:  StoredProcedure [Catalogo].[sp_GastosInherentes_ObtenerTodos]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Oscar Maya
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de TiposServicio
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_GastosInherentes_ObtenerTodos]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0
	IF @Todos = @Zero 
	BEGIN
		SELECT 
				IdGastoInherente,
				Nombre,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)	AS Paginas	
			
		FROM Catalogo.GastosInherentes
		CROSS JOIN (SELECT COUNT(IdGastoInherente) AS Total
					 FROM Catalogo.GastosInherentes
					 WHERE Activo = @activo) AS R
		WHERE Activo = @activo
		ORDER BY IdGastoInherente 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
	ELSE
	BEGIN
		SELECT 
				IdGastoInherente,
				Nombre,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				Paginas = @Zero 			
		FROM Catalogo.GastosInherentes		
		WHERE Activo = @activo
	END
END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_Grupos_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_Grupos_Actualizar]
	@Identificador INT = NULL,
	@IdDivision INT = NULL,
	@Nombre VARCHAR(50) = NULL,
	@Descripcion VARCHAR(100) = NULL
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Grupos
		SET 
			IdDivision = @IdDivision,
			Nombre = @Nombre,
			Descripcion = @Descripcion
			
		WHERE IdGrupo = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdDivision 		:', @Identificador,'
								  , Nombre			:',	@Nombre,'
								  , Descripcion		:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_Grupos_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_Grupos_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Grupos
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdGrupo = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdGrupo		:',@Identificador,'
								  , Activo		:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_Grupos_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_Grupos_Insertar]   
    @IdDivision INT = NULL,
	@Nombre VARCHAR(50) = NULL,
	@Descripcion VARCHAR(100) = NULL

AS
BEGIN	
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT INTO Catalogo.Divisiones
		(
			IdDivision,
			Nombre,
			Descripcion		
		) 
		VALUES
		(
			@IdDivision,
			@Nombre,
			@Descripcion
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdDivision		:',	@IdDivision,'
									Nombre			:',	@Nombre,'
								  , Descripcion		:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_Grupos_ValidarRegistro]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	INDRA - Lucio Arturo Juarez Hernandez
-- Create date: 01/09/2017
-- Description:	Valida el registro del grupo.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_Grupos_ValidarRegistro]
	@Identificador INT = NULL,
	@IdDivision INT = NULL,
	@Nombre VARCHAR(50) = NULL
AS
BEGIN	
	DECLARE @RESULTADO						VARCHAR(100) = '';
	DECLARE @FUE_MODIFICADO_EL_REGISTRO		BIT = 1;
	DECLARE @EXISTE_VALOR_SIMILAR			BIT = 0;
	DECLARE @ESDUPLICADO					BIT = 0;
	DECLARE @TIENE_DEPENDENCIA				BIT = 0;
	-- =============================================
	-- Si no existe entonces es un registro modificado 
	-- o nuevo, si existe entonces es el mismo registro
	-- y se permite actualizar sin validar
	-- =============================================
	IF  EXISTS(SELECT * FROM Catalogo.Grupos 
				WHERE	Nombre		= LTRIM(RTRIM(@Nombre))
				AND		IdDivision	= @IdDivision 
				AND		IdGrupo		= @Identificador)
		BEGIN
			SET @FUE_MODIFICADO_EL_REGISTRO = 0;
		END
	ELSE
		BEGIN
			-- =============================================
			-- Consulta los registros que coincidan con los 
			-- valores de los campos indispensables para 
			-- generar la unicidad.
			-- =============================================
			IF  EXISTS(SELECT * FROM Catalogo.Grupos 
						  WHERE Nombre	= LTRIM(RTRIM(@Nombre)) 
						  AND	IdDivision	= @IdDivision)
				BEGIN
					SET @EXISTE_VALOR_SIMILAR = 1;
				END	
			-- =============================================
			-- Consulta los registros que coincidan con el
			-- valor del concepto de la cuota.
			-- =============================================
			IF	EXISTS(SELECT * FROM Catalogo.Grupos 
						  WHERE Nombre		= LTRIM(RTRIM(@Nombre)))
				BEGIN
					SET @ESDUPLICADO = 1;
				END
			-- =============================================
			-- Indica si hay dependencias con otras tablas.
			-- =============================================
			IF	EXISTS(SELECT * FROM Catalogo.Fracciones
						WHERE IdGrupo = @Identificador )
				BEGIN
					SET @TIENE_DEPENDENCIA = 1;
				END
		END

	

	-- =============================================
	-- Reglas de negocio:
	--	1.	Si @FUE_MODIFICADO_EL_REGISTRO = 1, Ya no entra a 
	--		validar y envia el valor vacio.
	--	2.	Si @FUE_MODIFICADO_EL_REGISTRO = 0, entonces entra
	--		a la validación.
	--
	--		2.1	Si  @EXISTE_VALOR_SIMILAR = 1, manda 
	--			el mensaje "Ya existe un valor similar".
	--
	--		2.2 Si	@ESDUPLICADO = 1 manda el
	--			el mensaje "Ya existe el grupo".	
	--
	--		2.3 Si @TIENE_DEPENDENCIA = 1 manda el 
	--			mensaje "No puede cambiar el nombre
	--			del grupo, ya que existen registros 
	--			asociados al grupo".
	-- =============================================
	IF @FUE_MODIFICADO_EL_REGISTRO = 1
		BEGIN
			IF @ESDUPLICADO = 1
				SET @RESULTADO = 'El nombre del grupo ya existe, no se puede guardar su información.';	
			ELSE IF @EXISTE_VALOR_SIMILAR = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'Existe un grupo con los datos que ha editado, no se puede guardar su información.';
			ELSE IF @TIENE_DEPENDENCIA = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'No puede cambiar la información del grupo, ya que existen registros asociados al grupo.';
		END
		
	SELECT @RESULTADO AS Resultado
END
GO
/****** Object:  StoredProcedure [Catalogo].[sp_GruposObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Catalogo].[sp_GruposObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	BEGIN
		SELECT 
			G.IdGrupo,
			G.IdDivision,
			D.Nombre Division,
			G.Nombre,
			G.Descripcion,
			G.FechaInicial,
			G.FechaFinal,
			G.Activo,
			CASE WHEN @Todos = @Zero THEN
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
			ELSE @Zero END AS Paginas
			
		FROM Catalogo.Grupos G
		INNER JOIN Catalogo.Divisiones D WITH(NOLOCK) ON G.IdDivision = D.IdDivision
		CROSS JOIN (SELECT COUNT(IdGrupo) AS Total
					 FROM Catalogo.Grupos
					 WHERE Activo = @activo) AS R
		WHERE G.Activo = @activo
		ORDER BY G.IdGrupo 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_GruposObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_GruposObtenerPorCriterio]
	@Activo BIT = NULL,
	@IdGrupo INT = NULL,
	@IdDivision INT = NULL,
	@pagina INT = 1,
	@filas  INT = 20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT
	
		G.IdGrupo,
		G.IdDivision,
		D.Nombre Division,
		G.Nombre,
		G.Descripcion,
		G.FechaInicial,
		G.FechaFinal,
		G.Activo
			
	FROM Catalogo.Grupos G
	INNER JOIN Catalogo.Divisiones D WITH(NOLOCK) ON G.IdDivision = D.IdDivision
	WHERE G.Activo = @Activo AND (0 = @IdGrupo OR G.IdGrupo = @IdGrupo) AND (0 = @IdDivision OR G.IdDivision = @IdDivision)

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_GruposObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_GruposObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT
	
		G.IdGrupo,
		G.IdDivision,
		D.Nombre Division,
		G.Nombre,
		G.Descripcion,
		G.FechaInicial,
		G.FechaFinal,
		G.Activo
			
	FROM Catalogo.Grupos G
	INNER JOIN Catalogo.Divisiones D WITH(NOLOCK) ON G.IdDivision = D.IdDivision
	WHERE G.IdGrupo = @Identificador

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_GruposObtenerPorIdDivision]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_GruposObtenerPorIdDivision]
	@IdDivision INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT
	
		G.IdGrupo,
		G.IdDivision,
		D.Nombre Division,
		G.Nombre,
		G.Descripcion,
		G.FechaInicial,
		G.FechaFinal,
		G.Activo
			
	FROM Catalogo.Grupos G
	INNER JOIN Catalogo.Divisiones D WITH(NOLOCK) ON G.IdDivision = D.IdDivision
	WHERE G.IdDivision = @IdDivision

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_MedidasCobro_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_MedidasCobro_Actualizar]

	@Identificador int,
	@Nombre varchar(50),
	@Descripcion varchar(100)
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.MedidasCobro
		SET 

			Nombre	= @Nombre,
			Descripcion 	= @Descripcion
			
		WHERE IdMedidaCobro = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdMedidaCobro 		:', @Identificador,'
								  , NombreMedidaCobro	:',	@Nombre,'
								  , DescMedidaCobro		:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_MedidasCobro_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_MedidasCobro_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.MedidasCobro
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdMedidaCobro = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdMedidaCobro :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_MedidasCobro_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_MedidasCobro_Insertar]
    
    
	@Nombre varchar(50),
	@Descripcion varchar(100),
    @FechaInicial date,
    @FechaFinal date,
    @Activo bit

AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT into Catalogo.MedidasCobro
		(
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo			
		) 
		values
		( 
			@Nombre,
			@Descripcion,
			@FechaInicial,
			@FechaFinal,
			@Activo
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									NombreMedidaCobro	:', @Nombre,'
								  , DescMedidaCobro		:', @Descripcion,'
								  , FechaInicial		:', @FechaInicial,'
								  , FechaFinal			:', @FechaFinal,'
								  , Activo				:', @Activo,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_MedidasCobroObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_MedidasCobroObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	BEGIN
		SELECT 
				IdMedidaCobro,
				Nombre,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				CASE WHEN @Todos = @Zero THEN
					IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
				ELSE @Zero END AS Paginas
			
		FROM Catalogo.MedidasCobro
		CROSS JOIN (SELECT COUNT(IdMedidaCobro) AS Total
					 FROM Catalogo.MedidasCobro
					 WHERE Activo = @activo) AS R
		WHERE Activo = @activo
		ORDER BY IdMedidaCobro 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_MedidasCobroObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_MedidasCobroObtenerPorCriterio]
	@Activo BIT,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdMedidaCobro,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo
			
	FROM Catalogo.MedidasCobro
	WHERE Activo = @Activo

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_MedidasCobroObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_MedidasCobroObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdMedidaCobro,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo
			
	FROM Catalogo.MedidasCobro
	WHERE IdMedidaCobro = @Identificador

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_MesesObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Finaliza Actualizar-------------------------------------------


--------------------------------------------Iniciar Obtener-------------------------------------------
-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de Meses
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_MesesObtener]
	@Todos	BIT = 0,
	@pagina INT = 1,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0
	
	SELECT 
			IdMes,
			Nombre,
			CASE WHEN @Todos = @Zero THEN
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
			ELSE @Zero END AS Paginas
			
	FROM Catalogo.Meses
	CROSS JOIN (SELECT COUNT(IdMes) AS Total
			     FROM Catalogo.Meses) AS R
	ORDER BY IdMes 
	OFFSET ((@pagina - 1) * @filas)  
	ROWS FETCH NEXT @filas ROWS ONLY;
END




GO
/****** Object:  StoredProcedure [Catalogo].[sp_MesesObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Finaliza ObtenerPorId-------------------------------------------

--------------------------------------------Inicia ObtenerPorCriterio-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de una Meses
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_MesesObtenerPorCriterio]
	@Mes VARCHAR(50)
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdMes,
			Nombre
			
			
	FROM Catalogo.Meses
	WHERE Nombre = @Mes

END




GO
/****** Object:  StoredProcedure [Catalogo].[sp_MesesObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------Finalizar Obtener-------------------------------------------

--------------------------------------------Inicia ObtenerPorId-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de una Meses
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_MesesObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdMes,
			Nombre
			
			
	FROM Catalogo.Meses
	WHERE IdMes = @Identificador

END




GO
/****** Object:  StoredProcedure [Catalogo].[sp_Periodos_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Catalogo].[sp_Periodos_Actualizar]
	
	@Identificador int,
	@Periodo varchar(50),
	@Descripcion varchar(300)
	
AS
BEGIN	
	
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Periodos
		SET 
			Periodo = @Periodo,
			Descripcion		= @Descripcion
			
			
		WHERE IdPeriodo = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdPeriodo 		:', @Identificador,'
									, Periodo			:', @Periodo,'
								    , Descripcion		:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_Periodos_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Catalogo].[sp_Periodos_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Periodos
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdPeriodo = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdPeriodo :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_Periodos_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Catalogo].[sp_Periodos_Insertar]
	
	@Periodo varchar(50),
	@Descripcion varchar(300)
	

AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT into Catalogo.Periodos
		(
			Periodo,
			Descripcion
			
		) 
		values
		(	@Periodo,
			@Descripcion
			
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									Periodo 	: ',@Periodo,'
									, Descripcion		:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_Periodos_ValidarRegistro]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 01/09/2017
-- Description:	Valida el registro de periodo.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_Periodos_ValidarRegistro]
	@Identificador int,
	@Periodo varchar(50)	
AS
BEGIN	
	DECLARE @RESULTADO					VARCHAR(100) = '';
	DECLARE @FUE_MODIFICADO_EL_REGISTRO	BIT = 1;
	DECLARE @ES_DUPLICADO				BIT = 0;
	DECLARE @TIENE_DEPENDENCIA			BIT = 0;

	-- =============================================
	-- Si no existe entonces es un registro modificado 
	-- o nuevo, si existe entonces es el mismo registro
	-- y se permite actualizar sin validar
	-- =============================================
	IF  EXISTS(SELECT * FROM Catalogo.Periodos
				WHERE	IdPeriodo = @Identificador
				AND		RTRIM(LTRIM(Periodo)) = LTRIM(RTRIM(@Periodo)))
		BEGIN
			SET @FUE_MODIFICADO_EL_REGISTRO = 0;
		END
	ELSE
		BEGIN
			-- =============================================
			-- Consulta los registros que coincidan con el
			-- valor del concepto de la cuota.
			-- =============================================
			IF	EXISTS(SELECT * FROM Catalogo.Periodos
					   WHERE RTRIM(LTRIM(Periodo)) = LTRIM(RTRIM(@Periodo)))
				BEGIN
					SET @ES_DUPLICADO = 1;
				END
			-- =============================================
			-- Indica si hay dependencias con otras tablas.
			-- =============================================
			/*
			IF	EXISTS(SELECT * FROM Catalogo.Periodos
					   WHERE RTRIM(LTRIM(Periodo)) = LTRIM(RTRIM(@Periodo)))
				BEGIN
					SET @ES_DUPLICADO = 1;
				END
			*/
		END

	-- =============================================
	-- Reglas de negocio:
	--	1.	Si @FUE_MODIFICADO_EL_REGISTRO = 1, Ya no entra a 
	--		validar y envia el valor vacio.
	--	2.	Si @FUE_MODIFICADO_EL_REGISTRO = 0, entonces entra
	--		a la validación.
	--
	--		2.1 Si	@EXISTE_LA_FRACCION = 1 manda el
	--			el mensaje "Ya existe la fracción".	
	--
	--		2.2 Si @TIENE_DEPENDENCIA = 1 manda el 
	--			mensaje "No puede cambiar la fracción,
	--			ya que existen registros asociados a
	--			la fracción".
	-- =============================================
	IF @FUE_MODIFICADO_EL_REGISTRO = 1
		BEGIN
			IF @ES_DUPLICADO = 1
				SET @RESULTADO = 'El nombre del periodo ya existe, no se puede guardar su información.';	
			ELSE IF @TIENE_DEPENDENCIA = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'No puede cambiar la información del periodo, ya que existen registros asociados al periodo que desea editar.';
		END
		

	SELECT @RESULTADO AS Resultado
END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_PeriodosObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Catalogo].[sp_PeriodosObtener]
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	

	SELECT 
			IdPeriodo,
			Periodo,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo,
			R.Paginas 
			
	FROM Catalogo.Periodos
	CROSS JOIN (SELECT COUNT(IdPeriodo) AS Paginas
			     FROM Catalogo.Periodos) AS R
	ORDER BY IdPeriodo 
	OFFSET ((@pagina - 1) * @filas)  
	ROWS FETCH NEXT @filas ROWS ONLY;
END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_PeriodosObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Catalogo].[sp_PeriodosObtenerPorCriterio]
	@Activo bit,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdPeriodo,
			Periodo,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo
			
	FROM Catalogo.Periodos
	WHERE Activo = @Activo

END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_PeriodosObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Catalogo].[sp_PeriodosObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdPeriodo,
			Periodo,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo
	FROM Catalogo.Periodos
	WHERE IdPeriodo = @Identificador

END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_Referencias_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------Finaliza Insertar-------------------------------------------

--------------------------------------------Inicia Actualizar-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Actualiza la informacion de un registro de división para la actividad económica.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_Referencias_Actualizar]	
	@Identificador int,
	@ClaveReferencia int,
	@Descripcion varchar(300)
	
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Referencias
		SET 
			ClaveReferencia = @ClaveReferencia,
			Descripcion 	= @Descripcion
			
		WHERE IdReferencia = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdReferencia 		:',@Identificador,'
								  , ClaveReferencia		:', @ClaveReferencia,'
								  , Descripcion			:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END




GO
/****** Object:  StoredProcedure [Catalogo].[sp_Referencias_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Finaliza Actualizar-------------------------------------------

--------------------------------------------Inicia CambiarEstatus-------------------------------------------
-- =============================================
-- Author:		Josue Zaragoza
-- Create date: 03/08/2017 
-- Description:	Cambia el estado de un registro a activo o desactivo.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_Referencias_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.Referencias
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdReferencia = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdReferencia :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END




GO
/****** Object:  StoredProcedure [Catalogo].[sp_Referencias_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- ============================================= Procedimientos de Referencias
-- Author:		Josue Zaragoza R
-- Create date: 03/08/2017
-- Description:	Inserta la informacion de un registro de las Referencias para la actividad económica.
-- =============================================
--------------------------------------------Inicia Insertar-------------------------------------------
CREATE PROCEDURE [Catalogo].[sp_Referencias_Insertar]
	@ClaveReferencia int,
	@Descripcion varchar(300)

AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT into Catalogo.Referencias
		(
			ClaveReferencia,
			Descripcion
		) 
		values
		(
			@ClaveReferencia,
			@Descripcion
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									ClaveReferencia		:', @ClaveReferencia,',
									Descripcion			:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END




GO
/****** Object:  StoredProcedure [Catalogo].[sp_Referencias_ValidarRegistro]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 31/08/2017
-- Description:	Valida el registro del concepto
--				del clave referencia.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_Referencias_ValidarRegistro]
(
	@Identificador		INT,
	@ClaveReferencia	INT,
	@Descripcion		VARCHAR(100)
)
AS
BEGIN	
	
	DECLARE @RESULTADO					VARCHAR(100) = '';
	DECLARE @FUE_MODIFICADO_EL_REGISTRO	BIT = 1;
	DECLARE @ES_DUPLICADO				BIT = 0;
	DECLARE @EXISTE_VALOR_SIMILAR		BIT = 0;
	DECLARE @TIENE_DEPENDENCIA			BIT = 0;

	-- =============================================
	-- Si no existe entonces es un registro modificado 
	-- o nuevo, si existe entonces es el mismo registro
	-- y se permite actualizar sin validar
	-- =============================================
	IF  EXISTS(SELECT * FROM [Catalogo].[Referencias]
				WHERE	IdReferencia = @Identificador
				AND		ClaveReferencia = @ClaveReferencia)
		BEGIN
			SET @FUE_MODIFICADO_EL_REGISTRO = 0;
		END
	ELSE
		BEGIN
			-- =============================================
			-- Indica si hay valores similares.
			-- =============================================
			IF  EXISTS(SELECT * FROM [Catalogo].[Referencias]
						  WHERE ClaveReferencia = @ClaveReferencia
						  AND	RTRIM(LTRIM(Descripcion)) = RTRIM(LTRIM(@Descripcion)))
				BEGIN
					SET @EXISTE_VALOR_SIMILAR = 1;
				END	
			-- =============================================
			-- Indica si hay duplicidad en la referencia.
			-- =============================================
			IF	EXISTS(SELECT * FROM [Catalogo].[Referencias]
					   WHERE IdReferencia <> @Identificador
					   AND	 ClaveReferencia = @ClaveReferencia)
				BEGIN
					SET @ES_DUPLICADO = 1;
				END

			-- =============================================
			-- Indica si hay dependencias con otras tablas.
			-- =============================================
			IF EXISTS(SELECT * FROM [Catalogo].[Cuotas]
						WHERE IdReferencia = @Identificador )
				BEGIN
					SET @TIENE_DEPENDENCIA = 1;
				END
		END

	-- ========================================================
	-- Reglas de negocio:
	--	1.	Si @FUE_MODIFICADO_EL_REGISTRO = 1, Ya no entra a 
	--		validar y envia el valor vacio.
	--	2.	Si @FUE_MODIFICADO_EL_REGISTRO = 0, entonces entra
	--		a la validación.
	--
	--		2.1	Si  @EXISTE_VALOR_SIMILAR = 1, manda 
	--			el mensaje "Ya existe un valor similar".
	--
	--		2.2 Si	@EXISTE_LA_FRACCION = 1 manda el
	--			el mensaje "Ya existe la clave de referencia".	
	--
	--		2.3 Si @TIENE_DEPENDENCIA = 1 manda el 
	--			mensaje "No puede cambiar la clave de referencia,
	--			ya que existen registros asociados a
	--			la misma".
	-- =============================================
	IF @FUE_MODIFICADO_EL_REGISTRO = 1
		BEGIN
			IF @ES_DUPLICADO = 1
				SET @RESULTADO = 'La clave de referencia ya existe, no se puede guardar su información.';	
			ELSE IF @EXISTE_VALOR_SIMILAR = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'Existe una clave de referencia con los datos que ha editado, no se puede guardar su información.';
			ELSE IF @TIENE_DEPENDENCIA = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'No puede cambiar la información de la clave de referencia, ya que existen registros asociados a la misma clave.';
		END
		

	SELECT @RESULTADO AS Resultado
END





GO
/****** Object:  StoredProcedure [Catalogo].[sp_ReferenciasObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Final CambiarEstatus-------------------------------------------

--------------------------------------------Iniciar Obtener-------------------------------------------
-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de Referencias
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_ReferenciasObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0
	IF @Todos = @Zero 
	BEGIN
		SELECT 
				IdReferencia,
				ClaveReferencia,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)	AS Paginas	
			
		FROM Catalogo.Referencias
		CROSS JOIN (SELECT COUNT(IdReferencia) AS Total
					 FROM Catalogo.Referencias
					 WHERE Activo = @activo) AS R
		WHERE Activo = @activo
		ORDER BY IdReferencia 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
	ELSE
	BEGIN
		SELECT 
				IdReferencia,
				ClaveReferencia,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				Paginas = @Zero 			
		FROM Catalogo.Referencias		
		WHERE Activo = @activo
	END
END

GO
/****** Object:  StoredProcedure [Catalogo].[sp_ReferenciasObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Finaliza ObtenerPorId-------------------------------------------

--------------------------------------------Inicia ObtenerPorCriterio-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de una Referencias
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_ReferenciasObtenerPorCriterio]
	@Activo BIT,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdReferencia,
			ClaveReferencia,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo
			
	FROM Catalogo.Referencias
	WHERE Activo = @Activo

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_ReferenciasObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------Finalizar Obtener-------------------------------------------

--------------------------------------------Inicia ObtenerPorId-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de una Referencias
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_ReferenciasObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdReferencia,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo
	FROM Catalogo.Referencias
	WHERE IdReferencia = @Identificador

END




GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposDocumento_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------Finaliza Insertar-------------------------------------------

--------------------------------------------Inicia Actualizar-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Actualiza la informacion de un registro de división para la actividad económica.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_TiposDocumento_Actualizar]
	@Identificador INT = NULL,
	@Nombre VARCHAR(50) = NULL,
    @Descripcion VARCHAR(100) = NULL,
    @IdActividad INT = NULL,
    @Confidencial BIT = NULL
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.TiposDocumento
		SET 

			Nombre              = @Nombre,
			Descripcion	        = @Descripcion,
			IdActividad 		= @IdActividad,
			Confidencial 		= @Confidencial
			
		WHERE IdTipoDocumento = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdTipoDocumento 	:', @Identificador,'
								  , Nombre				:',	@Nombre,'
								  , Descripcion			:', @Descripcion,'
								  , IdActividad			:', @IdActividad,'
								  , Confidencial		:', @Confidencial,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END






GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposDocumento_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Josue Zaragoza
-- Create date: 03/08/2017 
-- Description:	Cambia el estado de un registro a activo o desactivo.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_TiposDocumento_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.TiposDocumento
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdTipoDocumento = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdTipoDocumento :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END






GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposDocumento_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= Procedimientos de TiposDocumento
-- Author:		Josue Zaragoza R
-- Create date: 03/08/2017
-- Description:	Inserta la informacion de un registro de las TiposDocumento para la actividad económica.
-- =============================================
--------------------------------------------Inicia Insertar-------------------------------------------
CREATE PROCEDURE [Catalogo].[sp_TiposDocumento_Insertar]
    
    @Nombre VARCHAR(50) = NULL,
    @Descripcion VARCHAR(100) = NULL,
    @IdActividad INT,
    @Confidencial BIT = NULL

AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT into Catalogo.TiposDocumento
		(
			Nombre,
			Descripcion,
			IdActividad,
			Confidencial	
		) 
		values
		( 
			@Nombre,
			@Descripcion,
			@IdActividad,
			@Confidencial
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									Nombre				:',	@Nombre,'
								  , Descripcion			:', @Descripcion,'
								  , IdActividad			:', @IdActividad,'
								  , Confidencial		:', @Confidencial,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END






GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposDocumento_ValidarRegistro]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 31/08/2017
-- Description:	Valida el registro del concepto
--				del tipo documento.
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_TiposDocumento_ValidarRegistro]
(
	@Identificador INT = NULL,
	@Nombre VARCHAR(50) = NULL,
    @IdActividad INT = NULL
)
AS
BEGIN	
	
	DECLARE @RESULTADO					VARCHAR(100) = '';
	DECLARE @FUE_MODIFICADO_EL_REGISTRO	BIT = 1;
	DECLARE @ES_DUPLICADO				BIT = 0;
	DECLARE @EXISTE_VALOR_SIMILAR		BIT = 0;
	DECLARE @TIENE_DEPENDENCIA			BIT = 0;

	-- =============================================
	-- Si no existe entonces es un registro modificado 
	-- o nuevo, si existe entonces es el mismo registro
	-- y se permite actualizar sin validar
	-- =============================================
	IF  EXISTS(SELECT * FROM [Catalogo].[TiposDocumento]
				WHERE	IdTipoDocumento = @Identificador
				AND		IdActividad	= @IdActividad
				AND		RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@Nombre)))
		BEGIN
			SET @FUE_MODIFICADO_EL_REGISTRO = 0;
		END
	ELSE
		BEGIN
			-- =============================================
			-- Indica si hay valores similares.
			-- =============================================
			IF  EXISTS(SELECT * FROM [Catalogo].[TiposDocumento]
						  WHERE RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@Nombre))
						  AND	IdActividad	= @IdActividad)
				BEGIN
					SET @EXISTE_VALOR_SIMILAR = 1;
				END	

			-- =============================================
			-- Indica si hay duplicidad en la referencia.
			-- =============================================
			IF	EXISTS(SELECT * FROM [Catalogo].[TiposDocumento]
						  WHERE RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@Nombre))
						  AND IdTipoDocumento <> @Identificador  
						)
				BEGIN
					SET @ES_DUPLICADO = 1;
				END
			-- =============================================
			-- Indica si hay dependencias con otras tablas.
			-- =============================================
			IF	EXISTS(SELECT * FROM [Configuracion].[ActividadesTiposDocumento]
						WHERE IdTipoDocumento = @Identificador )
				BEGIN
					SET @TIENE_DEPENDENCIA = 1;
				END

		END

	
	-- =============================================
	-- Reglas de negocio:
	--	1.	Si @FUE_MODIFICADO_EL_REGISTRO = 1, Ya no entra a 
	--		validar y envia el valor vacio.
	--	2.	Si @FUE_MODIFICADO_EL_REGISTRO = 0, entonces entra
	--		a la validación.
	--
	--		2.1	Si  @EXISTE_VALOR_SIMILAR = 1, manda 
	--			el mensaje "Ya existe un valor similar".
	--
	--		2.2 Si	@EXISTE_LA_FRACCION = 1 manda el
	--			el mensaje "Ya existe el tipo de documento".	
	--
	--		2.3 Si @TIENE_DEPENDENCIA = 1 manda el 
	--			mensaje "No puede cambiar el tipo de documento,
	--			ya que existen registros asociados al
	--			mismo documento".
	-- =============================================
	IF @FUE_MODIFICADO_EL_REGISTRO = 1
		BEGIN
			IF @ES_DUPLICADO = 1
				SET @RESULTADO = 'El nombre del tipo de documento ya existe, no se puede guardar su información.';	
			ELSE IF @EXISTE_VALOR_SIMILAR = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'Existe el tipo de documento con los datos que ha editado, no se puede guardar su información.';
			ELSE IF @TIENE_DEPENDENCIA = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'No puede cambiar la información del tipo de documento, ya que existen registros asociados al tipo de documento editado.';
		END
		

	SELECT @RESULTADO AS Resultado
END





GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposDocumentoObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Final CambiarEstatus-------------------------------------------

--------------------------------------------Iniciar Obtener-------------------------------------------
-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de TiposDocumento
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_TiposDocumentoObtener]
	@Todos	BIT = 0,
	@pagina INT = 1,
	@filas  INT = 20
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	SELECT 
			TD.IdTipoDocumento,
			TD.Nombre,
			TD.Descripcion,
			TD.IdActividad,
			A.Nombre Actividad,
			TD.Confidencial,
			TD.FechaInicial,
			TD.FechaFinal,
			TD.Activo,
				CASE WHEN @Todos = @Zero THEN
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
				ELSE @Zero END AS Paginas	
			
		FROM Catalogo.TiposDocumento TD
		INNER JOIN Catalogo.Actividades A WITH(NOLOCK) ON TD.IdActividad = A.IdActividad
		CROSS JOIN (SELECT COUNT(TD.IdTipoDocumento) AS Total
					 FROM Catalogo.TiposDocumento TD
					 WHERE Activo = @activo) AS R
		WHERE TD.Activo = @activo
		ORDER BY TD.IdTipoDocumento 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
END






GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposDocumentoObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Catalogo].[sp_TiposDocumentoObtenerPorCriterio]
	@Activo BIT = 0,
	@Nombre VARCHAR = 50,
	@pagina INT = 1,
	@filas INT = 20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			TD.IdTipoDocumento,
			TD.Nombre,
			TD.Descripcion,
			TD.IdActividad,
			A.Nombre Actividad,
			TD.Confidencial,
			TD.FechaInicial,
			TD.FechaFinal,
			TD.Activo
			
	FROM Catalogo.TiposDocumento TD
		INNER JOIN Catalogo.Actividades A WITH(NOLOCK) ON TD.IdActividad = A.IdActividad
	WHERE TD.Activo = @Activo

END






GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposDocumentoObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------Finalizar Obtener-------------------------------------------

--------------------------------------------Inicia ObtenerPorId-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de una TiposDocumento
-- =============================================
CREATE PROCEDURE [Catalogo].[sp_TiposDocumentoObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			TD.IdTipoDocumento,
			TD.Nombre,
			TD.Descripcion,
			TD.IdActividad,
			A.Nombre Actividad,
			TD.Confidencial,
			TD.FechaInicial,
			TD.FechaFinal,
			TD.Activo	
			
	FROM Catalogo.TiposDocumento TD
		INNER JOIN Catalogo.Actividades A WITH(NOLOCK) ON TD.IdActividad = A.IdActividad
	WHERE IdTipoDocumento = @Identificador

END






GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposPagoActividadesObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Catalogo].[sp_TiposPagoActividadesObtener]
	@IdTipoPago INT = NULL
AS BEGIN
	--EXEC Configuracion.sp_TiposPagoActividadesObtener 1
	SELECT	TipoPagoNombre = tp.Nombre, 
			ActividadNombre = a.Nombre,
			Aplica = CAST(0 AS BIT),
			Tiempo = CAST(0 AS DECIMAL(18,2))
	FROM Catalogo.Actividades AS a
	CROSS JOIN  Catalogo.TiposPago AS tp
	WHERE a.Activo	= 1
		AND tp.Actividad= 1
		AND tp.IdTipoPago = ISNULL(@IdTipoPago,tp.IdTipoPago)
	ORDER BY tp.IdTipoPago, a.IdActividad	

END
GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposPagoObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_TiposPagoObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	IF @Todos = @activo 
	BEGIN
		SELECT 
			IdTipoPago,
			Nombre,
			Descripcion,
			Actividad,
			FechaInicial,
			FechaFinal,
			Activo,
			Paginas = 0
			
			FROM Catalogo.TiposPago			
			WHERE Activo = @activo
			
	END
	ELSE
	BEGIN
		
		SELECT 
			IdTipoPago,
			Nombre,
			Descripcion,
			Actividad,
			FechaInicial,
			FechaFinal,
			Activo,
				CASE WHEN @Todos = @Zero THEN
					IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
				ELSE @Zero END AS Paginas	
			
		FROM Catalogo.TiposPago
		CROSS JOIN (SELECT COUNT(IdTipoPago) AS Total
					 FROM Catalogo.TiposPago
					 WHERE Activo = @activo) AS R
		WHERE Activo = @activo
		ORDER BY IdTipoPago 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposServicio_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_TiposServicio_Actualizar]
	@Identificador int,
	@Nombre varchar(50),
	@Descripcion varchar(100),
	@Clave varchar(4)
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.TiposServicio
		SET 

			Nombre		= @Nombre,
			Descripcion = @Descripcion,
			Clave		= @Clave
			
		WHERE IdTipoServicio = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdTipoServicio 	:', @Identificador,'
								  , Nombre			:',	@Nombre,'
								  , Descripcion		:', @Descripcion,'
								  , Clave			:', @Clave,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposServicio_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_TiposServicio_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Catalogo.TiposServicio
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdTipoServicio = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdTipoServicio :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposServicio_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_TiposServicio_Insertar]
    
	@Nombre varchar(50),
	@Descripcion varchar(100),
	@Clave varchar(4)

AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT INTO Catalogo.TiposServicio
		(
			Nombre,
			Descripcion,
			Clave
		) 
		VALUES
		( 
			@Nombre,
			@Descripcion,
			@Clave
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									Nombre			:',	@Nombre,'
								  , Descripcion		:', @Descripcion,'
								  , Clave			:', @Clave,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposServicioObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_TiposServicioObtenerPorCriterio]
	@Activo BIT = NULL,
	@IdTipoServicio INT = NULL,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdTipoServicio,
			Nombre,
			Descripcion,
			Clave,
			FechaInicial,
			FechaFinal,
			Activo	
			
	FROM Catalogo.TiposServicio
	WHERE Activo = @Activo AND (0 = @IdTipoServicio OR IdTipoServicio = @IdTipoServicio)

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposServicioObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_TiposServicioObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdTipoServicio,
			Nombre,
			Descripcion,
			Clave,
			FechaInicial,
			FechaFinal,
			Activo
			
	FROM Catalogo.TiposServicio
	WHERE IdTipoServicio = @Identificador

END


GO
/****** Object:  StoredProcedure [Catalogo].[sp_TiposServiciosObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Catalogo].[sp_TiposServiciosObtener]
	@Todos	BIT = 0,
	@pagina INT = 1,
	@filas  INT = 20
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	BEGIN
		SELECT 
				IdTipoServicio,
				Nombre,
				Descripcion,
				Clave,
				FechaInicial,
				FechaFinal,
				Activo,
				CASE WHEN @Todos = @Zero THEN
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
				ELSE @Zero END AS Paginas	
			
		FROM Catalogo.TiposServicio
		CROSS JOIN (SELECT COUNT(IdTipoServicio) AS Total
					 FROM Catalogo.TiposServicio
					 WHERE Activo = @activo) AS R
		WHERE Activo = @activo
		ORDER BY IdTipoServicio 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
END


GO
/****** Object:  StoredProcedure [Configuracion].[sp_ConfServicioObtenerPorIdConfiguracion]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 24/05/2017
-- Description:	Consulta la configuracion del 
--				servicio en base a la estructura
--				de la tabla definida por el 
--				usuario "ConfiguracionServicio".
-- =============================================
CREATE PROCEDURE [Configuracion].[sp_ConfServicioObtenerPorIdConfiguracion]	@Identificador INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	CS.IdTipoServicio, 
			CS.IdCentroCosto,
			TRFTP.IdRegimenFiscal, 
			TRFTP.IdTipoPago,
			TPA.IdActividad, 
			ATPD.IdTipoDocumento,
			TPA.Tiempo, 
			ATPD.Aplica, 
			ATPD.Obligatoriedad,
			ISNULL(VCC.Descripcion,'') AS CentroCosto,
			ISNULL(TS.Nombre , '') AS TipoServicio,
			ISNULL(TR.Nombre , '') AS TipoRegimenFiscal,
			ISNULL(TP.Nombre , '') AS TipoPago,
			ISNULL(AC.Nombre , '') AS Actividad,
			ISNULL(TD.Nombre , '') AS TipoDocumento
	FROM	  [Configuracion].[ConfServicio]					AS CS
	LEFT JOIN [Configuracion].[TiposRegimenFiscalTiposPago]		AS TRFTP	ON	CS.IdConfServicio = TRFTP.IdConfServicio 
	LEFT JOIN [Configuracion].[TiposPagoActividades]			AS TPA		ON	CS.IdConfServicio = TPA.IdConfServicio 
																			AND TRFTP.IdTipoPago = TPA.IdTipoPago

	LEFT JOIN [Configuracion].[ActividadesTiposDocumento]		AS ATPD		ON	CS.IdConfServicio = ATPD.IdConfServicio 
																			AND TPA.IdActividad = ATPD.IdActividad

	LEFT JOIN [Catalogo].[vwCentrosCosto]						AS VCC		ON CS.IdCentroCosto = VCC.IdCentroCosto
	LEFT JOIN [Catalogo].[TiposServicio]						AS TS		ON CS.IdTipoServicio = TS.IdTipoServicio
	LEFT JOIN [Catalogo].[TiposRegimenFiscal]					AS TR		ON TRFTP.IdRegimenFiscal = TR.IdRegimenFiscal
	LEFT JOIN [Catalogo].[TiposPago]							AS TP		ON TRFTP.IdTipoPago = TP.IdTipoPago
	LEFT JOIN [Catalogo].[Actividades]							AS AC		ON TPA.IdActividad = AC.IdActividad	
	LEFT JOIN [Catalogo].[TiposDocumento]						AS TD		ON ATPD.IdTipoDocumento = TD.IdTipoDocumento
	WHERE CS.IdConfServicio = @Identificador
	ORDER BY  TRFTP.IdRegimenFiscal, TRFTP.IdTipoPago, TPA.IdActividad, ATPD.IdTipoDocumento
END

GO
/****** Object:  StoredProcedure [Configuracion].[sp_ConfServicioObtenerPorIdTipoServicioIdCentroCosto]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 24/05/2017
-- Description:	Consulta la configuracion del 
--				servicio en base a la estructura
--				de la tabla definida por el 
--				usuario "ConfiguracionServicio".
-- =============================================


CREATE PROCEDURE [Configuracion].[sp_ConfServicioObtenerPorIdTipoServicioIdCentroCosto]	@IdTipoServicio INT,
																						@IdCentroCosto	VARCHAR(6)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	CS.IdTipoServicio, 
			CS.IdCentroCosto,
			TRFTP.IdRegimenFiscal, 
			TRFTP.IdTipoPago,
			TPA.IdActividad, 
			ATPD.IdTipoDocumento,
			TPA.Tiempo, 
			ATPD.Aplica, 
			ATPD.Obligatoriedad,
			ISNULL(VCC.Descripcion,'') AS CentroCosto,
			ISNULL(TS.Nombre , '') AS TipoServicio,
			ISNULL(TR.Nombre , '') AS TipoRegimenFiscal,
			ISNULL(TP.Nombre , '') AS TipoPago,
			ISNULL(AC.Nombre , '') AS Actividad,
			ISNULL(TD.Nombre , '') AS TipoDocumento
	FROM	  [Configuracion].[ConfServicio]					AS CS
	LEFT JOIN [Configuracion].[TiposRegimenFiscalTiposPago]		AS TRFTP	ON	CS.IdConfServicio = TRFTP.IdConfServicio 
	LEFT JOIN [Configuracion].[TiposPagoActividades]			AS TPA		ON	CS.IdConfServicio = TPA.IdConfServicio 
																			AND TRFTP.IdTipoPago = TPA.IdTipoPago

	LEFT JOIN [Configuracion].[ActividadesTiposDocumento]		AS ATPD		ON	CS.IdConfServicio = ATPD.IdConfServicio 
																			AND TPA.IdActividad = ATPD.IdActividad

	LEFT JOIN [Catalogo].[vwCentrosCosto]						AS VCC		ON CS.IdCentroCosto = VCC.IdCentroCosto
	LEFT JOIN [Catalogo].[TiposServicio]						AS TS		ON CS.IdTipoServicio = TS.IdTipoServicio
	LEFT JOIN [Catalogo].[TiposRegimenFiscal]					AS TR		ON TRFTP.IdRegimenFiscal = TR.IdRegimenFiscal
	LEFT JOIN [Catalogo].[TiposPago]							AS TP		ON TRFTP.IdTipoPago = TP.IdTipoPago
	LEFT JOIN [Catalogo].[Actividades]							AS AC		ON TPA.IdActividad = AC.IdActividad	
	LEFT JOIN [Catalogo].[TiposDocumento]						AS TD		ON ATPD.IdTipoDocumento = TD.IdTipoDocumento
	WHERE CS.IdTipoServicio = @IdTipoServicio AND CS.IdCentroCosto = @IdCentroCosto
	ORDER BY  TRFTP.IdRegimenFiscal, TRFTP.IdTipoPago, TPA.IdActividad, ATPD.IdTipoDocumento
END

GO
/****** Object:  StoredProcedure [Configuracion].[sp_ConfServicioObtenerPorPaginado]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 24/05/2017
-- Description:	Consulta la configuracion del 
--				servicio en base a la estructura
--				de la tabla definida por el 
--				usuario "ConfiguracionServicio".
-- =============================================


CREATE PROCEDURE [Configuracion].[sp_ConfServicioObtenerPorPaginado]
	@pagina INT = 1,
	@filas  INT = 20
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	SELECT	CS.IdTipoServicio, 
			CS.IdCentroCosto,
			TRFTP.IdRegimenFiscal, 
			TRFTP.IdTipoPago,
			TPA.IdActividad, 
			ATPD.IdTipoDocumento,
			TPA.Tiempo, 
			ATPD.Aplica, 
			ATPD.Obligatoriedad,
			ISNULL(VCC.Descripcion,'') AS CentroCosto,
			ISNULL(TS.Nombre , '') AS TipoServicio,
			ISNULL(TR.Nombre , '') AS TipoRegimenFiscal,
			ISNULL(TP.Nombre , '') AS TipoPago,
			ISNULL(AC.Nombre , '') AS Actividad,
			ISNULL(TD.Nombre , '') AS TipoDocumento
	FROM	  [Configuracion].[ConfServicio]					AS CS
	LEFT JOIN [Configuracion].[TiposRegimenFiscalTiposPago]		AS TRFTP	ON	CS.IdConfServicio = TRFTP.IdConfServicio 
	LEFT JOIN [Configuracion].[TiposPagoActividades]			AS TPA		ON	CS.IdConfServicio = TPA.IdConfServicio 
																			AND TRFTP.IdTipoPago = TPA.IdTipoPago

	LEFT JOIN [Configuracion].[ActividadesTiposDocumento]		AS ATPD		ON	CS.IdConfServicio = ATPD.IdConfServicio 
																			AND TPA.IdActividad = ATPD.IdActividad

	LEFT JOIN [Catalogo].[vwCentrosCosto]						AS VCC		ON CS.IdCentroCosto = VCC.IdCentroCosto
	LEFT JOIN [Catalogo].[TiposServicio]						AS TS		ON CS.IdTipoServicio = TS.IdTipoServicio
	LEFT JOIN [Catalogo].[TiposRegimenFiscal]					AS TR		ON TRFTP.IdRegimenFiscal = TR.IdRegimenFiscal
	LEFT JOIN [Catalogo].[TiposPago]							AS TP		ON TRFTP.IdTipoPago = TP.IdTipoPago
	LEFT JOIN [Catalogo].[Actividades]							AS AC		ON TPA.IdActividad = AC.IdActividad	
	LEFT JOIN [Catalogo].[TiposDocumento]						AS TD		ON ATPD.IdTipoDocumento = TD.IdTipoDocumento
	
	ORDER BY  TRFTP.IdRegimenFiscal, TRFTP.IdTipoPago, TPA.IdActividad, ATPD.IdTipoDocumento
	OFFSET ((@pagina - 1) * @filas)  
	ROWS FETCH NEXT @filas ROWS ONLY;
END

GO
/****** Object:  StoredProcedure [Configuracion].[sp_Fases_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------Finaliza Insertar-------------------------------------------

--------------------------------------------Inicia Actualizar-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Actualiza la informacion de un registro de división para la actividad económica.
-- =============================================
CREATE PROCEDURE [Configuracion].[sp_Fases_Actualizar]
	
	@Identificador int,
	@Nombre varchar(30),
	@Descripciones varchar(300)
	
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Configuracion.Fases
		SET 
			Nombre 	= @Nombre,
			Descripcion 	= @Descripciones
			
		WHERE IdFase = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdFase 		:',@Identificador,'
								  ,	Nombre 	:',	@Nombre,'
								  , Descripciones	:', @Descripciones,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END




GO
/****** Object:  StoredProcedure [Configuracion].[sp_Fases_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Finaliza Actualizar-------------------------------------------

--------------------------------------------Inicia CambiarEstatus-------------------------------------------
-- =============================================
-- Author:		Josue Zaragoza
-- Create date: 03/08/2017 
-- Description:	Cambia el estado de un registro a activo o desactivo.
-- =============================================
CREATE PROCEDURE [Configuracion].[sp_Fases_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Configuracion.Fases
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdFase = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdFase 	:',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END




GO
/****** Object:  StoredProcedure [Configuracion].[sp_Fases_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ============================================= Procedimientos de Fases
-- Author:		Josue Zaragoza R
-- Create date: 03/08/2017
-- Description:	Inserta la informacion de un registro de las Fases para la actividad económica.
-- =============================================
--------------------------------------------Inicia Insertar-------------------------------------------
CREATE PROCEDURE [Configuracion].[sp_Fases_Insertar]

	@Nombre varchar(30),
	@Descripciones varchar(300),
	@FechaInicial date,
	@FechaFinal date,
	@Activo bit

AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT into Configuracion.Fases
		(
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo 
		) 
		values
		( 
			@Nombre,
			@Descripciones,
			@FechaInicial,
			@FechaFinal,
			@Activo 
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									Nombre 		:',	@Nombre,'
								  , Descripciones		:', @Descripciones,'
								  , FechaInicial	:', @FechaInicial,'
								  , FechaFinal		:', @FechaFinal,'
								  , Activo			:', @Activo,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END




GO
/****** Object:  StoredProcedure [Configuracion].[sp_FasesObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Final CambiarEstatus-------------------------------------------

--------------------------------------------Iniciar Obtener-------------------------------------------
-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de Fases
-- =============================================
CREATE PROCEDURE [Configuracion].[sp_FasesObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0
	IF @Todos = @Zero 
	BEGIN
		SELECT 
				IdFase,
				Nombre,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)	AS Paginas	
			
		FROM Configuracion.Fases
		CROSS JOIN (SELECT COUNT(IdFase) AS Total
					 FROM Configuracion.Fases
					 WHERE Activo = @activo) AS R
		WHERE Activo = @activo
		ORDER BY IdFase 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
	ELSE
	BEGIN
		SELECT 
				IdFase,
				Nombre,
				Descripcion,
				FechaInicial,
				FechaFinal,
				Activo,
				Paginas = @Zero 			
		FROM Configuracion.Fases		
		WHERE Activo = @activo
	END
END


GO
/****** Object:  StoredProcedure [Configuracion].[sp_FasesObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Finaliza ObtenerPorId-------------------------------------------

--------------------------------------------Inicia ObtenerPorCriterio-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de una Fases
-- =============================================
CREATE PROCEDURE [Configuracion].[sp_FasesObtenerPorCriterio]
	@Activo BIT,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdFase,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo
			
	FROM Configuracion.Fases
	WHERE Activo = @Activo

END



GO
/****** Object:  StoredProcedure [Configuracion].[sp_FasesObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------Finalizar Obtener-------------------------------------------

--------------------------------------------Inicia ObtenerPorId-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de una Fases
-- =============================================
CREATE PROCEDURE [Configuracion].[sp_FasesObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			IdFase,
			Nombre,
			Descripcion,
			FechaInicial,
			FechaFinal,
			Activo
	FROM Configuracion.Fases
	WHERE IdFase = @Identificador

END




GO
/****** Object:  StoredProcedure [Configuracion].[sp_InsertaConfServicio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Configuracion].[sp_InsertaConfServicio]
		@Tabla AS ConfiguracionServicio READONLY

AS
	BEGIN

	DECLARE @IdTipoServicio INT, 
			@IdCentroCostos INT,
			@Identidad		INT, 
			@Mensaje		VARCHAR(200)


		BEGIN TRY
			BEGIN TRAN

			
				SELECT DISTINCT @IdTipoServicio = IdTipoServicio,
								@IdCentroCostos = IdCentroCostos
									
				FROM @Tabla
				INSERT Configuracion.ConfServicio
				VALUES (@IdTipoServicio,
						@IdCentroCostos,
						1
						)
				SELECT @Identidad = @@IDENTITY, @Mensaje = 'OK'  

				INSERT Configuracion.TiposRegimenFiscalTiposPago
					SELECT DISTINCT 
						IdRegimenFiscal,
						IdTipoPago,
						@Identidad,
						1 
					FROM @Tabla

				INSERT Configuracion.TiposPagoActividades
					SELECT DISTINCT
						IdTipoPago,
						@Identidad,
						IdActividad,
						1,
						Tiempo
					FROM @Tabla
				
				INSERT Configuracion.ActividadesTiposDocumento
					SELECT DISTINCT
						IdActividad,
						IdTipoDocumento,
						@Identidad,
						1,
						Obigatoriedad
					FROM @Tabla

				COMMIT
		
	END TRY
	BEGIN CATCH
		ROLLBACK
		SELECT @Identidad = 0,
				@Mensaje = ERROR_MESSAGE()
	END CATCH
	
	SELECT	Identidad = @Identidad, 
			Mensaje = 'OK'
	END

GO
/****** Object:  StoredProcedure [Configuracion].[sp_InsertaNotificaciones]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Configuracion].[sp_InsertaNotificaciones]
		@IdTipoServicio		int,
		@IdActividad		int,
		@CuerpoCorreo		varchar	(4000),
		@EsCorreo			bit,
		@EsSistema			bit,
		@EmitirAlerta		bit,
		@TiempoAlerta		int,
		@Frecuencia			int,
		@AlertaEsCorreo		bit,
		@AlertaEsSistema	bit,
		@CuerpoAlerta		varchar	(4000)
AS
	BEGIN

	DECLARE @Identidad	INT,
			@Mensaje	VARCHAR(300) 
			

		BEGIN TRY
			BEGIN TRAN
				INSERT  [Configuracion].[Notificaciones]
					([IdTipoServicio]
					,[IdActividad]
					,[CuerpoCorreo]
					,[EsCorreo]
					,[EsSistema]
					,[EmitirAlerta]
					,[TiempoAlerta]
					,[Frecuencia]
					,[AlertaEsCorreo]
					,[AlertaEsSistema]
					,[CuerpoAlerta]
					,[FechaInicial]
					,[Activo])
				 VALUES (
						@IdTipoServicio,
						@IdActividad,
						@CuerpoCorreo,
						@EsCorreo,
						@EsSistema,
						@EmitirAlerta,
						@TiempoAlerta,
						@Frecuencia,
						@AlertaEsCorreo,
						@AlertaEsSistema,
						@CuerpoAlerta,
						GETDATE(),
						1
					  )
					SELECT @Identidad = @@IDENTITY,
				@Mensaje = 'OK'  

		COMMIT
		
	END TRY
	BEGIN CATCH
		ROLLBACK
		SELECT @Identidad = 0,
				@Mensaje = ERROR_MESSAGE()
	END CATCH
	
		SELECT	Identidad = @Identidad, 
				Mensaje = 'OK'
	END


GO
/****** Object:  StoredProcedure [Configuracion].[sp_NotificacionesObtenerPorIdNotificacion]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Configuracion].[sp_NotificacionesObtenerPorIdNotificacion]
	-- Add the parameters for the stored procedure here
	@IdNotificacion int = 0
AS
BEGIN
	SELECT TOP(1)
		*,
		CTS.Nombre AS TipoServicio,
		CA.Nombre AS Actividad ,
		CA.IdFase,
		CF.Nombre AS Fase
		FROM Configuracion.Notificaciones CN
		INNER JOIN Catalogo.TiposServicio CTS ON CN.IdTipoServicio = CTS.IdTipoServicio
		INNER JOIN Catalogo.Actividades CA ON CN.IdActividad = CA.IdActividad 
		INNER JOIN Configuracion.Fases CF ON CA.IdFase = CF.IdFase 
	WHERE
		IdNotificacion = @IdNotificacion
END

GO
/****** Object:  StoredProcedure [Configuracion].[sp_NotificacionesObtenerTodos]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Configuracion].[sp_NotificacionesObtenerTodos]
	@pagina INT = 1,
	@filas  INT = 10
AS
BEGIN
	--SELECT @pagina AS pagina,@filas AS filas
	SELECT 
		CN.[IdNotificacion] ,
		CN.[IdTipoServicio] ,
		CN.[IdActividad],
		CN.[CuerpoCorreo],
		CN.[EsCorreo],
		CN.[EsSistema],
		CN.[EmitirAlerta],
		CN.[TiempoAlerta],
		CN.[Frecuencia],
		CN.[AlertaEsCorreo] ,
		CN.[AlertaEsSistema],
		CN.[CuerpoAlerta],
		CN.[FechaInicial] ,
		CN.[FechaFinal],
		CN.[Activo],
		CTS.Nombre AS TipoServicio,
		CA.Nombre AS Actividad ,
		CA.IdFase,
		CF.Nombre AS Fase
		FROM Configuracion.Notificaciones CN
		INNER JOIN Catalogo.TiposServicio CTS ON CN.IdTipoServicio = CTS.IdTipoServicio
		INNER JOIN Catalogo.Actividades CA ON CN.IdActividad = CA.IdActividad 
		INNER JOIN Configuracion.Fases CF ON CA.IdFase = CF.IdFase 
		CROSS JOIN (SELECT COUNT(IdNotificacion) AS Total
					 FROM Configuracion.Notificaciones
					 WHERE Activo = 1) AS R
		WHERE CN.Activo = 1
		ORDER BY IdNotificacion 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
END

GO
/****** Object:  StoredProcedure [Configuracion].[sp_TiposPagoActividadesObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Configuracion].[sp_TiposPagoActividadesObtener]
	@IdTipoPago INT = NULL
AS BEGIN
	--EXEC Configuracion.sp_TiposPagoActividadesObtener 1
	SELECT	TipoPagoNombre = tp.Nombre, 
			ActividadNombre = a.Nombre,
			Aplica = CAST(0 AS BIT),
			Tiempo = CAST(0 AS DECIMAL(18,2))
	FROM Catalogo.Actividades AS a
	CROSS JOIN  Catalogo.TiposPago AS tp
	WHERE a.Activo	= 1
		AND tp.Actividad= 1
		AND tp.IdTipoPago = ISNULL(@IdTipoPago,tp.IdTipoPago)
	ORDER BY tp.IdTipoPago, a.IdActividad	

END
GO
/****** Object:  StoredProcedure [Configuracion].[sp_TiposRegimenFiscalObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [Configuracion].[sp_TiposRegimenFiscalObtener]
AS BEGIN
   	--EXEC Catalogo.sp_TiposRegimenFiscalObtener
	SELECT 	trf.IdRegimenFiscal,
			trf.Nombre	
	FROM Catalogo.TiposRegimenFiscal AS trf
	WHERE trf.Activo = 1
	ORDER BY trf.IdRegimenFiscal

END

GO
/****** Object:  StoredProcedure [Factor].[sp_ClasificacionObtieneFactorEF]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Gil
-- Create date: 25/08/2017
-- Description:	Obtiene la información de Factores que no contenga FactorEntidadFederativa enviando el idClasificaciónFactor
-- =============================================
CREATE PROCEDURE [Factor].[sp_ClasificacionObtieneFactorEF]
	@IdClasificacion INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @IdFactor INT = 0
	SELECT DISTINCT @IdFactor = IdFactor FROM Factor.FactoresEntidadFederativa WHERE IdClasificadorFactor = @IdClasificacion
	SELECT	DISTINCT
			F.IdFactor,
			F.IdTipoServicio,
			F.IdClasificacionFactor,
			F.IdMedidaCobro,
			F.Factor,
			F.Descripcion,
			F.CuotaFactor,
			F.FechaAutorizacion, 
			F.FechaEntradaVigor,
			F.FechaTermino,
			F.FechaPublicacionDof,
			F.Activo 
	FROM Factor.Factores F
	INNER JOIN Catalogo.ClasificacionFactor CF WITH(NOLOCK) ON F.IdClasificacionFactor = CF.IdClasificacionFactor
	WHERE F.IdClasificacionFactor = @IdClasificacion AND (0 = @IdFactor OR IdFactor <> @IdFactor)
END
GO
/****** Object:  StoredProcedure [Factor].[sp_Factores_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_Factores_Actualizar]

	@Identificador INT = NULL,
	@IdTipoServicio INT = NULL,
	@IdClasificacionFactor INT NULL,
	@IdMedidaCobro INT = NULL,
	@Descripcion VARCHAR(100) = '',
	@Factor VARCHAR(100) = '',
	@CuotaFactor DECIMAL(18,2) = NULL ,
	@FechaAutorizacion DATE = NULL,
	@FechaEntradaVigor DATE = NULL,
	@FechaTermino DATE = NULL,
	@FechaPublicacionDof DATE = NULL
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Factor.Factores
		SET 
			IdTipoServicio = @IdTipoServicio,
			IdClasificacionFactor = @IdClasificacionFactor,
			IdMedidaCobro = @IdMedidaCobro,
			Descripcion = @Descripcion,
			Factor = @Factor,
			CuotaFactor = @CuotaFactor,
			FechaAutorizacion = @FechaAutorizacion,
			FechaEntradaVigor = @FechaEntradaVigor,
			FechaTermino = @FechaTermino,
			FechaPublicacionDof = @FechaPublicacionDof
			
		WHERE IdFactor = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdFactor :', @Identificador,',
									IdTipoServicio :', @IdTipoServicio,',
									IdClasificacionFactor :', @IdClasificacionFactor,',
									IdMedidaCobro :', @IdMedidaCobro,',
									Descripcion :', @Descripcion,',
									Factor :', @Factor,',
									CuotaFactor :', @CuotaFactor,',
									FechaAutorizacion :', @FechaAutorizacion,',
									FechaEntradaVigor :', @FechaEntradaVigor,',
									FechaTermino :', @FechaTermino,',
									FechaPublicacionDof :', @FechaPublicacionDof,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Factor].[sp_Factores_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_Factores_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Factor.Factores
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdFactor = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdFactor :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END



GO
/****** Object:  StoredProcedure [Factor].[sp_Factores_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_Factores_Insertar]
	@IdTipoServicio INT = NULL,
	@IdClasificacionFactor INT NULL,
	@IdMedidaCobro INT = NULL,
	@Descripcion VARCHAR(100) = '',
	@Factor VARCHAR(100) = '',
	@CuotaFactor DECIMAL(18,2) = NULL ,
	@FechaAutorizacion DATE = NULL,
	@FechaEntradaVigor DATE = NULL,
	@FechaTermino DATE = NULL,
	@FechaPublicacionDof DATE = NULL
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY

		INSERT INTO Factor.Factores
		(
			IdTipoServicio,
			IdClasificacionFactor,
			IdMedidaCobro,
			Descripcion,
			Factor,
			CuotaFactor,
			FechaAutorizacion,
			FechaEntradaVigor,
			FechaTermino,
			FechaPublicacionDof
		) 
		VALUES
		( 
			@IdTipoServicio,
			@IdClasificacionFactor,
			@IdMedidaCobro,
			@Descripcion,
			@Factor,
			@CuotaFactor,
			@FechaAutorizacion,
			@FechaEntradaVigor,
			@FechaTermino,
			@FechaPublicacionDof
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdTipoServicio :', @IdTipoServicio,',
									IdClasificacionFactor :', @IdClasificacionFactor,',
									IdMedidaCobro :', @IdMedidaCobro,',
									Descripcion :', @Descripcion,',
									Factor :', @Factor,',
									CuotaFactor :', @CuotaFactor,',
									FechaAutorizacion :', @FechaAutorizacion,',
									FechaEntradaVigor :', @FechaEntradaVigor,',
									FechaTermino :', @FechaTermino,',
									FechaPublicacionDof :', @FechaPublicacionDof,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresEntidadFederativa_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_FactoresEntidadFederativa_Actualizar]
	@Identificador INT = NULL,
	@IdClasificadorFactor INT = NULL,
	@IdFactor INT = NULL,
	@Descripcion VARCHAR(300) = NULL,
	@IdEntidFed INT = NULL


AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Factor.FactoresEntidadFederativa
		SET 
			IdClasificadorFactor = @IdClasificadorFactor,
			IdFactor = @IdFactor,
			Descripcion = @Descripcion,
			IdEntidFed = @IdEntidFed
			
		WHERE IdFactEntidFed = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdFactEntidFed 	:', @Identificador,'
									IdClasificadorFactor :', @IdClasificadorFactor,',
									IdFactor :', @IdFactor,',
									Descripcion :', @Descripcion,',
									IdEntidFed :', @IdEntidFed,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresEntidadFederativa_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_FactoresEntidadFederativa_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Factor.FactoresEntidadFederativa
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdFactEntidFed = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdFactEntidFed :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresEntidadFederativa_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_FactoresEntidadFederativa_Insertar]
    @IdClasificadorFactor INT = NULL,
	@IdFactor INT = NULL,
	@Descripcion VARCHAR(300) = NULL,
	@IdEntidFed INT = NULL

AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT INTO Factor.FactoresEntidadFederativa
		(
			IdClasificadorFactor,
			IdFactor,
			Descripcion,
			IdEntidFed
		) 
		VALUES
		( 
			@IdClasificadorFactor,
			@IdFactor,
			@Descripcion,
			@IdEntidFed
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdClasificadorFactor :', @IdClasificadorFactor,',
									IdFactor :', @IdFactor,',
									Descripcion :', @Descripcion,',
									IdEntidFed :', @IdEntidFed,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresEntidadFederativaObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_FactoresEntidadFederativaObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0
	BEGIN
		IF @Todos = @activo
		BEGIN
			SELECT 
				FEF.IdFactEntidFed,
				FEF.IdClasificadorFactor,
				CF.Nombre ClasificadorFactor,
				FEF.IdFactor,
				F.Factor,
				FEF.Descripcion,
				FEF.IdEntidFed,
				FEF.FechaInicial,
				FEF.FechaFinal,
				FEF.Activo,
				Paginas= @Zero
			FROM Factor.FactoresEntidadFederativa FEF
			INNER JOIN Catalogo.ClasificacionFactor CF WITH(NOLOCK) ON FEF.IdClasificadorFactor = CF.IdClasificacionFactor
			INNER JOIN Factor.Factores F WITH(NOLOCK) ON FEF.IdFactor = F.IdFactor
			WHERE FEF.Activo = @activo
		END
		ELSE
		BEGIN
			SELECT 
				FEF.IdFactEntidFed,
				FEF.IdClasificadorFactor,
				CF.Nombre ClasificadorFactor,
				FEF.IdFactor,
				F.Factor,
				FEF.Descripcion,
				FEF.IdEntidFed,
				FEF.FechaInicial,
				FEF.FechaFinal,
				FEF.Activo,
				CASE WHEN @Todos = @Zero THEN
					IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
				ELSE @Zero END AS Paginas
			
			FROM Factor.FactoresEntidadFederativa FEF
			INNER JOIN Catalogo.ClasificacionFactor CF WITH(NOLOCK) ON FEF.IdClasificadorFactor = CF.IdClasificacionFactor
			INNER JOIN Factor.Factores F WITH(NOLOCK) ON FEF.IdFactor = F.IdFactor
			CROSS JOIN (SELECT COUNT(IdFactEntidFed) AS Total
							FROM Factor.FactoresEntidadFederativa 
							WHERE Activo = @activo) AS R
			WHERE FEF.Activo = @activo
			ORDER BY IdFactEntidFed 
			OFFSET ((@pagina - 1) * @filas)  
			ROWS FETCH NEXT @filas ROWS ONLY;
		END
	END
END


GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresEntidadFederativaObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_FactoresEntidadFederativaObtenerPorCriterio]
	@Activo BIT,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
		FEF.IdFactEntidFed,
		FEF.IdClasificadorFactor,
		CF.Nombre ClasificadorFactor,
		FEF.IdFactor,
		F.Factor,
		FEF.Descripcion,
		FEF.IdEntidFed,
		FEF.FechaInicial,
		FEF.FechaFinal,
		FEF.Activo
			
	FROM Factor.FactoresEntidadFederativa FEF
		INNER JOIN Catalogo.ClasificacionFactor CF WITH(NOLOCK) ON FEF.IdClasificadorFactor = CF.IdClasificacionFactor
		INNER JOIN Factor.Factores F WITH(NOLOCK) ON FEF.IdFactor = F.IdFactor
	WHERE FEF.Activo = @Activo

END


GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresEntidadFederativaObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [Factor].[sp_FactoresEntidadFederativaObtenerPorId]
	@Identificador INT
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
		FEF.IdFactEntidFed,
		FEF.IdClasificadorFactor,
		CF.Nombre ClasificadorFactor,
		FEF.IdFactor,
		F.Factor,
		FEF.Descripcion,
		FEF.IdEntidFed,
		FEF.FechaInicial,
		FEF.FechaFinal,
		FEF.Activo
			
		FROM Factor.FactoresEntidadFederativa FEF
		INNER JOIN Catalogo.ClasificacionFactor CF WITH(NOLOCK) ON FEF.IdClasificadorFactor = CF.IdClasificacionFactor
		INNER JOIN Factor.Factores F WITH(NOLOCK) ON FEF.IdFactor = F.IdFactor
	WHERE FEF.IdFactEntidFed = @Identificador

END


GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresEntidadFederativaPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_FactoresEntidadFederativaPorCriterio]
	@Activo BIT,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
		FEF.IdFactEntidFed,
		FEF.IdClasificadorFactor,
		CF.Nombre ClasificadorFactor,
		FEF.IdFactor,
		F.Factor,
		FEF.Descripcion,
		FEF.IdEntidFed,
		FEF.FechaInicial,
		FEF.FechaFinal,
		FEF.Activo
			
	FROM Factor.FactoresEntidadFederativa FEF
	INNER JOIN Catalogo.ClasificacionFactor CF WITH(NOLOCK) ON FEF.IdClasificadorFactor = CF.IdClasificacionFactor
	INNER JOIN Factor.Factores F WITH(NOLOCK) ON FEF.IdFactor = F.IdFactor
	WHERE FEF.Activo = @Activo

END


GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresLeyIngreso_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------Finaliza Insertar-------------------------------------------

--------------------------------------------Inicia Actualizar-------------------------------------------

-- =============================================
-- Author:		Josue Zaragoza Rodriguez
-- Create date: 03/08/2017
-- Description:	Actualiza la informacion de un registro de división para la actividad económica.
-- =============================================
CREATE PROCEDURE [Factor].[sp_FactoresLeyIngreso_Actualizar]

	@Identificador int = NULL,
    @IdAnio int = NULL,
    @IdMes int = NULL,
    @Factor decimal(18,2) = NULL
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Factor.FactoresLeyIngreso
		SET 
			Anio 		= @IdAnio,
			IdMes 		= @IdMes,
			Factor 		= @Factor
			
		WHERE IdFactorLI = @Identificador
		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 		
				'Valor ingresado: { 
									IdFactorLI 	:', @Identificador,'
								  , Anio		:', @IdAnio,'
								  , IdMes		:', @IdMes,'
								  , Factor		:', @Factor,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END






GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresLeyIngreso_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Finaliza Actualizar-------------------------------------------

--------------------------------------------Inicia CambiarEstatus-------------------------------------------
-- =============================================
-- Author:		Josue Zaragoza
-- Create date: 03/08/2017 
-- Description:	Cambia el estado de un registro a activo o desactivo.
-- =============================================
CREATE PROCEDURE [Factor].[sp_FactoresLeyIngreso_CambiarEstatus]
	@Identificador	INT,
	@Activo	bit
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANDivision
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Factor.FactoresLeyIngreso
		SET FechaFinal = GETDATE(), Activo = @Activo			
		WHERE IdFactorLI = @Identificador

		-- COMMIT TRANDivision
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									IdFactorLI :',@Identificador,'
								  , Activo	:', @Activo,'
								}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END






GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresLeyIngreso_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_FactoresLeyIngreso_Insertar]

    @IdAnio INT = NULL,
    @IdMes INT = NULL,
    @Factor DECIMAL(18,2) = NULL

AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANPeriodos
	BEGIN TRAN

	BEGIN TRY  
		
		INSERT into Factor.FactoresLeyIngreso
		(
			Anio,
			IdMes,
			Factor		
		) 
		values
		(
			@IdAnio,
			@IdMes,
			@Factor
		) 
		--	COMMIT TRANDivision
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									Anio		:', @IdAnio,'
								  , IdMes		:', @IdMes,'
								  , Factor		:', @Factor,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END


GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresLeyIngresoObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_FactoresLeyIngresoObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	BEGIN
		SELECT 
				FLI.IdFactorLI,
				FLI.Anio IdAnio,
				A.Descripcion Anio,
				FLI.IdMes,
				M.Nombre Mes,
				FLI.Factor,
				FLI.FechaInicial,
				FLI.FechaFinal,
				FLI.Activo,
				CASE WHEN @Todos = @Zero THEN
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
				ELSE @Zero END AS Paginas	
			
		FROM Factor.FactoresLeyIngreso FLI
		INNER JOIN Catalogo.Anios A WITH(NOLOCK) ON FLI.Anio = A.Anio
		INNER JOIN Catalogo.Meses M WITH(NOLOCK) ON FLI.IdMes = M.IdMes
		CROSS JOIN (SELECT COUNT(IdFactorLI) AS Total
					 FROM Factor.FactoresLeyIngreso
					 WHERE Activo = @activo) AS R
		WHERE Activo = @activo
		ORDER BY IdFactorLI 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
END

GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresLeyIngresoObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Factor].[sp_FactoresLeyIngresoObtenerPorCriterio]
	@Activo BIT = NULL,
	@IdAnio INT = NULL,
	@pagina INT = 1,
	@filas  INT = 20
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
			FLI.IdFactorLI,
			FLI.Anio IdAnio,
			A.Descripcion Anio,
			FLI.IdMes,
			M.Nombre Mes,
			FLI.Factor,
			FLI.FechaInicial,
			FLI.FechaFinal,
			FLI.Activo
			
	FROM Factor.FactoresLeyIngreso FLI
	INNER JOIN Catalogo.Anios A WITH(NOLOCK) ON FLI.Anio = A.Anio
	INNER JOIN Catalogo.Meses M WITH(NOLOCK) ON FLI.IdMes = M.IdMes
	WHERE FLI.Activo = @Activo AND (0 = @IdAnio OR FLI.Anio = @IdAnio)

END

GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresLeyIngresoObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Factor].[sp_FactoresLeyIngresoObtenerPorId]
	@Identificador INT
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT 
			FLI.IdFactorLI,
			FLI.Anio IdAnio,
			A.Descripcion Anio,
			FLI.IdMes,
			M.Nombre Mes,
			FLI.Factor,
			FLI.FechaInicial,
			FLI.FechaFinal,
			FLI.Activo	
			
	FROM Factor.FactoresLeyIngreso FLI
	INNER JOIN Catalogo.Anios A WITH(NOLOCK) ON FLI.Anio = A.Anio
	INNER JOIN Catalogo.Meses M WITH(NOLOCK) ON FLI.IdMes = M.IdMes
	WHERE IdFactorLI = @Identificador

END

GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresObtener]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------Final CambiarEstatus-------------------------------------------

--------------------------------------------Iniciar Obtener-------------------------------------------
-- =============================================
-- Author:		Daniel Gil
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de Factores
-- =============================================
CREATE PROCEDURE [Factor].[sp_FactoresObtener]
	@Todos	BIT = 0,
	@pagina INT,
	@filas  INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @activo INT = 1
	DECLARE @Zero INT = 0

	IF @Todos = @Zero 
	BEGIN
		SELECT 
			F.IdFactor,
			F.IdTipoServicio,
			TS.Nombre TipoServicio,
			F.IdClasificacionFactor,
			CF.Nombre ClasificadorFactor,
			F.IdMedidaCobro,
			MC.Nombre MedidaCobro,
			F.Descripcion,
			F.Factor,
			F.CuotaFactor,
			F.FechaAutorizacion,
			F.FechaEntradaVigor,
			F.FechaTermino,
			F.FechaPublicacionDof,
			F.FechaInicial,
			F.FechaFinal,
			F.Activo,
			CASE WHEN @Todos = @Zero THEN
				IIF(R.Total%@filas = @Zero, R.Total/@filas,R.Total/@filas + @activo)
			ELSE @Zero END Paginas
			
		FROM Factor.Factores F
		INNER JOIN Catalogo.TiposServicio TS WITH(NOLOCK) ON F.IdTipoServicio = TS.IdTipoServicio
		INNER JOIN Catalogo.ClasificacionFactor CF WITH(NOLOCK) ON F.IdClasificacionFactor = CF.IdClasificacionFactor
		INNER JOIN Catalogo.MedidasCobro MC WITH(NOLOCK) ON F.IdMedidaCobro = MC.IdMedidaCobro	
		CROSS JOIN (SELECT COUNT(IdFactor) AS Total
					 FROM Factor.Factores
					 WHERE Activo = @activo) AS R
		WHERE F.Activo = @activo
		ORDER BY IdFactor 
		OFFSET ((@pagina - 1) * @filas)  
		ROWS FETCH NEXT @filas ROWS ONLY;
	END
	ELSE	
	BEGIN
		SELECT 
			F.IdFactor,
			F.IdTipoServicio,
			TS.Nombre TipoServicio,
			F.IdClasificacionFactor,
			CF.Nombre ClasificadorFactor,
			F.IdMedidaCobro,
			MC.Nombre MedidaCobro,
			F.Descripcion,
			F.Factor,
			F.CuotaFactor,
			F.FechaAutorizacion,
			F.FechaEntradaVigor,
			F.FechaTermino,
			F.FechaPublicacionDof,
			F.FechaInicial,
			F.FechaFinal,
			F.Activo,
			Paginas = 0
			
		FROM Factor.Factores F
		INNER JOIN Catalogo.TiposServicio TS WITH(NOLOCK) ON F.IdTipoServicio = TS.IdTipoServicio
		INNER JOIN Catalogo.ClasificacionFactor CF WITH(NOLOCK) ON F.IdClasificacionFactor = CF.IdClasificacionFactor
		INNER JOIN Catalogo.MedidasCobro MC WITH(NOLOCK) ON F.IdMedidaCobro = MC.IdMedidaCobro	
		WHERE F.Activo = @activo
		ORDER BY IdFactor 
	END

END


GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresObtenerPorClasificacion]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Oscar Maya
-- Create date: 25/08/2017
-- Description:	Obtiene la informacion de una Factores
-- =============================================
CREATE PROCEDURE [Factor].[sp_FactoresObtenerPorClasificacion]
	@IdClasificacion INT
AS
BEGIN	
	
	SET NOCOUNT ON;
	SELECT	
			IdFactor,
			IdTipoServicio,
			IdClasificacionFactor,
			IdMedidaCobro,
			Factor,
			Descripcion,
			CuotaFactor,
			FechaAutorizacion, 
			FechaEntradaVigor,
			FechaTermino,
			FechaPublicacionDof,
			Activo 
	FROM Factor.Factores
	WHERE IdClasificacionFactor = @IdClasificacion

END
GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_FactoresObtenerPorCriterio]
	@Activo BIT = NULL,
	@IdFactor INT = NULL,
	@IdTipoServicio INT = NULL,
	@IdClasificacionFactor INT = NULL,
	@pagina INT = 1,
	@filas  INT =20
AS
BEGIN

	SELECT 
		F.IdFactor,
		F.IdTipoServicio,
		TS.Nombre TipoServicio,
		F.IdClasificacionFactor,
		CF.Nombre ClasificadorFactor,
		F.IdMedidaCobro,
		MC.Nombre MedidaCobro,
		F.Descripcion,
		F.Factor,
		F.CuotaFactor,
		F.FechaAutorizacion,
		F.FechaEntradaVigor,
		F.FechaTermino,
		F.FechaPublicacionDof,
		F.FechaInicial,
		F.FechaFinal,
		F.Activo
			
		FROM Factor.Factores F
		INNER JOIN Catalogo.TiposServicio TS WITH(NOLOCK) ON F.IdTipoServicio = TS.IdTipoServicio
		INNER JOIN Catalogo.ClasificacionFactor CF WITH(NOLOCK) ON F.IdClasificacionFactor = CF.IdClasificacionFactor
		INNER JOIN Catalogo.MedidasCobro MC WITH(NOLOCK) ON F.IdMedidaCobro = MC.IdMedidaCobro
	WHERE F.Activo = @Activo AND (0 = @IdFactor OR F.IdFactor = @IdFactor)
	 AND (0 = @IdTipoServicio OR F.IdTipoServicio = @IdTipoServicio)
	 AND (0 = @IdClasificacionFactor OR F.IdClasificacionFactor = @IdClasificacionFactor)

END

GO
/****** Object:  StoredProcedure [Factor].[sp_FactoresObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_FactoresObtenerPorId]
	@Identificador INT
AS
BEGIN

	SELECT 
		F.IdFactor,
		F.IdTipoServicio,
		TS.Nombre TipoServicio,
		F.IdClasificacionFactor,
		CF.Nombre ClasificadorFactor,
		F.IdMedidaCobro,
		MC.Nombre MedidaCobro,
		F.Descripcion,
		F.Factor,
		F.CuotaFactor,
		F.FechaAutorizacion,
		F.FechaEntradaVigor,
		F.FechaTermino,
		F.FechaPublicacionDof,
		F.FechaInicial,
		F.FechaFinal,
		F.Activo
			
		FROM Factor.Factores F
		INNER JOIN Catalogo.TiposServicio TS WITH(NOLOCK) ON F.IdTipoServicio = TS.IdTipoServicio
		INNER JOIN Catalogo.ClasificacionFactor CF WITH(NOLOCK) ON F.IdClasificacionFactor = CF.IdClasificacionFactor
		INNER JOIN Catalogo.MedidasCobro MC WITH(NOLOCK) ON F.IdMedidaCobro = MC.IdMedidaCobro
	WHERE F.IdFactor = @Identificador

END

GO
/****** Object:  StoredProcedure [Factor].[sp_GastosInherentes_Actualizar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Oscar Maya
-- Create date: 03/08/2017
-- Description:	Actualiza la informacion de un registro de TiposServicio para la actividad económica.
-- =============================================
CREATE PROCEDURE [Factor].[sp_GastosInherentes_Actualizar]
(
	@Identificador	INT,
	@Nombre VARCHAR(50),
	@Descripcion	VARCHAR(100)
	)
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANTiposServicio
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Factor.GastosInherentes
		SET Nombre = @Nombre,
			Descripcion = @Descripcion
		WHERE IdGastoInherente = @Identificador
		-- COMMIT TRANTiposServicio
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									identificador	:', @Identificador,
								  ' Nombre			:',@Nombre,
								  ' Descripcion		:', @Descripcion,
								  '}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END





GO
/****** Object:  StoredProcedure [Factor].[sp_GastosInherentes_CambiarEstatus]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Oscar Maya
-- Create date: 03/08/2017
-- Description:	Cambia el estado de un registro a activo o desactivo.
-- =============================================
CREATE PROCEDURE [Factor].[sp_GastosInherentes_CambiarEstatus]
(
	@Identificador	INT,
	@Activo bit
	)
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	-- BEGIN TRANSACTION TRANTiposServicio
	BEGIN TRAN

	BEGIN TRY  
		
		UPDATE Factor.GastosInherentes
		SET FechaFinal = GETDATE(),
		Activo = @Activo			
		WHERE IdGastoInherente = @Identificador

		-- COMMIT TRANTiposServicio
		COMMIT TRAN
		SET @result = @Identificador
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { identificador:', @Identificador,
									'Activo:', @Activo,
									'}'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END





GO
/****** Object:  StoredProcedure [Factor].[sp_GastosInherentes_Insertar]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Oscar Maya
-- Create date: 03/08/2017
-- Description:	Inserta la informacion de un registro de la TiposServicio para la actividad económica.
-- =============================================
CREATE PROCEDURE [Factor].[sp_GastosInherentes_Insertar]
(
	@Nombre VARCHAR(50),
	@Descripcion VARCHAR(100)
	
)	
AS
BEGIN	
	
	SET NOCOUNT ON;
	DECLARE @result INT = 0

	--BEGIN TRANSACTION TRANTiposServicio
	BEGIN TRAN

	BEGIN TRY
			
		INSERT INTO Factor.GastosInherentes 
		(
			Nombre, 
			Descripcion
		) 
		VALUES
		( 
			@Nombre, 
			@Descripcion
		) 
		--	COMMIT TRANTiposServicio
		COMMIT TRAN
		SET @result = SCOPE_IDENTITY()
	
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT @ErrorMessage = CONCAT( ERROR_MESSAGE(), 
				'Valor ingresado: { 
									 NombreGastoInherente :',@Nombre,
								  ', DescGastoInherente:', @Descripcion,'
								  }'),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

		RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );		
	END CATCH;  
	RETURN @result
END





GO
/****** Object:  StoredProcedure [Factor].[sp_GastosInherentes_ObtenerPorCriterio]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Oscar Maya
-- Create date: 03/08/2017
-- Description:	Actualiza la informacion de un registro de TiposServicio para la actividad económica.
-- =============================================
CREATE PROCEDURE [Factor].[sp_GastosInherentes_ObtenerPorCriterio]
(
	@Activo bit,
	@pagina INT = 1,
	@filas  INT =20
)
	AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
	
		IdGastoInherente, 
		Nombre, 
		Descripcion, 
		FechaInicial, 
		FechaFinal, 
		Activo
		
	FROM Factor.GastosInherentes
	WHERE Activo = @Activo
	
END





GO
/****** Object:  StoredProcedure [Factor].[sp_GastosInherentes_ObtenerPorId]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


------------------- GASTOS INHERENTES --------------------
-- =============================================
-- Author:		Oscar Maya
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de una TiposServicio
-- =============================================
CREATE PROCEDURE [Factor].[sp_GastosInherentes_ObtenerPorId]
(
	@Identificador INT
)
AS
BEGIN	
	
	SET NOCOUNT ON;

	SELECT 
	
		IdGastoInherente, 
		Nombre, 
		Descripcion, 
		FechaInicial, 
		FechaFinal, 
		Activo
		
	FROM Factor.GastosInherentes
	WHERE IdGastoInherente = @Identificador

END





GO
/****** Object:  StoredProcedure [Factor].[sp_GastosInherentes_ObtenerTodos]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Oscar Maya
-- Create date: 03/08/2017
-- Description:	Obtiene la informacion de TiposServicio
-- =============================================
CREATE PROCEDURE [Factor].[sp_GastosInherentes_ObtenerTodos]
(
	@pagina INT = 1,
	@filas  INT =20
)
	AS
BEGIN	

	SELECT 
		IdGastoInherente, 
		Nombre, 
		Descripcion, 
		FechaInicial, 
		FechaFinal, 
		Activo,
		R.Paginas
		
	FROM Factor.GastosInherentes
	CROSS JOIN (SELECT COUNT(Nombre) AS Paginas
			     FROM Factor.GastosInherentes) AS R
				 WHERE Activo = 1
	ORDER BY IdGastoInherente 
	OFFSET ((@pagina - 1) * @filas)  
	ROWS FETCH NEXT @filas ROWS ONLY;
END





GO
/****** Object:  StoredProcedure [Factor].[sp_GastosInherentes_ValidarRegistro]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Lucio Arturo Juarez Hernandez
-- Create date: 31/08/2017
-- Description:	Valida el registro del concepto
--				del gasto inherente.
-- =============================================
CREATE PROCEDURE [Factor].[sp_GastosInherentes_ValidarRegistro]
(
	@Identificador	INT,
	@Nombre VARCHAR(50),
	@Descripcion	VARCHAR(100)
)
AS
BEGIN	
	
	DECLARE @RESULTADO					VARCHAR(100) = '';
	DECLARE @FUE_MODIFICADO_EL_REGISTRO	BIT = 1;
	DECLARE @ES_DUPLICADO				BIT = 0;
	DECLARE @EXISTE_VALOR_SIMILAR		BIT = 0;
	DECLARE @TIENE_DEPENDENCIA			BIT = 0;

	-- =============================================
	-- Si no existe entonces es un registro modificado 
	-- o nuevo, si existe entonces es el mismo registro
	-- y se permite actualizar sin validar
	-- =============================================
	IF  EXISTS(SELECT * FROM [Factor].[GastosInherentes]
				WHERE	IdGastoInherente = @Identificador
				AND		RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@Nombre))
				AND		RTRIM(LTRIM(Descripcion)) = RTRIM(LTRIM(@Descripcion)))
		BEGIN
			SET @FUE_MODIFICADO_EL_REGISTRO = 0;
		END
	ELSE
		BEGIN
			IF  EXISTS(SELECT * FROM [Factor].[GastosInherentes]
						  WHERE RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@Nombre))
						  AND	RTRIM(LTRIM(Descripcion)) = RTRIM(LTRIM(@Descripcion)))
				BEGIN
					SET @EXISTE_VALOR_SIMILAR = 1;
				END	
			
			IF	EXISTS(SELECT * FROM [Factor].[GastosInherentes]
						  WHERE RTRIM(LTRIM(Nombre)) = LTRIM(RTRIM(@Nombre))
						  AND IdGastoInherente <> @Identificador
						  )
				BEGIN
					SET @ES_DUPLICADO = 1;
				END
		END

	-- =============================================
	-- Aún no estan registradas las dependencias
	-- 
	--
	-- =============================================
	/*
	IF EXISTS(SELECT * FROM [Catalogo].[GastosInherentes]
				WHERE Nombre = LTRIM(RTRIM(@Nombre)) 
				AND IdGrupo = @IdGrupo )
	*/

	-- =============================================
	-- Reglas de negocio:
	--	1.	Si @FUE_MODIFICADO_EL_REGISTRO = 1, Ya no entra a 
	--		validar y envia el valor vacio.
	--	2.	Si @FUE_MODIFICADO_EL_REGISTRO = 0, entonces entra
	--		a la validación.
	--
	--		2.1	Si  @EXISTE_VALOR_SIMILAR = 1, manda 
	--			el mensaje "Ya existe un valor similar".
	--
	--		2.2 Si	@EXISTE_LA_FRACCION = 1 manda el
	--			el mensaje "Ya existe la fracción".	
	--
	--		2.3 Si @TIENE_DEPENDENCIA = 1 manda el 
	--			mensaje "No puede cambiar la fracción,
	--			ya que existen registros asociados a
	--			la fracción".
	-- =============================================
	IF @FUE_MODIFICADO_EL_REGISTRO = 1
		BEGIN
			IF @ES_DUPLICADO = 1
				SET @RESULTADO = 'El nombre del concepto del gasto inherente ya existe, no se puede guardar su información.';	
			ELSE IF @EXISTE_VALOR_SIMILAR = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'Existe un concepto de gastos inherente con los datos que ha editado, no se puede guardar su información.';
			ELSE IF @TIENE_DEPENDENCIA = 1 AND LEN(@RESULTADO) = 0
				SET @RESULTADO = 'No puede cambiar la información del gasto inherente, ya que existen registros asociados a la concepto de gasto inherente.';
		END
		

	SELECT @RESULTADO AS Resultado
END





GO
/****** Object:  StoredProcedure [Factor].[sp_InsertaFactoresEntidadFederativa]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Factor].[sp_InsertaFactoresEntidadFederativa]
		@Tabla AS FactoresEntidadFederativa READONLY

AS
	BEGIN

	DECLARE @Identidad	INT,
			@Mensaje	VARCHAR(300) 
			


		BEGIN TRY
			BEGIN TRAN

 
				INSERT Factor.FactoresEntidadFederativa(IdClasificadorFactor, 
														IdFactor, 
														Descripcion, 
														IdEntidFed)
						SELECT	IdClasificadorFactor,	
								IdFactor,
								Descripcion,
								IdEntidFed
						FROM @Tabla


				SELECT @Identidad = @@IDENTITY,
						@Mensaje = 'OK'  

				COMMIT
		
	END TRY
	BEGIN CATCH
		ROLLBACK
		SELECT @Identidad = 0,
				@Mensaje = ERROR_MESSAGE()
	END CATCH
	
	SELECT	Identidad = @Identidad, 
			Mensaje = 'OK'
	END
GO
/****** Object:  DdlTrigger [tg_DropTable]    Script Date: 01/09/2017 19:24:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE TRIGGER [tg_DropTable] ON DATABASE FOR DROP_TABLE
AS

	DECLARE @USUARIOCUR	VARCHAR(100),
			@USUARIOLOG	NVARCHAR(100),
			@LOGIN		VARCHAR(100),
			@HOST		VARCHAR(100),
			@TLOGIN		VARCHAR(100),
			@IP			VARCHAR(100);
	
	DECLARE @eventData XML = EVENTDATA();

	SELECT 	@USUARIOCUR =  SUSER_SNAME(),
			@USUARIOLOG =  @eventData.value('data(/EVENT_INSTANCE/UserName)[1]', 'sysname'),
			@LOGIN		= @eventData.value('data(/EVENT_INSTANCE/LoginName)[1]', 'sysname');
			
	IF	( @USUARIOCUR = 'sa'		OR  @USUARIOCUR = 'conec_dba'	OR @USUARIOCUR = 'DELL-609\Gesfor Mexico' OR
		  @USUARIOLOG = 'sa'		OR  @USUARIOLOG = 'conec_dba'	OR @USUARIOLOG = 'DELL-609\Gesfor Mexico' OR
		  @LOGIN = 'sa'				OR  @LOGIN = 'conec_dba'		OR @LOGIN = 'DELL-609\Gesfor Mexico' 
		)
		BEGIN
			PRINT 'Tabla eliminada.';
			COMMIT;
		END
	ELSE
		BEGIN
			PRINT 'No tiene permisos para eliminar la tabla.';
			RAISERROR ('Unauthorized DROP TABLE', 10, 1);
			ROLLBACK;
		END





GO
ENABLE TRIGGER [tg_DropTable] ON DATABASE
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "centroCosto_1"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'VIEW',@level1name=N'vwCentrosCosto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'VIEW',@level1name=N'vwCentrosCosto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "estado"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'VIEW',@level1name=N'vwEstados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'VIEW',@level1name=N'vwEstados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "grupoTarifario_1"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 214
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'VIEW',@level1name=N'vwGruposTarifario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'VIEW',@level1name=N'vwGruposTarifario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "jerarquia_1"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'VIEW',@level1name=N'vwJerarquias'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'VIEW',@level1name=N'vwJerarquias'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Municipio"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 214
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'VIEW',@level1name=N'vwMunicipios'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'VIEW',@level1name=N'vwMunicipios'
GO
USE [master]
GO
ALTER DATABASE [ConecII] SET  READ_WRITE 
GO
