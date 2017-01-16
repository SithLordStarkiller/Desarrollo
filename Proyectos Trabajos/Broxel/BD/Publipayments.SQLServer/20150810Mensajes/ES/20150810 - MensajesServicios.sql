

/****** Object:  Table [dbo].[MensajesServicios]    Script Date: 10/08/2015 02:12:24 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MensajesServicios](
	[idMensaje] [int] IDENTITY(1,1) NOT NULL,
	[idAplicacion] [int] NOT NULL,
	[Titulo] [nvarchar](300) NULL,
	[Mensaje] [varchar](8000) NOT NULL,
	[Clave] [varchar](20) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[EsHtml] [bit] NOT NULL,
	[Tipo] [int] NOT NULL,
 CONSTRAINT [PK_MensajesServicios] PRIMARY KEY CLUSTERED 
(
	[idMensaje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MensajesServicios]  WITH CHECK ADD  CONSTRAINT [FK_MensajesServicios_Aplicacion] FOREIGN KEY([idAplicacion])
REFERENCES [dbo].[Aplicacion] ([idAplicacion])
GO

ALTER TABLE [dbo].[MensajesServicios] CHECK CONSTRAINT [FK_MensajesServicios_Aplicacion]
GO


