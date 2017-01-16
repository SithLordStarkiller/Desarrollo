

CREATE NONCLUSTERED INDEX PK__Bitacora2__433DA71A0539C240
ON [dbo].[BitacoraRespuestas] ([idUsuarioPadre])
INCLUDE ([idOrden],[idCampo],[Valor],[Fecha],[idFormulario])
GO

CREATE NONCLUSTERED INDEX IX_Respuestas004
ON [dbo].[Respuestas] ([idUsuarioPadre])
INCLUDE ([idOrden],[idCampo],[Valor],[idFormulario])

