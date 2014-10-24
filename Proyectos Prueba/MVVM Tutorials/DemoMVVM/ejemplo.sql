USE [master]
GO
/****** Object:  Database [EjemploAngelFactura]    Script Date: 04/16/2014 01:59:21 ******/
CREATE DATABASE [EjemploAngelFactura] ON  PRIMARY 
( NAME = N'EjemploAngelFactura', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\EjemploAngelFactura.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EjemploAngelFactura_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\EjemploAngelFactura_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EjemploAngelFactura] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EjemploAngelFactura].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EjemploAngelFactura] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET ANSI_NULLS OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET ANSI_PADDING OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET ARITHABORT OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [EjemploAngelFactura] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [EjemploAngelFactura] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [EjemploAngelFactura] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET  DISABLE_BROKER
GO
ALTER DATABASE [EjemploAngelFactura] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [EjemploAngelFactura] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [EjemploAngelFactura] SET  READ_WRITE
GO
ALTER DATABASE [EjemploAngelFactura] SET RECOVERY FULL
GO
ALTER DATABASE [EjemploAngelFactura] SET  MULTI_USER
GO
ALTER DATABASE [EjemploAngelFactura] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [EjemploAngelFactura] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'EjemploAngelFactura', N'ON'
GO
USE [EjemploAngelFactura]
GO
/****** Object:  Table [dbo].[CabeceraFactura]    Script Date: 04/16/2014 01:59:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CabeceraFactura](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombreCliente] [varchar](50) NULL,
	[FechaEmision] [datetime] NULL,
	[Total] [decimal](18, 3) NULL,
 CONSTRAINT [PK_CabeceraFactura] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 04/16/2014 01:59:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DetalleFactura](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCabecera] [int] NULL,
	[NombreProducto] [varchar](50) NULL,
	[Cantidad] [int] NULL,
	[Precio] [decimal](18, 3) NULL,
 CONSTRAINT [PK_DetalleFactura] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_DetalleFactura_CabeceraFactura]    Script Date: 04/16/2014 01:59:22 ******/
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_DetalleFactura_CabeceraFactura] FOREIGN KEY([IdCabecera])
REFERENCES [dbo].[CabeceraFactura] ([Id])
GO
ALTER TABLE [dbo].[DetalleFactura] CHECK CONSTRAINT [FK_DetalleFactura_CabeceraFactura]
GO
