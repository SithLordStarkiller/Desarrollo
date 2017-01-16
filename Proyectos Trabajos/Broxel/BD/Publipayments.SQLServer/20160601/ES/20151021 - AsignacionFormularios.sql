/****** Object:  Table [dbo].[AsignacionFormularios]    Script Date: 21/10/2015 06:22:45 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AsignacionFormularios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[NombreFormulario] [varchar](50) NOT NULL,
	[CV_Ruta] [varchar](10) NOT NULL,
	[idDominio] [int] NULL,
	[Orden_Estatus] [int] NULL,
	[Orden_idVisita] [int] NULL,
	[Orden_Tipo] [nvarchar](2) NULL CONSTRAINT [DF_AsignacionFormularios_Orden_Tipo]  DEFAULT (' '),
 CONSTRAINT [PK_AsignacionFormularios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



