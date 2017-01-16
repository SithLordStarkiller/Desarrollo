SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 2015/04/27
-- Description:	Obtiene las fechas de los reportes 
-- =============================================
ALTER PROCEDURE ObtenerFechasReportes @TipoReporte INT = - 1
	,@idPadre INT = - 1
AS
BEGIN
	SET NOCOUNT ON;

	SELECT substring(CONVERT(CHAR(11), Fecha, 106), 4, 10) AS Fecha
		,CONVERT(CHAR(6), Fecha, 112) FechaReporte
	FROM Reportes
	WHERE Tipo = @TipoReporte
		AND idPadre = @idPadre
		AND CONVERT(CHAR(6), Fecha, 112) != CONVERT(CHAR(6), GETDATE(), 112)
	ORDER BY Fecha DESC
END
GO

--ObtenerFechasReportes 1,250
