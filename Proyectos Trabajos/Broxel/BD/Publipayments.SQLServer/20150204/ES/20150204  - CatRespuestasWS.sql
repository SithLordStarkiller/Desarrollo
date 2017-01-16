/******
Autor: Maximiliano Silva 
Fecha: 04/02/2015 01:27:02 p. m. 
******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CatRespuestasWS](
	[id] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Ruta] [varchar](10) NOT NULL,
	[Descripcion] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_CatRespuestasWS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


