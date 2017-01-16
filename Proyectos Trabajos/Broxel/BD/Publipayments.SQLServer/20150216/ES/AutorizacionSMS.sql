
GO

/****** Object:  Table [dbo].[AutorizacionSMS]    Script Date: 10/02/2015 02:57:49 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AutorizacionSMS](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[num_Cred] [varchar](15) NOT NULL,
	[idOrden] [int] NOT NULL,
	[Telefono] [char](10) NOT NULL,
	[Clave] [varchar](10)  NULL,
	[FechaAlta] [datetime] NOT NULL,
	[FechaEnvio] [datetime]  NULL,
	[LogIdProb] int  NULL,
	[FechaRespEnvioProb] [datetime]  NULL,
	[FechaRespuestaSMS] [datetime]  NULL,
	[TextoRespuestaSMS] [varchar](180)  NULL,
	[TotalEnvio] [int]  NULL,
 CONSTRAINT [PK_AutorizacionSMS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


