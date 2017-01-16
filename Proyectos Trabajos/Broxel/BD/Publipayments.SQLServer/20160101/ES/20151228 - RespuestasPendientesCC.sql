
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RespuestasPendientes](
	[idOrden] [int] NOT NULL,
	[NombreCampo] [nvarchar](50) NOT NULL,
	[Valor] [nvarchar](350) NULL,
	[idUsuario] [int] NULL,
	[Fecha] DATETIME NULL
PRIMARY KEY CLUSTERED 
(
	[idOrden] ASC
	,NombreCampo ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
