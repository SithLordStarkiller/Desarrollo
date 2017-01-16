/****** Object:  Table [dbo].[BitacoraLlamadasSinExito]    Script Date: 13/09/2016 10:58:56 a.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BitacoraLlamadasSinExito](
	[ID_ARCHIVO] [int] NOT NULL,
	[CV_CREDITO] [varchar](15) NOT NULL,
	[FECHA_LLAMADA] [datetime] NOT NULL,
	[FECHA_ALTA] [datetime] NOT NULL,
	[FECHA_MODIFICACION] [datetime] NOT NULL
 )

GO

SET ANSI_PADDING OFF
GO