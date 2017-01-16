
GO
/****** Object:  StoredProcedure [dbo].[ObtenerBitacoraEnvio]    Script Date: 06/06/2016 01:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 2014/09/26
-- Description:	Obtiene los registros a eviar al WS
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerBitacoraEnvio] @TipoConsulta INT = 1
AS
BEGIN
	SET NOCOUNT ON;

	IF (@TipoConsulta = 1)
	BEGIN
		SELECT id
			,idOrden
			,EstatusEnvio
			,EstatusAnterior
			,Fecha
			,FechaEnvio
			,RespuestaEnvio
		FROM BitacoraEnvio
		WHERE FechaEnvio IS NULL
		ORDER BY id;
	END
	ELSE
	BEGIN
		SELECT id
			,idOrden
			,EstatusEnvio
			,EstatusAnterior
			,Fecha
			,FechaEnvio
			,RespuestaEnvio
		FROM BitacoraEnvio
		WHERE FechaEnvio > DATEADD(minute, - 2, GETDATE())
		ORDER BY id;
	END
END
