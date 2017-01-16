
ALTER TABLE [dbo].[RelacionDelegaciones] DROP CONSTRAINT [PK_RelacionDelegaciones]

/****** Object:  Index [PK_RelacionDelegaciones]    Script Date: 04/03/2016 01:19:54 p.m. ******/
ALTER TABLE [dbo].[RelacionDelegaciones] ADD  CONSTRAINT [PK_RelacionDelegaciones] PRIMARY KEY CLUSTERED 
(
	[idRelacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


