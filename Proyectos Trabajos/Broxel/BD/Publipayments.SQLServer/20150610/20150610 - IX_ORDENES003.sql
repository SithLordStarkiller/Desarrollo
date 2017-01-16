USE [SistemasCobranza]
GO

DROP INDEX [IX_ORDENES003] ON [dbo].[Ordenes]
GO

CREATE NONCLUSTERED INDEX [IX_ORDENES003]
ON [dbo].[Ordenes] ([idUsuarioPadre],[idUsuario],[idDominio],[Estatus])
INCLUDE ([num_Cred],
	[idOrden],
	[FechaAlta],
	[idVisita])
GO