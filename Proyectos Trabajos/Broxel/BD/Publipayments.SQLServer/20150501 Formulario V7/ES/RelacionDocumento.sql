/****** Object:  Table [dbo].[RelacionDocumento]    Script Date: 03/26/2015 12:45:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RelacionDocumento](
	[idRelacionDocumento] [int] IDENTITY(1,1) NOT NULL,
	[NombreCampo] [varchar](100) NULL,
	[FaseProceso] [int] NULL,
	[TipoCampo] [int] NULL,
 CONSTRAINT [PK_RelacionDocumento] PRIMARY KEY CLUSTERED 
(
	[idRelacionDocumento] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


