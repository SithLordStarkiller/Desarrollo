
/****** Object:  Table [dbo].[FiltrosAplicacion]    Script Date: 13/05/2016 04:36:05 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FiltrosAplicacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[idFiltro] [int] NOT NULL,
	[Valor] [varchar](100) NULL,
 CONSTRAINT [PK_FiltrosAplicacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[FiltrosAplicacion]  WITH CHECK ADD  CONSTRAINT [FK_FiltrosAplicacion_CatFiltros] FOREIGN KEY([idFiltro])
REFERENCES [dbo].[CatFiltrosAplicacion] ([id])
GO

ALTER TABLE [dbo].[FiltrosAplicacion] CHECK CONSTRAINT [FK_FiltrosAplicacion_CatFiltros]
GO

ALTER TABLE [dbo].[FiltrosAplicacion]  WITH CHECK ADD  CONSTRAINT [FK_FiltrosAplicacion_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([idUsuario])
GO

ALTER TABLE [dbo].[FiltrosAplicacion] CHECK CONSTRAINT [FK_FiltrosAplicacion_Usuario]
GO


