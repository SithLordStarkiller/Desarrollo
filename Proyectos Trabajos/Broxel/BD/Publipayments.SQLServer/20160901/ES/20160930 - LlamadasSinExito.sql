/****** Object:  Table [dbo].[LlamadasSinExito]    Script Date: 13/09/2016 10:58:56 a.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LlamadasSinExito](
	[ID_ARCHIVO] [int] NOT NULL,
	[CV_CREDITO] [varchar](15) NOT NULL,
	[FECHA_LLAMADA] [datetime] NOT NULL,
	[FECHA_ALTA] [datetime] NOT NULL,
 CONSTRAINT PK_LlamadasSinExito PRIMARY KEY([CV_CREDITO] ASC),
 CONSTRAINT FK_ARCHIVO_LlamadasSinExito FOREIGN KEY ([ID_ARCHIVO]) REFERENCES Archivos(id)
 )

GO

SET ANSI_PADDING OFF
GO