/****** Object:  Table [dbo].[CatDictamenRespuesta]    Script Date: 02/04/2015 19:14:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CatDictamenRespuesta](
	[idCatDictamenRespuesta] [int] IDENTITY(1,1) NOT NULL,
	[idCampo] [int] NULL,
	[Nombre] [varchar](50) NULL,
	[Valor] [varchar](50) NULL,
	[Bloqueo] [int] NULL,
 CONSTRAINT [PK_CatDictamenRespuesta] PRIMARY KEY CLUSTERED 
(
	[idCatDictamenRespuesta] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


