SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 04/02/2015
-- Description:	Obtiene el dictamen para enviar al webService
-- =============================================
CREATE PROCEDURE ObtenerDictamenWS @idOrden INT
	,@Ruta VARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1 Clave
	FROM Respuestas r
	INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo
	INNER JOIN CatDictamen d ON c.Nombre = d.Nombre
	WHERE r.idOrden = @idOrden
		AND d.CV_RUTA = @Ruta
END
GO
