USE [BroxelSMSs]
GO

ALTER TABLE [dbo].[LogSistemasCobranzaSMSs] DROP CONSTRAINT [DF_LogSistemasCobranzaSMSs_FechaAlta]
GO

/****** Object:  Table [dbo].[LogSistemasCobranzaSMSs]    Script Date: 17/02/2015 01:02:36 p. m. ******/
DROP TABLE [dbo].[LogSistemasCobranzaSMSs]
GO

/****** Object:  Table [dbo].[LogSistemasCobranzaSMSs]    Script Date: 17/02/2015 01:02:36 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LogSistemasCobranzaSMSs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FechaAlta] [datetime] NOT NULL,
	[NroTelefono] [char](10) NOT NULL,
	[Mensaje] [varchar](160) NOT NULL,
 CONSTRAINT [PK_LogSistemasCobranzaSMSs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[LogSistemasCobranzaSMSs] ADD  CONSTRAINT [DF_LogSistemasCobranzaSMSs_FechaAlta]  DEFAULT (getdate()) FOR [FechaAlta]
GO


