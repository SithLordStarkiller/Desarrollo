-- Creado: 20151009 MJNS
-- Descripción: Actualización de indice para aumentar performance en querys de SMS

USE [SistemasCobranza]
GO

/****** Object:  Index [IX_Respuestas001]    Script Date: 09/10/2015 01:13:33 p. m. ******/
DROP INDEX [IX_Respuestas001] ON [dbo].[Respuestas]
GO

/****** Object:  Index [IX_Respuestas001]    Script Date: 09/10/2015 01:13:33 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Respuestas001] ON [dbo].[Respuestas]
(
	[idCampo] ASC
)
INCLUDE ( 	[idOrden],
	[Valor],
	[idDominio],
	[idUsuarioPadre]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

