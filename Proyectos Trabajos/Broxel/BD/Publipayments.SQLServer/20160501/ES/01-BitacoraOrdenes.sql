

/****** Object:  Table [dbo].[BitacoraOrdenes]    Script Date: 25/04/2016 10:14:02 a.m. ******/
DROP TABLE [dbo].[BitacoraOrdenes]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BitacoraOrdenes](
	[idOrden] [int] NOT NULL,
	[idPool] [int] NOT NULL,
	[num_Cred] [nvarchar](50) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[idUsuarioPadre] [int] NOT NULL,
	[idUsuarioAlta] [int] NOT NULL,
	[idDominio] [int] NOT NULL,
	[idVisita] [int] NOT NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Estatus] [int] NOT NULL,
	[usuario] [varchar](32) NULL,
	[FechaModificacion] [datetime] NULL,
	[FechaEnvio] [datetime] NULL,
	[FechaRecepcion] [datetime] NULL,
	[Auxiliar] [varchar](20) NULL,
	[idUsuarioAnterior] [int] NOT NULL DEFAULT ((0)),
	[Tipo] [nvarchar](2) NULL ,
PRIMARY KEY CLUSTERED 
(
	[idOrden] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



